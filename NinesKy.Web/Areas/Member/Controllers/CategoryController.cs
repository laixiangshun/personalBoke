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

        /// <summary>
        /// 获取json格式栏目树
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult JsonTree(string model)
        {
            return Json(null, JsonRequestBehavior.AllowGet);
        }
	}
}