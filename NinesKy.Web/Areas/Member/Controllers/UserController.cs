using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using NinesKy.BLL.UserServiceImpl;
using NinesKy.Common.CheckCode;
using NinesKy.IBLL.IUserBLL;
using NinesKy.Models;
using NinesKy.Web.Areas.Member.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NinesKy.Web.Areas.Member.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private InterfaceUserService userService = new UserService();

        //添加AuthenticationManager属性，在HttpContext.GetOwinContext()中获取这个属性
        private IAuthenticationManager AuthenticationManager { get { return HttpContext.GetOwinContext().Authentication; } }
        //
        // GET: /Member/User/
        public PartialViewResult Menu()
        {
            return PartialView();
        }

        /// <summary>
        /// 生成验证码
        /// TempData中和Session中的区别：TempData只传递一次，保存临时数据，也就是传递到下一个action后，action代码执行完毕就会销毁，Session会持续保存，所以验证码用TempData比较合适
        /// </summary>
        /// <returns></returns>
       [AllowAnonymous]
        public ActionResult VerificationCode()
        {
            string verificationCode = Security.CreateVerificationText(6);
            Bitmap img = Security.CreateVerificationImage(verificationCode, 160, 30);
            Response.ContentType = "image/Jpeg";
            img.Save(Response.OutputStream, ImageFormat.Jpeg);//将图片以指定的格式保存到指定的流中
            TempData["checkcode"] = verificationCode.ToUpper();
            return null;
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 注册提交
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserID,UserName,DisplayName,Password,ConfirmPassword,Email,VerificationCode")]RegisterViewModel register)
        {
            if (TempData["checkcode"] == null || TempData["checkcode"].ToString() != register.VerificationCode.ToUpper())
            {
                ModelState.AddModelError("checkcode", "验证码错误");
                return View(register);
            }
            if (ModelState.IsValid)
            {
                if (userService.Exist(register.UserName)) ModelState.AddModelError("UserName", "用户已经存在");
                else
                {
                    User user = new User()
                    {
                        UserName = register.UserName,
                        DisplayName = register.DisplayName,
                        Password = Security.Sha256(register.Password),
                        Emial = register.Email,
                        Status = 0,
                        RegistrationTime = System.DateTime.Now
                    };
                    user = userService.Add(user);
                    if (user.UserID > 0)
                    {
                        //return Content("注册成功");
                        var identity = userService.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        AuthenticationManager.SignIn(identity);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "注册失败");
                    }
                }
            }
            return View(register);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="url">返回url</param>
        /// <returns></returns>
       [AllowAnonymous]
        public ActionResult Login(string url)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login([Bind(Include = "UserName,Password,RememberMe")] LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = userService.Find(loginModel.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("UserName", "用户名不存在");
                }
                else if (user.Password == Security.Sha256(loginModel.Password))
                {
                    user.LoginTime = System.DateTime.Now;
                    user.LoginIP = Request.UserHostAddress;
                    userService.Update(user);
                    //用CreateIdentity方法创建标识，然后用SignOut方法清空Cookies，然后用SignIn登录
                    var identity = userService.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = loginModel.RememberMe }, identity);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "密码错误");
                }
            }
            return View(loginModel);
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //return RedirectToAction(Url.Content("~/"));//将相对路径转换为绝对路径
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 显示资料
        /// </summary>
        /// <returns></returns>
        public ActionResult Details()
        {
            return View(userService.Find(User.Identity.Name));
        }

        /// <summary>
        /// 修改资料
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modify()
        {
            var user = userService.Find(User.Identity.Name);
            if (user == null) {
                ModelState.AddModelError("", "用户不存在");
            }
            else
            {
                //使用TryUpdateModel来部分更新模型
                //TryUpdateModel(_user, new string[] { "DisplayName", "Email" }) 表示我只想从客户提交的数据中更新DisplayName和Email
                if (TryUpdateModel(user, new string[] { "DisplayName", "Email" }))
                {
                    if (ModelState.IsValid)
                    {
                        if (userService.Update(user))
                        {
                            ModelState.AddModelError("", "修改成功");
                        }
                        else
                        {
                            ModelState.AddModelError("", "无需要修改的资料");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "更新模型数据失败");
                }
            }
            return View("Details", user);
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            //User user = userService.Find(User.Identity.Name);
            //ChangePasswordViewModel password = new ChangePasswordViewModel()
            //{
            //    OriginalPassword = user.Password
            //};

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "OriginalPassword,Password,ConfirmPassword")] ChangePasswordViewModel passwordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = userService.Find(User.Identity.Name);
                if(user.Password==Security.Sha256(passwordViewModel.OriginalPassword))
                {
                    user.Password = Security.Sha256(passwordViewModel.Password);
                    if (userService.Update(user))
                    {
                        ModelState.AddModelError("", "修改密码成功");
                    }
                    else
                    {
                        ModelState.AddModelError("", "修改密码失败");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "原密码错误");
                }
            }
            return View(passwordViewModel);
        }
	}
}