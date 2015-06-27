using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Model.ModelViews
{
    public class LoginInfo
    {
        [DisplayName("账号"),Required(ErrorMessage="账号非空")]
        public string uLoginName { get; set; }
        [DisplayName("密码"), Required(ErrorMessage = "密码非空")]
        public string uLoginPWD { get; set; }
        [DisplayName("验证码"), Required(ErrorMessage = "验证码非空")]
        public string Vcode { get; set; }
    }
}
