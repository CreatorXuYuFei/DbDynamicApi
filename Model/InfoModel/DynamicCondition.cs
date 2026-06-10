using Model.EnumModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.InfoModel
{
    ///<summary>
    ///动态查询条件（核心泛化条件）
    ///</summary>
    public class DynamicCondition
    {
        public string FieldName { get; set; }
        public CompareOperator Operator { get; set; }
        public object Value { get; set; }
        public LogicOperator NextLogic { get; set; } = LogicOperator.And;
    }
}
