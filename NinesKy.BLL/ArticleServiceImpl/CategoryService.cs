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
    public class CategoryService : BaseService<Category>, InterfaceCategoryService
    {
        public CategoryService() : base(RepositoryFactory.CategoryRepository) { }
    }
}
