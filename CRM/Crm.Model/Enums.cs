using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Model
{
    public class Enums
    {
        /// <summary>
        /// 返回的json字符串中的类型
        /// </summary>
        public enum EStateType
        {
            success = 1,
            error = 0,
            nologin = 2
        }
    }
}
