using CoreLogger;
using Model.EnumModel;
using Model.InfoModel;
using NewLife.Data;
using ToolService;
using XCode.DataAccessLayer;

namespace DbDynamicService
{
    ///<summary>
    ///基于 NewLife.XCode 的泛化数据库执行器
    ///集成官方连接池 NewLifeDALHelper，支持任意表/字段/条件动态操作
    ///</summary>
    public class XCodeDynamicExecutor : IDbDynamicExecutor
    {
        #region 获取DAL实例
        private static DAL GetDal(string dbConnName)
        {
            if (string.IsNullOrWhiteSpace(dbConnName))
                throw new ArgumentNullException(nameof(dbConnName));

            return NewLifeDALHelper.Create(dbConnName);
        }
        #endregion

        #region 动态条件构建（XCode参数化，防注入）
        private static (string WhereSql, Dictionary<string, object> Params) BuildCondition(List<DynamicCondition>? conditions)
        {
            var paramDict = new Dictionary<string, object>();
            if (conditions == null || conditions.Count == 0)
                return ("1=1", paramDict);

            var conditionList = new List<string>();
            int index = 0;
            foreach (var cond in conditions)
            {
                string paramName = $"p{index++}";
                //SafeField(cond.FieldName) 给字段加反引号
                conditionList.Add($"{SafeField(cond.FieldName)} {GetOperatorText(cond.Operator)} @{paramName}");
                //====================== 解决中文乱码 ======================
                //字符串 → 保持字符串
                //数字/时间/布尔 → 保持原始类型
                //解决中文：只处理 string 类型，不影响其他类型
                //=============================================================
                paramDict.Add(paramName, FixStringValueForChinese(cond.Value));
            }

            return (string.Join(" AND ", conditionList), paramDict);
        }
        #endregion

        #region 动态排序构建
        private static string BuildOrder(List<DynamicOrder>? orders)
        {
            if (orders == null || orders.Count == 0)
                return " ORDER BY Id DESC";

            var orderList = orders.Select(o => $"{o.FieldName} {(o.IsDescending ? "DESC" : "ASC")}");
            return $" ORDER BY {string.Join(",", orderList)}";
        }
        #endregion

        #region 泛化查询
        public async Task<DbResult> QueryListAsync(string dbConnName, string tableName, List<string>? selectFields = null, List<DynamicCondition>? conditions = null, List<DynamicOrder>? orders = null)
        {
            try
            {
                var dal = GetDal(dbConnName);
                string fields = selectFields == null || selectFields.Count == 0 ? "*" : string.Join(",", selectFields);
                var (whereSql, paras) = BuildCondition(conditions);
                string orderSql = BuildOrder(orders);

                string sql = $"SELECT {fields} FROM {tableName} WHERE {whereSql} {orderSql}";
                var data = await Task.Run(() => dal.Query(sql, paras));
                var item = DataTableToDic(data);

                return new DbResult
                {
                    Success = true,
                    Data = item
                };
            }
            catch (Exception ex)
            {
                SysLogHelper.Error($"动态查询失败!", ex, "XCode泛化DB");
                return new DbResult { Success = false, Message = ex.Message };
            }
        }

        public async Task<DbResult> QueryPageAsync(string dbConnName, string tableName, int pageIndex, int pageSize, List<string>? selectFields = null, List<DynamicCondition>? conditions = null, List<DynamicOrder>? orders = null)
        {
            try
            {
                var dal = GetDal(dbConnName);
                string fields = selectFields == null || selectFields.Count == 0 ? "*" : string.Join(",", selectFields);
                var (whereSql, paras) = BuildCondition(conditions);
                string orderSql = BuildOrder(orders);

                int offset = (pageIndex - 1) * pageSize;
                string sql = $"SELECT {fields} FROM {tableName} WHERE {whereSql} {orderSql} LIMIT @offset,@pageSize";

                paras.Add("offset", offset);
                paras.Add("pageSize", pageSize);

                var data = await dal.QueryAsync(sql, paras);
                var countResult = await QueryCountAsync(dbConnName, tableName, conditions);
                var item = DataTableToDic(data);
                return new DbResult
                {
                    Success = true,
                    Data = new
                    {
                        Total = countResult.Data,
                        List = item
                    }
                };
            }
            catch (Exception ex)
            {
                SysLogHelper.Error($"动态分页查询失败!", ex, "XCode泛化DB");
                return new DbResult { Success = false, Message = ex.Message };
            }
        }

