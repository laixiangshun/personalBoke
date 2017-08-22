using NinesKy.BLL.ArticleServiceImpl;
using NinesKy.IBLL.ArticleService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NinesKy.Web.Areas.Member.Controllers
{
    public class ArticleController : Controller
    {
        private InterfaceArticleService articleService;
        private InterfaceCommentService commonModelService;
        public ArticleController()
        {
            articleService = new ArticleService();
            commonModelService = new CommonModelService();
        }

        //菜单
        [ChildActionOnly]
        public ActionResult Menu()
        {
            return PartialView();
        }
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }
	}
}