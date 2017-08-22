using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinesKy.Models
{
   public class Role
    {
       [Key]
       public int RoleID { get; set; }

       [Required(ErrorMessage="必填")]
       [StringLength(20,MinimumLength=2,ErrorMessage="{1}到{0}个字")]
       [Display(Name="名称")]
       public string Name { get; set; }

       [Required(ErrorMessage="必填")]
       [Display(Name="角色类型")]
       public int Type { get; set; }

       [Required(ErrorMessage="必填")]
       [StringLength(50,ErrorMessage="少于{0}个字")]
       [Display(Name="说明")]
       public string Description { get; set; }

       public virtual ICollection<UserRoleRelation> UserRoleRelations { get; set; }
       public string TypeToString()
       {
           switch (Type)
           {
               case 0:
                   return "普通";
               case 1:
                   return "特权";
               case 2:
                   return "管理";
               default:
                   return "未知";
           }
       }
    }
}
