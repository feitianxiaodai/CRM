using System.Web;
using System.Web.Mvc;

namespace Crm.Site
{
    using Crm.WebHelper;
    public class FilterConfig
    {

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //添加自定义过滤路由
            filters.Add(new CheckLoginAttribute());
        }
    }
}