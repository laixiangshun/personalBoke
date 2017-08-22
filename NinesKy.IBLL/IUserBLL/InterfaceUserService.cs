using NinesKy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NinesKy.IBLL.IUserBLL
{
     public interface InterfaceUserService:InterfaceBaseService<User>
    {
         bool Exist(string userName);

         User Find(int UserID);

         User Find(string userName);

         /// <summary>
         /// 
         /// </summary>
         /// <param name="pageIndex"></param>
         /// <param name="pageSize"></param>
         /// <param name="?"></param>
         /// <param name="order">排序：0-ID升序（默认），1ID降序，2注册时间升序，3注册时间降序，4登录时间升序，5登录时间降序</param>
         /// <returns></returns>
         IQueryable<User> FindPageList(int pageIndex,int pageSize,out int total,int order);

         ClaimsIdentity CreateIdentity(User user, string authenticationType);
    }
}
