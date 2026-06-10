using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.InfoModel
{
    ///<summary>
    ///动态排序
    ///</summary>
    public class DynamicOrder
    {
        public string FieldName { get; set; }
        public bool IsDescending { get; set; } = true;
    }
}
