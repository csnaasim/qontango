using System;
using System.Data;
using System.Collections.Generic;
using RoleUserApi.Model.Repositories.Interface;
using RoleUserApi.Model.Repositories.Implementation;

namespace RoleUserApi.Model
{
    public class Role 
    {
        public int RoleID { get; set; }
        public int OrgID { get; set; }
        public Organization Org { get; set; }
        public string Name { get; set; }
        public string Descp { get; set; }
        public string Tags { get; set; }
        public Int16 Level { get; set; }
        public bool IsSys { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public EntryLog EntryLog { get; set; }
        public List<Feature> features { get; set; }
        public string FeatureFunctions { get; set; }

        public static Role Parse(DataRow row, string ColPrefix = "rol")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}RoleID",
                $"{ColPrefix}OrgID",
                $"Org",
                $"{ColPrefix}Name",
                $"{ColPrefix}Descp",
                $"{ColPrefix}Tags",
                $"{ColPrefix}Level",
                $"{ColPrefix}IsSys",
                $"{ColPrefix}Remarks",
                $"{ColPrefix}IsActive",
                $"{ColPrefix}IsDeleted",
                $"{ColPrefix}CreatedBy",
                $"{ColPrefix}CreateDate",
                $"{ColPrefix}UpdatedBy",
                $"{ColPrefix}UpdateDate",
        };
            if (!row.HasColumns(requiredColumns))
                return null;
                Role rol       = new Role();
                rol.RoleID     = row.GetValue<int>($"{ColPrefix}RoleID");
                rol.OrgID     = row.GetValue<int>($"{ColPrefix}OrgID");
                rol.Org = Organization.GetOrganization(rol.OrgID);
                rol.Name     = row.GetValue<string>($"{ColPrefix}Name");
                rol.Descp     = row.GetValue<string>($"{ColPrefix}Descp");
                rol.Tags     = row.GetValue<string>($"{ColPrefix}Tags");
                rol.Level     = row.GetValue<Int16>($"{ColPrefix}Level");
                rol.IsSys     = row.GetValue<bool>($"{ColPrefix}IsSys");
                rol.Remarks     = row.GetValue<string>($"{ColPrefix}Remarks");
                rol.IsActive     = row.GetValue<bool>($"{ColPrefix}IsActive");
                rol.IsDeleted     = row.GetValue<bool>($"{ColPrefix}IsDeleted");
                rol.CreatedBy     = row.GetValue<Int64>($"{ColPrefix}CreatedBy");
                rol.CreateDate     = row.GetValue<DateTime>($"{ColPrefix}CreateDate");
                rol.UpdatedBy     = row.GetValue<Int64>($"{ColPrefix}UpdatedBy");
                rol.UpdateDate     = row.GetValue<DateTime>($"{ColPrefix}UpdateDate");
                return rol;
        }

        public static List<Role> GetAllRoles(int OrgID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0,
               OrgID
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetAllRoles", obj);
            List<Role> Roles = new List<Role>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Roles.Add(Parse(item));
            }
            return Roles;
        }

        public static dynamic GetAllRolesWithFeatures(int OrgID)
        {
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,
               OrgID
            };
                DataSet ds = networkRepo.PostDataTable("sp_FruGetAllRoles", obj);
                List<Role> Roles = new List<Role>();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            Roles.Add(Parse(item));
                        }

                        var listFeatures = Feature.GetAllFeaturesJson();
                        var result = new { Roles = Roles, Features = listFeatures };
                        return result;
                    }
                    throw new Exception("No results found.");
                }
                else
                {
                    throw new Exception("No results found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Role GetRole(Int64 RoleID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0,
               RoleID
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetRoleByID", obj);
            Role Role = new Role();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Role = Parse(ds.Tables[0].Rows[0]);
                }
            }
            return Role;
        }

        public string Insert()
        {
            this.FeatureFunctions = this.FeatureFunctions.Replace("[", "");
            this.FeatureFunctions = this.FeatureFunctions.Replace("]", "");
            this.FeatureFunctions = this.FeatureFunctions.Replace('"', '-');
            this.FeatureFunctions = this.FeatureFunctions.Replace("-", "");

            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
            {
                0,
				this.OrgID,
				this.Name,
				this.Descp,
				this.Tags,
				this.Level,
				this.IsSys,
				this.IsActive,
				this.IsDeleted,
                this.FeatureFunctions,
				this.CreatedBy,
 				this.Remarks
           };
            string res = networkRepo.Post("sp_FruInsertRole", obj);
            return res;
        }

        public string Update()
        {
            this.FeatureFunctions = this.FeatureFunctions.Replace("[", "");
            this.FeatureFunctions = this.FeatureFunctions.Replace("]", "");
            this.FeatureFunctions = this.FeatureFunctions.Replace('"', '-');
            this.FeatureFunctions = this.FeatureFunctions.Replace("-", "");

            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
            {
                0,
				this.RoleID,
				this.OrgID,
				this.Name,
				this.Descp,
				this.Tags,
				this.Level,
				this.IsSys,
				this.IsActive,
				this.IsDeleted,
                this.FeatureFunctions,
				this.UpdatedBy,
				this.Remarks
            };
            string res = networkRepo.Post("sp_FruUpdateRole", obj);
            return res;
        }

        public string Delete(int RoleID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
             {
                0,
                RoleID
            };
            string res = networkRepo.Post("sp_FruDeleteRole", obj);
            return res;
        }

        //public string GetAllFeatures()
        //{
        //    INetworkRepo networkRepo = new NetworkRepo();
        //    Object[] obj = {
        //       0
        //    };
        //    string res = networkRepo.Post("sp_GetAllFeatures", obj);
        //    return res;
        //}

        //public void GetFeaturesAssignedToRole()
        //{
        //    try
        //    {
        //        INetworkRepo networkRepo = new NetworkRepo();
        //        Object[] objFeature = {
        //       0,
        //       this.RoleID
        //    };
        //        DataSet dataSet = networkRepo.PostDataTable("sp_FruGetFeaturesAssignedToRole", objFeature);
        //        GetFeaturesInfoFromDataset(dataSet);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private List<Feature> GetFeaturesInfoFromDataset(DataSet dataSet)
        //{
        //    if (dataSet != null && dataSet.Tables.Count > 0)
        //    {
        //        DataTable table = dataSet.Tables[0];
        //        if (table != null && table.Rows.Count > 0)
        //        {
        //            List<Feature> features = new List<Feature>();
        //            foreach (DataRow item in table.Rows)
        //            {
        //                Feature feature      = new Feature();
        //                feature.FeatureID    = Convert.ToInt16(item["fetFeatureID"]);
        //                feature.Descp        = item["fetDescp"].ToString();
        //                feature.Category     = item["fetCategory"].ToString();
        //                feature.IsDefault    = Convert.ToBoolean(item["rffIsDefault"]);
        //                feature.Path         = item["fetPath"].ToString();
        //                feature.Link         = item["fetLink"].ToString();
        //                feature.Icon         = item["fetIcon"].ToString();
        //                feature.LoadChildren = item["fetDescp"].ToString();
        //                feature.HelpCode     = Convert.ToInt16(item["fetHelpCode"]);
        //                feature.functions = new List<Function>();
        //                //foreach (DataRow item in table.Rows)
        //                //{
        //                //    Function function = new Function();
        //                //    function.FunctionID = Convert.ToInt16(item["funFunctionID"]);
        //                //    function.Descp = item["funDescp"].ToString();

        //                //    this.functions.Add(function);
        //                //}
        //                feature.GetFunctionsbyFeatures();
        //                features.Add(feature);
        //            }
        //            return features;
        //        }
        //        return features;
        //    }
        //    return features;
        //}
    }
}      
