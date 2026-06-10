using DbDynamicService;
using Microsoft.AspNetCore.Mvc;
using Model.InfoModel;

namespace DbDynamicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    /// <summary>
    /// 通用数据库调度API（对外接口）
    /// </summary>
    public class DbDynamicController(IDbDynamicExecutor dbExecutor) : ControllerBase
    {
        private readonly IDbDynamicExecutor _dbExecutor = dbExecutor;

        #region 1. 列表查询
        [HttpPost("query-list")]
        public async Task<IActionResult> QueryList([FromBody] QueryListRequest request)
        {
            try
            {
                var result = await _dbExecutor.QueryListAsync(
                request.DbConnName,
                request.TableName,
                request.SelectFields,
                request.Conditions.ToDynamicConditions(), // 自动转换
                request.Orders.ToDynamicOrders());        // 自动转换
                return Ok(result);
            }
            catch(Exception ex)
            {
                return Ok(new DbResult { AffectedRows = 0, Data= "", Message =ex.Message, Success =true});
            }
        }
        #endregion

        #region 2. 分页查询
        [HttpPost("query-page")]
        public async Task<IActionResult> QueryPage([FromBody] QueryPageRequest request)
        {
            try
            {
                var result = await _dbExecutor.QueryPageAsync(
                    request.DbConnName,
                    request.TableName,
                    request.PageIndex,
                    request.PageSize,
                    request.SelectFields,
                    request.Conditions.ToDynamicConditions(),
                    request.Orders.ToDynamicOrders());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(new DbResult { AffectedRows = 0, Data = "", Message = ex.Message, Success = true });
            }
        }
        #endregion

        #region 3. 新增
        [HttpPost("insert")]
        public async Task<IActionResult> Insert([FromBody] InsertRequest request)
        {
            try
            {
                var result = await _dbExecutor.InsertAsync(
                    request.DbConnName,
                    request.TableName,
                    request.FieldValues);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(new DbResult { AffectedRows = 0, Data = "", Message = ex.Message, Success = true });
            }
        }
        #endregion

        #region 4. 批量新增
        [HttpPost("insert-batch")]
        public async Task<IActionResult> InsertBatch([FromBody] InsertBatchRequest request)
        {
            try
            {
                var result = await _dbExecutor.InsertBatchAsync(
                    request.DbConnName,
                    request.TableName,
                    request.BatchFieldValues);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(new DbResult { AffectedRows = 0, Data = "", Message = ex.Message, Success = true });
            }
        }
        #endregion

        #region 5. 更新
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateRequest request)
        {
            try
            {
                var result = await _dbExecutor.UpdateAsync(
                    request.DbConnName,
                    request.TableName,
                    request.FieldValues,
                    request.Conditions.ToDynamicConditions());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(new DbResult { AffectedRows = 0, Data = "", Message = ex.Message, Success = true });
            }
        }
        #endregion

        #region 6. 删除
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
        {
            var result = await _dbExecutor.DeleteAsync(
                request.DbConnName,
                request.TableName,
                request.Conditions.ToDynamicConditions());
            return Ok(result);
        }
        #endregion

        #region 7. 执行原生SQL
        [HttpPost("execute-sql")]
        public async Task<IActionResult> ExecuteSql([FromBody] ExecuteSqlRequest request)
        {
            try
            {
                var result = await _dbExecutor.ExecuteNativeSqlAsync(
                    request.DbConnName,
                    request.Sql,
                    request.Parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(new DbResult { AffectedRows = 0, Data = "", Message = ex.Message, Success = true });
            }
        }
        #endregion
    }
}
