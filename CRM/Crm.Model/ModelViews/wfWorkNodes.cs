//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Crm.Model.ModelViews
{
    using System;
    using System.Collections.Generic;
    
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    
    public partial class wfWorkNodesView
    {
       
        public int wfnID { get; set; }
        public int wfID { get; set; }
        public int wfnOrderNo { get; set; }
        public int wfNodeType { get; set; }
        public string wfNodeTitle { get; set; }
        public string wfnBizMethod { get; set; }
        public decimal wfnMaxNum { get; set; }
        public int wfnRoleType { get; set; }
        public Nullable<int> wfnExt1 { get; set; }
        public string wfnExt2 { get; set; }
        public int fCreatorID { get; set; }
        public System.DateTime fCreateTime { get; set; }
        public Nullable<int> fUpdateID { get; set; }
        public Nullable<System.DateTime> fUpdateTime { get; set; }
    
        public virtual sysKeyValueView sysKeyValue { get; set; }
        public virtual sysKeyValueView sysKeyValue1 { get; set; }
        public virtual ICollection<wfProcessView> wfProcess { get; set; }
        public virtual wfWorkView wfWork { get; set; }
        public virtual ICollection<wfWorkBranchView> wfWorkBranch { get; set; }
        public virtual ICollection<wfWorkBranchView> wfWorkBranch1 { get; set; }
    }
}
