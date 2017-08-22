using NinesKy.DAL.ArticleDaoImpl;
using NinesKy.DAL.UserDAL;
using NinesKy.IDAL.ArticleDAO;
using NinesKy.IDAL.IUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinesKy.DAL.Repository
{
    /// <summary>
    /// 工厂类：返回项目所有仓库的工厂
    /// </summary>
     public class RepositoryFactory
    {
         //用户仓库
         public static InterfaceUserRepository UserRepository { get { return new UserRepository(); } }

         public static InterfaceCategoryRepository CategoryRepository { get { return new CategoryRepository(); } }

         public static InterfaceArticleRepository ArticleRepository { get { return new ArticleRepository(); } }

         public static InterfaceAttachmentRepository AttachmentRepository { get { return new AttachmentRepository(); } }

         public static InterfaceCommonModelRepository CommonModelRepository { get { return new CommonModelRepository(); } }
    }
}
