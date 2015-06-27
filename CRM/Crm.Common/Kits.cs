using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Common
{
    public class Kits
    {
        /// <summary>
        /// 将一个对象序列化成json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonString(object obj)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jsor = new System.Web.Script.Serialization.JavaScriptSerializer();

            return jsor.Serialize(obj);
        }

        /// <summary>
        /// 将字符串加密成MD5格式的密文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5Entry(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
        }

        /// <summary>
        /// 判断当前传入的字符串是否为一个整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt(string str)
        {
            int i = 0;
            return int.TryParse(str, out i);
        }
    }
}
