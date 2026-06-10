using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.InfoModel
{
    ///<summary>
    ///泛化列表查询请求
    ///</summary>
    public class QueryListRequest : DbBaseRequest
    {
        ///<summary>
        ///查询字段（为空默认查全部 *）
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