        public async Task<DbResult> QueryCountAsync(string dbConnName, string tableName, List<DynamicCondition>? conditions = null)
        {
            try
            {
                var dal = GetDal(dbConnName);
                var (whereSql, paras) = BuildCondition(conditions);
                string sql = $"SELECT COUNT(*) FROM {tableName} WHERE {whereSql}";

                var count = await dal.ExecuteScalarAsync<int>(sql, paras);
                return new DbResult { Success = true, Data = count };
            }
            catch (Exception ex)
            {
                SysLogHelper.Error($"动态统计数量失败!", ex, "XCode泛化DB");
                return new DbResult { Success = false, Message = ex.Message };
            }
        }
        #endregion

        #region 泛化新增
        public async Task<DbResult> InsertAsync(string dbConnName, string tableName, Dictionary<string, object>? fieldValues)
        {
            try
            {
                if (fieldValues == null || fieldValues.Count == 0)
                    return new DbResult { Success = false, Message = "字段值不能为空" };

                var dal = GetDal(dbConnName);

                //===================== 修复中文 =====================
                var finalParams = FixStringValues(fieldValues);
                //===============================================================

                string fields = string.Join(",", finalParams.Keys.Select(k => $"`{k}`"));
                string values = string.Join(",", finalParams.Select(k => $"@{k.Key}"));
                string sql = $"INSERT INTO {tableName} ({fields}) VALUES ({values})";

                int rows = await dal.ExecuteAsync(sql, finalParams);

                return new DbResult { Success = true, AffectedRows = rows };
            }
            catch (Exception ex)
            {
                SysLogHelper.Error($"动态插入失败!", ex, "XCode泛化DB");
                return new DbResult { Success = false, Message = ex.Message };
            }
        }

        public async Task<DbResult> InsertBatchAsync(string dbConnName, string tableName, List<Dictionary<string, object>>? batchFieldValues)
        {
            try
            {
                if (batchFieldValues == null || batchFieldValues.Count == 0)
                    return new DbResult { Success = false, Message = "批量数据不能为空" };

                var dal = GetDal(dbConnName);
                var first = batchFieldValues.First();

                //字段拼接
                string fields = string.Join(",", first.Keys.Select(k => $"`{k}`"));
                //参数用 @字段名
                string values = string.Join(",", first.Keys.Select(k => $"@{k}"));
                string sql = $"INSERT INTO `{tableName}` ({fields}) VALUES ({values})";

                List<Dictionary<string, object>> pairs = [];
                foreach (var dic in batchFieldValues)
                {
                    pairs.Add(FixStringValues(dic));
                }

                //XCode 批量插入原生支持 List<Dictionary>，直接传
                int rows = await dal.ExecuteAsync(sql, pairs);

                return new DbResult { Success = true, AffectedRows = rows };
            }
            catch (Exception ex)
            {
                SysLogHelper.Error($"动态批量插入失败!", ex, "XCode泛化DB");
                return new DbResult { Success = false, Message = ex.Message };
            }
        }
        #endregion

        #region 泛化更新/删除
        public async Task<DbResult> UpdateAsync(string dbConnName, string tableName, Dictionary<string, object>? fieldValues, List<DynamicCondition>? conditions)
        {
            try
            {
                if (fieldValues == null || fieldValues.Count == 0 || conditions == null || conditions.Count == 0)
                    return new DbResult { Success = false, Message = "更新字段/条件不能为空" };

                var dal = GetDal(dbConnName);

                //正确构建 SET 语句：k.Key = @k.Key
                string setSql = string.Join(", ", fieldValues.Select(k => $"{SafeField(k.Key)} = @{k.Key}"));

                //构建条件
                var (whereSql, paras) = BuildCondition(conditions);

                //合并更新字段的参数
                foreach (var item in fieldValues)
                {
                    //防止重复添加
                    if (!paras.ContainsKey(item.Key))
                        paras.Add(item.Key, item.Value);
                }

                paras = FixStringValues(paras);
                string sql = $"UPDATE {tableName} SET {setSql} WHERE {whereSql}";
                int rows = await dal.ExecuteAsync(sql, paras);

                return new DbResult { Success = true, AffectedRows = rows, Message = rows > 0 ? "修改成功！" : "修改失败！" };
            }
            catch (Exception ex)
            {
                SysLogHelper.Error($"动态更新失败!", ex, "XCode泛化DB");
                return new DbResult { Success = false, AffectedRows = 0, Message = ex.Message };
            }
        }

        public async Task<DbResult> DeleteAsync(string dbConnName, string tableName, List<DynamicCondition>? conditions)
        {
            try
            {
                if (conditions == null || conditions.Count == 0)
                    return new DbResult { Success = false, Message = "删除条件不能为空，禁止全表删除" };

                var dal = GetDal(dbConnName);
                var (whereSql, paras) = BuildCondition(conditions);
                string sql = $"DELETE FROM {tableName} WHERE {whereSql}";

                int rows = await dal.ExecuteAsync(sql, paras);
                return new DbResult { Success = true, AffectedRows = rows };
            }
            catch (Exception ex)
            {
                SysLogHelper.Error($"动态删除失败!", ex, "XCode泛化DB");
                return new DbResult { Success = false, Message = ex.Message };
            }
        }
        #endregion

