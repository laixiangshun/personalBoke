using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinesKy.Models
{
     public class UserRoleRelation
    {
         [Key]
         public int RelationID { get; set; }

         [Required()]
         public int UserID { get; set; }

         [Required()]
         public int RoleID { get; set; }

         public virtual User User { get; set; }
         public virtual Role Role { get; set; }
    }
}
