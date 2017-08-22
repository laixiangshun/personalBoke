using NinesKy.BLL.ArticleServiceImpl;
using NinesKy.IBLL.ArticleService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NinesKy.Web.Areas.Member.Controllers
{
    public class CategoryController : Controller
    {
        private InterfaceCategoryService categoryService;
        public CategoryController()
        {
            categoryService = new CategoryService();
        }
	}
}