using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.WebHelper
{
    using System.Web.Mvc;
    using Crm.Common;
    using Crm.Model;

    public class CheckLoginAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 负责验证当前系统统一登录
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //判断触发此方法的本身和控制器有skipchecklogin标签，就跳过登录检查
            if (filterContext.ActionDescriptor.IsDefined(typeof(SkipCheckLoginAttribute), false) == true)
            {
                return;
            }
            if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SkipCheckLoginAttribute), false) == true)
            {
                return;
            }

            //01.判断session是否为空
            if (filterContext.HttpContext.Session[Keys.Uinfo] == null)
            {
                //1.0 检查是否有cookie，如果不为空则取值（用户id）,然后查询之后赋值给session
                if (filterContext.HttpContext.Request.Cookies[Keys.remember] != null)
                {
                    string userid = filterContext.HttpContext.Request.Cookies[Keys.remember].Value;
                    int uid = 0;
                    if (!string.IsNullOrWhiteSpace(userid))
                    {
                        uid = int.Parse(userid);
                        //hack
                        CRMEntities db = new CRMEntities();
                        var userinfo = db.sysUserInfo.FirstOrDefault(s => s.uID == uid);
                        if (userinfo != null)
                        {
                            filterContext.HttpContext.Session[Keys.Uinfo] = userinfo;
                        }
                        else
                        {
                            ToUrl(filterContext);
                        }

                    }
                    else
                    {
                        ToUrl(filterContext);
                    }

                }
                else
                {
                    //2.0跳转到登录页面

                    ToUrl(filterContext);
                }

            }
        }

        private static void ToUrl(ActionExecutingContext filterContext)
        {
            //获取当前方法是否有ajax标签
            bool isAjax = filterContext.ActionDescriptor.IsDefined(typeof(AjaxRequestAttribute), false);

            if (isAjax)
            {
                //2.0.1如果是ajax请求，应该返回Json
                JsonResult json = new JsonResult();
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                json.Data = new { status = Enums.EStateType.nologin, msg = "您未登录", loginurl = "/Admin/Login/Login" };
                filterContext.Result = json;
            }
            else
            {
                //2.0.2 如果是浏览器请求则直接将Url到登录页面
                ContentResult content = new ContentResult();
                content.Content = "<script>alert('您未登录');window.location='/Admin/Login/Login';</script>";
                filterContext.Result = content;
            }
        }
    }
}
