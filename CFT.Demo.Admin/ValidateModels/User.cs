using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace CFT.Demo.Admin.ValidateModels
{
    /*
        [Required] 表示必填
        [MinLength] 表示最小长度
        [MaxLength] 表示最大长度
        [StringLength] 表示可以同时验证最小和最大长度
        [Range] 表示数值的范围
        [Display(Name="XXX")] 表示 给属性起一个名字
    */
    public class User
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name ="用户名")]
        [Required(ErrorMessage ="{0}是必填项")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name ="密码")]
        [Required(ErrorMessage ="{0}是必填项")]
        [MinLength(6,ErrorMessage ="{0}的最小长度为{1}")]
        public string PassWord { get; set; }
    }
}
