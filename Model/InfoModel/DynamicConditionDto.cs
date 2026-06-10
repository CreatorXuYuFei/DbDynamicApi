using Model.EnumModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.InfoModel
{
    ///<summary>
    ///动态查询条件（接口入参）
    ///</summary>
    public class DynamicConditionDto
    {
        ///<summary>
        ///字段名
        ///</summary>
        [Required(ErrorMessage = "条件字段名不能为空")]
        public string FieldName { get; set; }

        ///<summary>
        ///比较运算符
        ///</summary>
        public CompareOperator Operator { get; set; }

        ///<summary>
        ///条件值
        ///</summary>
        public object Value { get; set; }

        ///<summary>
        ///下一个条件逻辑符（默认And）
        ///</summary>
        public LogicOperator NextLogic { get; set; } = LogicOperator.And;
    }
}
