using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.WebHelper
{
    using System.Web.Mvc;
    using IServices;
    public class BaseController:Controller
    {
        protected IsysFunctionServices funSer;
        protected IsysKeyValueServices keyvalSer;
        protected IsysMenusServices menuSer;
        protected IsysOrganStructServices organSer;
        protected IsysPermissListServices permissSer;
        protected IsysRoleServices roleSer;
        protected IsysUserInfoServices userinfoSer;
        protected IsysUserInfo_RoleServices userinfoRoleSer;
        protected IwfProcessServices processSer;
        protected IwfRequestFormServices requestformSer;
        protected IwfWorkServices workSer;
        protected IwfWorkBranchServices workbranchSer;
        protected IwfWorkNodesServices worknodesSer;
    }
}
