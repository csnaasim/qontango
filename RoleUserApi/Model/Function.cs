using System;
using System.Data;
using System.Collections.Generic;
using RoleUserApi.Model.Repositories.Interface;
using RoleUserApi.Model.Repositories.Implementation;

namespace RoleUserApi.Model
{
    public class Function 
    {
        public Int16 FunctionID { get; set; }
        public string Descp { get; set; }
        //public int OrgID { get; set; }
        public string Key { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }

        public static Function Parse(DataRow row, string ColPrefix = "fun")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}FunctionID",
                $"{ColPrefix}Descp",
                //$"{ColPrefix}OrgID",
                $"{ColPrefix}IsActive",
                $"{ColPrefix}IsDeleted",
                $"{ColPrefix}CreatedBy",
                $"{ColPrefix}CreateDate",
                $"{ColPrefix}UpdatedBy",
                $"{ColPrefix}UpdateDate",
        };
            if (!row.HasColumns(requiredColumns))
                return null;
                Function fun   = new Function();
                fun.FunctionID = row.GetValue<Int16>($"{ColPrefix}FunctionID");
                fun.Descp      = row.GetValue<string>($"{ColPrefix}Descp");
                //fun.OrgID      = row.GetValue<int>($"{ColPrefix}OrgID");
                fun.IsActive   = row.GetValue<bool>($"{ColPrefix}IsActive");
                fun.IsDeleted  = row.GetValue<bool>($"{ColPrefix}IsDeleted");
                fun.CreatedBy  = row.GetValue<Int64>($"{ColPrefix}CreatedBy");
                fun.CreateDate = row.GetValue<DateTime>($"{ColPrefix}CreateDate");
                fun.UpdatedBy  = row.GetValue<Int64>($"{ColPrefix}UpdatedBy");
                fun.UpdateDate = row.GetValue<DateTime>($"{ColPrefix}UpdateDate");
            return fun;
        }

        public static List<Function> GetAllFunctions()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0
               //OrgID
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetAllFunctions", obj);
            List<Function> Functions = new List<Function>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Functions.Add(Parse(item));
            }
            return Functions;
        }
        
        public static Function GetFunction(Int16 FunctionID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0,
               FunctionID
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetFunctionByID", obj);
            Function Function = new Function();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Function = Parse(ds.Tables[0].Rows[0]);
                }
            }
            return Function;
        }



        public  List<Function> GetFunctionNew(int FunctionID)
        {
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,
               FunctionID
               //OrgID
            };
                DataSet ds = networkRepo.PostDataTable("sp_FruGetAllFunctions", obj);
                List<Function> Functions = new List<Function>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    Functions.Add(Parse(item));
                }
                return Functions;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string Insert()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
            {
                0,
				this.Descp,
				//this.OrgID,
                this.IsActive,
                this.IsDeleted,
                this.CreatedBy
            };
            string res = networkRepo.Post("sp_FruInsertFunction", obj);
            return res;
        }

        public string Update()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
            {
                0,
				this.FunctionID,
				this.Descp,
				//this.OrgID,
                this.IsActive,
                this.IsDeleted,
                this.UpdatedBy
            };
            string res = networkRepo.Post("sp_FruUpdateFunction", obj);
            return res;
        }

        public string Delete(int FunctionID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
             {
                0,
                FunctionID
            };
            string res = networkRepo.Post("sp_FruDeleteFunction", obj);
            return res;
        }
    }
}      
