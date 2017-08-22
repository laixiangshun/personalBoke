using NinesKy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinesKy.DAL
{
    /// <summary>
    /// 数据上下文
    /// </summary>
     public class MyDbContext:DbContext
    {
         public MyDbContext() : base("DefaultConnection") {
             Database.CreateIfNotExists();
         }

         public DbSet<User> Users { get; set; }

         public DbSet<Role> UserGroups { get; set; }

         public DbSet<UserConfig> UserConfig { get; set; }

         public DbSet<UserRoleRelation> UserRoleRelations { get; set; }
    }
}
