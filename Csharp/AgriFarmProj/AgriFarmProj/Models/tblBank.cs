//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AgriFarmProj.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public partial class tblBank
    {
        [DataMember]
        public string AccountNo { get; set; }
        [DataMember]
        public Nullable<int> Farmerid { get; set; }
        [DataMember]
        public Nullable<int> Bidderid { get; set; }
        [DataMember]
        public string IFSC_Code { get; set; }
    
        public virtual tblBidder tblBidder { get; set; }
        public virtual tblFarmer tblFarmer { get; set; }
    }
}
