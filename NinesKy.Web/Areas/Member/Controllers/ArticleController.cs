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
                    //附件处理
                    InterfaceAttachmentService attachmentService = new AttachmentService();
                    var attachmens = attachmentService.FindList(null, User.Identity.Name, string.Empty).ToList();
                    foreach (var att in attachmens)
                    {
                        var filepath = Url.Content(att.FileParth);
                        //判断当前文章的首页图片或者内容是否使用该附件，使用则更新ModelID为当前文章的公共模型ID
                        if ((article.CommonModel.DefaultPicUrl != null && article.CommonModel.DefaultPicUrl.IndexOf(filepath) >= 0) || article.Content.IndexOf(filepath) > 0)
                        {
                            att.ModelID = article.ModelID;
                            attachmentService.Update(att);
                        }
                        else
                        {
                            //未使用改附件则删除附件和数据库中对应的数据
                            System.IO.File.Delete(Server.MapPath(att.FileParth));
                            attachmentService.Delete(att);
                        }
                    }
                    return View("AddSuccee", article);
                }
            }
            return View(article);
        }
	}
}