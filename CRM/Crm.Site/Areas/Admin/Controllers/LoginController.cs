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
                return RedirectToAction("/Admin/Home/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

        }

    }
}
