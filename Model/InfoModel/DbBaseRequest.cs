using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.InfoModel
{
    #region 基础公共请求（所有数据库操作必传）
    ///<summary>
    ///数据库基础请求参数（所有接口继承此类）
    ///</summary>
    public class DbBaseRequest
    {
        ///<summary>
        ///数据库连接名（对应NewLifeDALHelper配置）
        ///</summary>
        [Required(ErrorMessage = "数据库连接名不能为空")]
        public required string DbConnName { get; set; }

        ///<summary>
        ///操作表名
        ///</summary>
        [Required(ErrorMessage = "表名不能为空")]
        public required string TableName { get; set; }
    }
    #endregion
}
