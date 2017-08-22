using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NinesKy.Web.Areas.Member.Models
{
    /// <summary>
    /// 修改密码视图模型
    /// </summary>
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage="必填")]
        [Display(Name="密码")]
        [StringLength(20,MinimumLength=6,ErrorMessage="{2}到{1}个字符")]
        [DataType(DataType.Password)]
        public string OriginalPassword { get; set; }

        [Required(ErrorMessage="必填")]
        [Display(Name="新密码")]
        [DataType(DataType.Password)]
        [StringLength(20,MinimumLength=6,ErrorMessage="{2}到{1}个字符")]
        public string Password { get; set; }

        [Required(ErrorMessage="必填")]
        [Display(Name="确认密码")]
        [Compare("Password",ErrorMessage="两次输入密码不一样")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}