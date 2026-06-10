using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.InfoModel
{
    ///<summary>
    ///泛化分页查询请求
    ///</summary>
    public class QueryPageRequest : DbBaseRequest
    {
        ///<summary>
        ///页码（从1开始）
        ///</summary>
        [Range(1, int.MaxValue, ErrorMessage = "页码必须大于0")]
        public int PageIndex { get; set; } = 1;

        ///<summary>
        ///每页条数
        ///</summary>
        [Range(1, 1000, ErrorMessage = "每页条数范围1-1000")]
        public int PageSize { get; set; } = 10;

        ///<summary>
        ///查询字段
        ///</summary>
        public List<string> SelectFields { get; set; } = new();

        ///<summary>
        ///查询条件
        ///</summary>
        public List<DynamicConditionDto> Conditions { get; set; } = new();

        ///<summary>
        ///排序规则
        ///</summary>
        public List<DynamicOrderDto> Orders { get; set; } = new();
    }
}
