
using RoleUserApi.Model.Repositories.Implementation;
using RoleUserApi.Model.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class Dashboard : ICrud
    {
        public string Delete(int id)
        {
            throw new NotImplementedException();
        }

        public string Insert()
        {
            throw new NotImplementedException();
        }

        public string Select()
        {
            throw new NotImplementedException();
        }

        public string CustomerDashboardJobStatistics(string criteria, int customerID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = { 0, criteria, customerID};
            string res = "";
            res = networkRepo.Post("sp_CustomerDashboardJobStatistics", obj);

            return res;
        }

        public string DashboardJobs(int customerID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = { 0, customerID };
            string res = "";
            res = networkRepo.Post("sp_DashboardJobs", obj);

            return res;
        }

        public string Select(int id)
        {
            throw new NotImplementedException();
        }

        public string Update()
        {
            throw new NotImplementedException();
        }

        internal string MainDashboardData(int jobID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = { 0, jobID };
            string res = "";
            res = networkRepo.Post("sp_MainDashboardData", obj);

            return res;
        }
    }
}
