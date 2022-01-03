using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class RoleUser
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public int OrgID { get; set; }
    }
}
