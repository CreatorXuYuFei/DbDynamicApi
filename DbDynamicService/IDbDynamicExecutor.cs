using Model.InfoModel;

namespace DbDynamicService
{
    ///<summary>
    ///泛化数据库执行器（任意库/任意表/任意字段/任意条件）
    ///基于 NewLife.XCode 实现
    ///</summary>
    public interface IDbDynamicExecutor
    {
        #region 泛化查询
        Task<DbResult> QueryListAsync(
            string dbConnName,
            string tableName,
            List<string>? selectFields = null,
            List<DynamicCondition>? conditions = null,
            List<DynamicOrder>? orders = null);

        Task<DbResult> QueryPageAsync(
            string dbConnName,
            string tableName,
            int pageIndex, int pageSize,
            List<string>? selectFields = null,
            List<DynamicCondition>? conditions = null,
            List<DynamicOrder>? orders = null);

        Task<DbResult> QueryCountAsync(
            string dbConnName,
            string tableName,
            List<DynamicCondition>? conditions = null);
        #endregion

        #region 泛化新增
        Task<DbResult> InsertAsync(
            string dbConnName,
            string tableName,
            Dictionary<string, object> fieldValues);

        Task<DbResult> InsertBatchAsync(
            string dbConnName,
            string tableName,
            List<Dictionary<string, object>> batchFieldValues);
        #endregion

        #region 泛化更新/删除
        Task<DbResult> UpdateAsync(
            string dbConnName,
            string tableName,
            Dictionary<string, object> fieldValues,
            List<DynamicCondition> conditions);

        Task<DbResult> DeleteAsync(
            string dbConnName,
            string tableName,
            List<DynamicCondition> conditions);
        #endregion

        #region 事务 & 原生SQL
        Task<DbResult> ExecuteTransactionAsync(string dbConnName, Func<XCode.DataAccessLayer.DAL, Task<int>> action);
        Task<DbResult> ExecuteNativeSqlAsync(string dbConnName, string sql, Dictionary<string, object> parameters = null);
        #endregion
    }
}
