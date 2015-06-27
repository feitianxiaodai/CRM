using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Crm.Site.Areas.Admin.Controllers
{
    using Model.ModelViews;
    using IServices;
    using Crm.Common;
    using Crm.WebHelper;

    [SkipCheckLogin]
    public class LoginController : BaseController
    {
        /// <summary>
        /// 利用autofac传入的接口对象实例赋值给父类
        /// </summary>
        /// <param name="userSer"></param>
        public LoginController(IsysUserInfoServices userSer)
        {
            this.userinfoSer = userSer;
        }


        [HttpGet]
        public ActionResult Login()
        {
            //1.0获取记住三天的cookie是否有
            if(Request.Cookies[Keys.remember]!=null)
            {
                ViewBag.ischeck=true;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginInfo model)
        {
            //按照用户名进行数据库的访问操作
            try
            {
                //视图验证
                if (ModelState.IsValid == false)
                {
                    return View();
                }

                string vcode = string.Empty;
                if (Session[Keys.vcode] != null)
                {
                    vcode = Session[Keys.vcode].ToString();
                }

                if (string.IsNullOrWhiteSpace(model.Vcode) || model.Vcode.Equals(vcode, StringComparison.OrdinalIgnoreCase) == false)
                {
                    ModelState.AddModelError("", "验证码错误");
                    return View();
                }

                //查找数据库验证
                string md5Pwd = Kits.Md5Entry(model.uLoginPWD);
                var userInfo = userinfoSer.CheckUser(model.uLoginName, md5Pwd);

                if (userInfo == null)
                {
                    ModelState.AddModelError("", "用户名或密码错误");
                    return View();
                }
                //将userinfo保存到session中
                Session[Keys.Uinfo] = userInfo;

                //4.0是否记住三天
                if (Request.Form["remember"] != null)
                {
                    //用户选择记住三天,将用户的id写入cookie
                    //userInfo.uID
                    HttpCookie cookie = new HttpCookie(Keys.remember, userInfo.uID.ToString());
                    cookie.Expires = DateTime.Now.AddDays(3);
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    //用户没有勾选则应该手动清空
                    HttpCookie cookie = new HttpCookie(Keys.remember, "");
                    cookie.Expires = DateTime.Now.AddYears(-1);
                    Response.Cookies.Add(cookie);
                }
                return Redirect("/Admin/Home/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

        }

    }
}
