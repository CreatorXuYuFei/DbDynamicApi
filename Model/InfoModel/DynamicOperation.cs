using System.ComponentModel.DataAnnotations;

namespace Model.InfoModel
{
    ///<summary>
    ///泛化新增请求
    ///</summary>
    public class InsertRequest : DbBaseRequest
    {
        ///<summary>
        ///字段键值对（字段名=值）
        ///</summary>
        [Required(ErrorMessage = "新增数据不能为空")]
        public Dictionary<string, object> FieldValues { get; set; } = new();
    }

    ///<summary>
    ///泛化批量新增请求
    ///</summary>
    public class InsertBatchRequest : DbBaseRequest
    {
        ///<summary>
        ///批量数据集合
        ///</summary>
        [Required(ErrorMessage = "批量数据不能为空")]
        public List<Dictionary<string, object>> BatchFieldValues { get; set; } = new();
    }

    ///<summary>
    ///泛化更新请求
    ///</summary>
    public class UpdateRequest : DbBaseRequest
    {
        ///<summary>
        ///要更新的字段键值对
        ///</summary>
        [Required(ErrorMessage = "更新字段不能为空")]
        public Dictionary<string, object> FieldValues { get; set; } = new();

        ///<summary>
        ///更新条件（不能为空，禁止全表更新）
        ///</summary>
        [Required(ErrorMessage = "更新条件不能为空")]
        public List<DynamicConditionDto> Conditions { get; set; } = new();
    }

    ///<summary>
    ///泛化删除请求
    ///</summary>
    public class DeleteRequest : DbBaseRequest
    {
        ///<summary>
        ///删除条件（不能为空，禁止全表删除）
        ///</summary>
        [Required(ErrorMessage = "删除条件不能为空")]
        public List<DynamicConditionDto> Conditions { get; set; } = new();
    }

    ///<summary>
    ///原生SQL执行请求
    ///</summary>
    public class ExecuteSqlRequest : DbBaseRequest
    {
        ///<summary>
        ///SQL语句
        ///</summary>
        [Required(ErrorMessage = "SQL语句不能为空")]
        public string Sql { get; set; }

        ///<summary>
        ///SQL参数
        ///</summary>
        public Dictionary<string, object> Parameters { get; set; } = new();
    }
}
