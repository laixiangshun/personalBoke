using NinesKy.BLL.ArticleServiceImpl;
using NinesKy.IBLL.ArticleService;
using NinesKy.Models.Article;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(Article article)
        {
            if (ModelState.IsValid)
            {
                article.CommonModel.Hits = 0;
                article.CommonModel.Inputer = User.Identity.Name;
                article.CommonModel.Model = "Article";
                article.CommonModel.ReleaseDate = System.DateTime.Now;
                article.CommonModel.Status = 99;
                article = articleService.Add(article);
                if (article.ArticleID > 0)
                {
                    return View("AddSuccee", article);
                }
            }
            return View(article);
        }
	}
}