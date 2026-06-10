using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.InfoModel
{
    ///<summary>
    ///动态排序（接口入参）
    ///</summary>
    public class DynamicOrderDto
    {
        ///<summary>
        ///排序字段名
        ///</summary>
        [Required(ErrorMessage = "排序字段不能为空")]
        public string FieldName { get; set; }

        ///<summary>
        ///是否降序（默认true）
        ///</summary>
        public bool IsDescending { get; set; } = true;
    }
}
