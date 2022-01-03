using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model.Response
{
    public class ResponseModel
    {
        public int StatusCode { get; set; }
        public string Data { get; set; }
    }

    public class ResponseModelNew
    {
        public string StatusCode { get; set; }
        public object Data { get; set; }
    }
}
