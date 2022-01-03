using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class UserLoginHistory
    {
        public int LoginID { get; set; }
        public Organization organization { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LogoutTime { get; set; }
        public int PortNo { get; set; }
        public string MachineIP { get; set; }
        public string Remarks { get; set; }
    }
}
