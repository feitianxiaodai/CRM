//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Crm.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class sysRole
    {
        public sysRole()
        {
            this.sysPermissList = new HashSet<sysPermissList>();
            this.sysUserInfo_Role = new HashSet<sysUserInfo_Role>();
        }
    
        public int rID { get; set; }
        public Nullable<int> eDepID { get; set; }
        public int RoleType { get; set; }
        public string rName { get; set; }
        public string rRemark { get; set; }
        public int rSort { get; set; }
        public int rStatus { get; set; }
        public int rCreatorID { get; set; }
        public System.DateTime rCreateTime { get; set; }
        public Nullable<int> rUpdateID { get; set; }
        public System.DateTime rUpdateTime { get; set; }
    
        public virtual sysKeyValue sysKeyValue { get; set; }
        public virtual sysOrganStruct sysOrganStruct { get; set; }
        public virtual ICollection<sysPermissList> sysPermissList { get; set; }
        public virtual ICollection<sysUserInfo_Role> sysUserInfo_Role { get; set; }
    }
}
