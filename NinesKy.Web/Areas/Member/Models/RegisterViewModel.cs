using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NinesKy.Web.Areas.Member.Models
{
    /// <summary>
    /// 注册视图模型
    /// </summary>
    public class RegisterViewModel
    {
        [Required(ErrorMessage="必填")]
        [StringLength(20,MinimumLength=4,ErrorMessage="{2}到{1}个字符")]
        [Display(Name="用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "必填")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{2}到{1}个字符")]
        [Display(Name = "显示名")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage="必填")]
        [Display(Name="密码")]
        [StringLength(20,MinimumLength=6,ErrorMessage="{2}到{1}个字符")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage="必填")]
        [Display(Name="确认密码")]
        [Compare("Password",ErrorMessage="两次输入密码不相同")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage="必填")]
        [Display(Name="邮箱")]
        [DataType(DataType.EmailAddress,ErrorMessage="Email格式不正确")]
        public string Email { get; set; }

        [Required(ErrorMessage="必填")]
        [StringLength(6,MinimumLength=6,ErrorMessage="验证码不正确")]
        [Display(Name="验证码")]
        public string VerificationCode { get; set; }
    }
}