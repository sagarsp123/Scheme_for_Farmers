using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace AgriFarmProj.ViewModel
{
    [DataContract]
    public class Login
    {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string UserType { get; set; }
    }
}