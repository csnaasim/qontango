using System;
using System.Data;
using System.Collections.Generic;
using RoleUserApi.Model.Repositories.Interface;
using RoleUserApi.Model.Repositories.Implementation;

namespace RoleUserApi.Model
{
    public class Organization
    {
        public int OrgID { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public EntryLog EntryLog { get; set; }

        public static Organization Parse(DataRow row, string ColPrefix = "Org")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}OrgID",
                $"{ColPrefix}Name",
                $"{ColPrefix}Address1",
                $"{ColPrefix}Address2",
                $"{ColPrefix}City",
                $"{ColPrefix}State",
                $"{ColPrefix}Zip",
                $"{ColPrefix}Country",
                $"{ColPrefix}Phone1",
                $"{ColPrefix}Phone2",
                $"{ColPrefix}Email",
                $"{ColPrefix}Logo",
                $"{ColPrefix}IsActive",
                $"{ColPrefix}IsDeleted",
                $"{ColPrefix}CreatedBy",
                $"{ColPrefix}CreateDate",
                $"{ColPrefix}UpdatedBy",
                $"{ColPrefix}UpdateDate",
        };
            if (!row.HasColumns(requiredColumns))
                return null;
            Organization org = new Organization();
            org.OrgID      = row.GetValue<int>($"{ColPrefix}ID");
            org.Name       = row.GetValue<string>($"{ColPrefix}Name");
            org.Address1   = row.GetValue<string>($"{ColPrefix}Address1");
            org.Address2   = row.GetValue<string>($"{ColPrefix}Address2");
            org.City       = row.GetValue<string>($"{ColPrefix}City");
            org.State      = row.GetValue<string>($"{ColPrefix}State");
            org.Zip        = row.GetValue<string>($"{ColPrefix}Zip");
            org.Country    = row.GetValue<string>($"{ColPrefix}Country");
            org.Phone1     = row.GetValue<string>($"{ColPrefix}Phone1");
            org.Phone2     = row.GetValue<string>($"{ColPrefix}Phone2");
            org.Email      = row.GetValue<string>($"{ColPrefix}Email");
            org.Logo       = row.GetValue<string>($"{ColPrefix}Logo");
            org.IsActive   = row.GetValue<bool>($"{ColPrefix}IsActive");
            org.IsDeleted  = row.GetValue<bool>($"{ColPrefix}IsDeleted");
            org.CreatedBy  = row.GetValue<Int64>($"{ColPrefix}CreatedBy");
            org.CreateDate = row.GetValue<DateTime>($"{ColPrefix}CreateDate");
            org.UpdatedBy  = row.GetValue<Int64>($"{ColPrefix}UpdatedBy");
            org.UpdateDate = row.GetValue<DateTime>($"{ColPrefix}UpdateDate");
            return org;
        }

        public static List<Organization> GetAllOrganizations()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetAllOrganizations", obj);
            List<Organization> Organizations = new List<Organization>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Organizations.Add(Parse(item));
            }
            return Organizations;
        }

        public static Organization GetOrganization(Int64 OrgID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0,
               OrgID
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetOrganizationByID", obj);
            Organization Organization = new Organization();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Organization = Parse(ds.Tables[0].Rows[0]);
                }
            }
            return Organization;
        }

        public string Insert()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
            {
                0,
                this.Name,
                this.Address1,
                this.Address2,
                this.City,
                this.State,
                this.Zip,
                this.Country,
                this.Phone1,
                this.Phone2,
                this.Email,
                this.Logo,
                this.IsActive,
                this.IsDeleted,
                this.CreatedBy
            };
            string res = networkRepo.Post("sp_FruInsertOrganization", obj);
            return res;
        }

        public string Update()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
            {
                0,
                this.OrgID,
                this.Name,
                this.Address1,
                this.Address2,
                this.City,
                this.State,
                this.Zip,
                this.Country,
                this.Phone1,
                this.Phone2,
                this.Email,
                this.Logo,
                this.IsActive,
                this.IsDeleted,
                this.UpdatedBy
            };
            string res = networkRepo.Post("sp_FruUpdateOrganization", obj);
            return res;
        }

        public string Delete(int OrgID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
             {
                0,
                OrgID
            };
            string res = networkRepo.Post("sp_FruDeleteOrganization", obj);
            return res;
        }
    }
}
