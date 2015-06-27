using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.WebHelper
{
    /// <summary>
    /// 自定义特性标签标明是否当前请求的action是否为ajax请求方法
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AjaxRequestAttribute : Attribute
    {
    }
}
