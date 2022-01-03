using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model.Repositories.Interface
{
    interface ICrud
    {
        string Insert();
        string Select();
        string Select(int id);
        string Update();
        string Delete(int id);
    }
}
