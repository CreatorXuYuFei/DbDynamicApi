using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.InfoModel
{
    ///<summary>
    ///通用数据库操作结果
    ///</summary>
    public class DbResult
    {
        public bool Success { get; set; }
        public int AffectedRows { get; set; }
        public object Data { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
