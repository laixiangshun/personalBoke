using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinesKy.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required(ErrorMessage="必填")]
        [StringLength(20,MinimumLength=4,ErrorMessage="{1}到{0}个字符")]
        [Display(Name="用户名")]
        public string UserName { get; set; }

        //[Required(ErrorMessage="必填")]
        //[Display(Name="用户组ID")]
        //public int GroupID { get; set; }
        [Required(ErrorMessage="必填")]
        [StringLength(20,MinimumLength=2,ErrorMessage="{1}到{0}个字符")]
        [Display(Name="显示名")]
        public string DisplayName { get; set; }
        [Required(ErrorMessage="必填")]
        [Display(Name="密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage="必填")]
        [Display(Name="邮箱")]
        [DataType(DataType.EmailAddress)]
        public string Emial { get; set; }
        /// <summary>
        /// 用户状态
        /// 0正常，1锁定，2未通过邮件验证，3未通过管理员
        /// </summary>
        public int Status { get; set; }
        public DateTime? RegistrationTime { get; set; }
        //EF实体框架给一个DateTime字段加载一个默认值是 {01/01/0001 00:00:00},
        //它已经在SQL日期类型的范围之外了。所以，如果要让他正常工作，我们需要告诉EF框架不需要创建一个默认的日期时间值。
        //我们可以在模型类型上加一个可空类型，表示它的值是可以为空的
        public DateTime? LoginTime { get; set; }
        public string LoginIP { get; set; }
        public virtual ICollection<UserRoleRelation> UserRoleRelations{ get; set; }


        /// <summary>
        /// 用户状态文字说明
        /// </summary>
        /// <returns></returns>
        public string StatusToString()
        {
            switch (Status)
            {
                case 0:
                    return "正常";
                case 1:
                    return "已锁定";
                case 2:
                    return "未通过邮件验证";
                case 3:
                    return "未通过管理员确认";
                default:
                    return "未知";
            }
        }
    }
}
