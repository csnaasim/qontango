using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model.Repositories.Interface
{
    interface INetworkRepo
    {
        string Post(string storedProcedure, object[] sqlParameter);
        DataSet Execute(string storedProcedure, object[] sqlParameter);

        DataSet PostDataTable(string v, object[] obj);
        DataSet CustomTypeDataTable(string v, Dictionary<string, object> parameterValues);


    }
}
