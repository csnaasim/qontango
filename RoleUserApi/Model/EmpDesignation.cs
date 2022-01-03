using System;
using System.Data;
using System.Collections.Generic;
using RoleUserApi.Model.Repositories.Interface;
using RoleUserApi.Model.Repositories.Implementation;

namespace RoleUserApi.Model
{
    public class EmpDesignation 
    {
        public int DesigID { get; set; }
        public int OrgID { get; set; }
        public Organization Org { get; set; }
        public string Title { get; set; }
        public string Descp { get; set; }
        public int ReportsToDesig { get; set; }
        public string ReportsToDescp { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public static EmpDesignation Parse(DataRow row, string ColPrefix = "dsg")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}DesigID",
                $"{ColPrefix}OrgID",
                $"Org",
                $"{ColPrefix}Title",
                $"{ColPrefix}Descp",
                $"{ColPrefix}ReportsToDesig",
                $"{ColPrefix}ReportsToDescp",
                $"{ColPrefix}IsActive",
                $"{ColPrefix}IsDeleted",
                $"{ColPrefix}CreatedBy",
                $"{ColPrefix}CreateDate",
                $"{ColPrefix}UpdatedBy",
                $"{ColPrefix}UpdateDate",
        };
            if (!row.HasColumns(requiredColumns))
                return null;
                EmpDesignation dsg  = new EmpDesignation();
                dsg.DesigID         = row.GetValue<int>($"{ColPrefix}DesigID");
                dsg.OrgID           = row.GetValue<int>($"{ColPrefix}OrgID");
                dsg.Org             = Organization.GetOrganization(dsg.OrgID);
                dsg.Title           = row.GetValue<string>($"{ColPrefix}Title");
                dsg.Descp           = row.GetValue<string>($"{ColPrefix}Descp");
                dsg.ReportsToDesig  = row.GetValue<int>($"{ColPrefix}ReportsToDesig");
                if (dsg.ReportsToDesig == -1)
                    {
                    dsg.ReportsToDescp = "None";
                    }
                else
                {
                    dsg.ReportsToDescp = row.GetValue<string>($"ReportsToDescp");
                }
                dsg.IsActive        = row.GetValue<bool>($"{ColPrefix}IsActive");
                dsg.IsDeleted       = row.GetValue<bool>($"{ColPrefix}IsDeleted");
                dsg.CreatedBy       = row.GetValue<Int64>($"{ColPrefix}CreatedBy");
                dsg.CreateDate      = row.GetValue<DateTime>($"{ColPrefix}CreateDate");
                dsg.UpdatedBy       = row.GetValue<Int64>($"{ColPrefix}UpdatedBy");
                dsg.UpdateDate      = row.GetValue<DateTime>($"{ColPrefix}UpdateDate");
                return dsg;
        }

        public static List<EmpDesignation> SelectAllEmpDesignations(int OrgID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0,
               OrgID
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetAllEmpDesignations", obj);
            List<EmpDesignation> EmpDesignations = new List<EmpDesignation>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                EmpDesignations.Add(Parse(item));
            }
            return EmpDesignations;
        }
        
        public static EmpDesignation GetEmpDesignation(Int64 EmpDesignationID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0,
               EmpDesignationID
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetEmpDesignationByID", obj);
            EmpDesignation FruEmpDesignation = new EmpDesignation();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    FruEmpDesignation = Parse(ds.Tables[0].Rows[0]);
                }
            }
            return FruEmpDesignation;
        }

        public string Select(int OrgID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
             {0, 
              OrgID};
            string res = networkRepo.Post("sp_FruGetAllEmpDesignations", obj);
            return res;
        }

        public string Select()
        {
            throw new NotImplementedException();
        }

        public string Insert()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
            {
                0,
				this.OrgID,
				this.Title,
				this.Descp,
				this.ReportsToDesig,
				this.IsActive,
				this.IsDeleted,
				this.CreatedBy,
				//this.CreateDate,
				//this.UpdatedBy,
				//this.UpdateDate,
            };
            string res = networkRepo.Post("sp_FruInsertEmpDesignation", obj);
            return res;
        }

        public string Update()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
            {
                0,
				this.DesigID,
				this.OrgID,
				this.Title,
				this.Descp,
				this.ReportsToDesig,
				this.IsActive,
				this.IsDeleted,
				//this.CreatedBy,
				//this.CreateDate,
				this.UpdatedBy,
				//this.UpdateDate,
            };
            string res = networkRepo.Post("sp_FruUpdateEmpDesignation", obj);
            return res;
        }

        public string Delete(int EmpDesignationID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
             {
                0,
                EmpDesignationID
            };
            string res = networkRepo.Post("sp_FruDeleteEmpDesignation", obj);
            return res;
        }
    }
}      
