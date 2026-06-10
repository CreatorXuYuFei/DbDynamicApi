using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EnumModel
{
    ///<summary>
    ///比较运算符
    ///</summary>
    public enum CompareOperator
    {
        Equal,              //=
        NotEqual,           //!=
        GreaterThan,        //>
        LessThan,           //<
        GreaterThanOrEqual, //>=
        LessThanOrEqual,    //<=
        Like,               //LIKE
        In,                 //IN
        NotIn               //NOT IN
    }
}