        #region 事务 & 原生SQL
        public async Task<DbResult> ExecuteTransactionAsync(string dbConnName, Func<DAL, Task<int>> action)
        {
            var dal = GetDal(dbConnName);
            try
            {
                dal.BeginTransaction();

                int rows = await action(dal);
                dal.Commit();

                return new DbResult { Success = true, AffectedRows = rows };
            }
            catch (Exception ex)
            {
                dal?.Rollback();
                SysLogHelper.Error($"事务执行失败!", ex, "XCode泛化DB");
                return new DbResult { Success = false, Message = ex.Message };
            }
        }

        public async Task<DbResult> ExecuteNativeSqlAsync(string dbConnName, string sql, Dictionary<string, object>? parameters = null)
        {
            try
            {
                var dal = GetDal(dbConnName);
                int rows = await dal.ExecuteAsync(sql, parameters);
                return new DbResult { Success = true, AffectedRows = rows };
            }
            catch (Exception ex)
            {
                SysLogHelper.Error($"原生SQL执行失败!", ex, "XCode泛化DB");
                return new DbResult { Success = false, Message = ex.Message };
            }
        }
        #endregion

        #region DataTable 转 字典列表
        ///<summary>
        ///DataTable 转 字典列表
        ///</summary>
        private static List<Dictionary<string, object>> DataTableToDic(DbTable dt)
        {
            var list = new List<Dictionary<string, object>>();
            if (dt == null || dt.Rows.Count == 0) return list;

            for (int row = 0; row < dt.Rows.Count; row++)
            {
                var dic = new Dictionary<string, object>();
                for (var col = 0; col < dt.Columns.Length; col++)
                {
                    string value;
                    if (dt.Rows[row][col] == null)
                        value = "";
                    else
                        value = dt.Rows[row][col].ToString() ?? "";
                    dic.Add(dt.Columns[col], value);
                }
                list.Add(dic);
            }
            return list;
        }
        #endregion

        #region 解决字段名关键字冲突
        //给字段名加反引号，解决关键字冲突
        private static string SafeField(string fieldName)
        {
            //MySQL 关键字必须包裹 ``
            return $"`{fieldName}`";
        }

        private static string GetOperatorText(CompareOperator op)
        {
            return op switch
            {
                CompareOperator.Equal => "=",
                CompareOperator.NotEqual => "!=",
                CompareOperator.GreaterThan => ">",
                CompareOperator.LessThan => "<",
                CompareOperator.GreaterThanOrEqual => ">=",
                CompareOperator.LessThanOrEqual => "<=",
                CompareOperator.Like => "LIKE",
                CompareOperator.In => "IN",
                CompareOperator.NotIn => "NOT IN",
                _ => "="
            };
        }
        #endregion

        #region 泛型数据库字段类型兼容
        private static object FixStringValueForChinese(object value)
        {
            if (value == null || value == DBNull.Value)
                return DBNull.Value;

            //只要能安全转成字符串，并且不是数字/日期/布尔等基础类型 → 按 string 处理
            //解决中文：强制以 String 类型传入 ADO.NET
            if (value is IConvertible convertible)
            {
                var typeCode = convertible.GetTypeCode();

                //基础值类型 → 保持不变
                if (typeCode is TypeCode.Boolean
                    or TypeCode.Byte
                    or TypeCode.Decimal
                    or TypeCode.Double
                    or TypeCode.Int16
                    or TypeCode.Int32
                    or TypeCode.Int64
                    or TypeCode.UInt16
                    or TypeCode.UInt32
                    or TypeCode.UInt64
                    or TypeCode.Single
                    or TypeCode.DateTime)
                {
                    return value;
                }
            }

            //其余全部 → 强制安全字符串（解决中文、char、object[]、动态类型）
            return Convert.ToString(value) ?? string.Empty;
        }
        #endregion

        #region 修复中文插入/更新问题，不破坏 int/DateTime/bool
        private static Dictionary<string, object> FixStringValues(Dictionary<string, object> parameters)
        {
            var dict = new Dictionary<string, object>();
            foreach (var kv in parameters)
            {
                if (kv.Value == null || kv.Value == DBNull.Value)
                {
                    dict[kv.Key] = DBNull.Value;
                    continue;
                }

                //值类型 → 保持不变
                if (kv.Value is bool or int or long or double or decimal or DateTime)
                {
                    dict[kv.Key] = kv.Value;
                }
                //其余 → 强制安全字符串（解决中文编码）
                else
                {
                    dict[kv.Key] = Convert.ToString(kv.Value) ?? "";
                }
            }
            return dict;
        }
        #endregion
    }
}
