//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Crm.Services
{
    using Crm.Model;
    using Crm.IServices;
    using Crm.IRepository;
    using System.Linq;
    public partial class sysUserInfoServices : BaseServices<sysUserInfo>, IsysUserInfoServices
    {
        /// <summary>
        /// 注意在构造函数中要对父类中的dal 进行一次赋值
        /// </summary>
        IsysUserInfoDal subdal;
        public sysUserInfoServices(IsysUserInfoDal subdal)
        {
            base.dal = subdal;
            this.subdal = subdal;
        }

        /// <summary>
        /// 根据用户名和密码查找用户实体
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="md5pwd"></param>
        /// <returns></returns>
        public sysUserInfo CheckUser(string uname, string md5pwd)
        {
            return dal.QueryWhere(c => c.uLoginName == uname && c.uLoginPWD == md5pwd).FirstOrDefault();
        }
    }
}
