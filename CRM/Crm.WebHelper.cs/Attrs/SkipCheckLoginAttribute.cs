using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.WebHelper
{
    /// <summary>
    /// 只要在控制器类上有此标签，标识登录跳过验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple=false)]
    public class SkipCheckLoginAttribute:Attribute
    {
    }
}
