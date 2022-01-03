using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class ForgotPassword
    {

        public string UserName { get; set; }
        public string ResetLink { get; set; }
        public string OS { get; set; }
        public string Browser { get; set; }
    }
}
