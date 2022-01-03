using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class UserPwdHistory
    {
        public string UserName { get; set; }
        public Organization organization { get; set; }
        public DateTime DatePwdChanged { get; set; }
        public string PrvPwd { get; set; }
        public string NewPwd { get; set; }
        public string Remarks { get; set; }
    }
}
