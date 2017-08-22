using NinesKy.DAL.Repository;
using NinesKy.IBLL.ArticleService;
using NinesKy.Models.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinesKy.BLL.ArticleServiceImpl
{
    public class CommonModelService : BaseService<CommonModel>, InterfaceCommentService
    {
        public CommonModelService() : base(RepositoryFactory.CommonModelRepository) { }

        public Comment Add(Comment entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Comment entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}
