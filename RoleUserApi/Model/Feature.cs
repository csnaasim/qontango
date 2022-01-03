using System;
using System.Data;
using System.Collections.Generic;
using RoleUserApi.Model.Repositories.Interface;
using RoleUserApi.Model.Repositories.Implementation;
using Newtonsoft.Json;

namespace RoleUserApi.Model
{
    public class Feature
    {
        public Int16 FeatureID { get; set; }
        public string Descp { get; set; }
        //public int OrgID { get; set; }
        //public Organization Org { get; set; }
        public int MenuCategoryID { get; set; }
        public Category MenuCategory { get; set; }
        public string Path { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
        public string LoadChildren { get; set; }
        public int HelpCode { get; set; }
        public int DisplayIndex { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDefault { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public List<Feature> features { get; set; }
        public List<Function> functions { get; set; }
        public string FunctionsList { get; set; }
        public int AssignToRole { get; set; }
        public bool CrudFunctions { get; set; }
        public static Feature ParseRows(DataRowCollection dataRowCollection)
        {
            Feature feature = new Feature();

            foreach (DataRow row in dataRowCollection) { 
            
            
            }
            return feature;
        }
        public static Feature Parse(DataRow row, string ColPrefix = "fet")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}FeatureID",
                $"{ColPrefix}Descp",
                //$"{ColPrefix}OrgID",
                //$"Org",
                $"{ColPrefix}Category",
                $"{ColPrefix}CategoryID",
                $"{ColPrefix}Path",
                $"{ColPrefix}Link",
                $"{ColPrefix}Icon",
                $"{ColPrefix}LoadChildren",
                $"{ColPrefix}HelpCode",
                $"{ColPrefix}DispIndex",
                $"{ColPrefix}IsActive",
                $"{ColPrefix}IsDeleted",
                $"{ColPrefix}CreatedBy",
                $"{ColPrefix}CreateDate",
                $"{ColPrefix}UpdatedBy",
                $"{ColPrefix}UpdateDate",
        };
            if (!row.HasColumns(requiredColumns))
                return null;
            Feature fet        = new Feature();
            fet.FeatureID      = row.GetValue<Int16>($"{ColPrefix}FeatureID");
            fet.Descp          = row.GetValue<string>($"{ColPrefix}Descp");
            //fet.OrgID          = row.GetValue<int>($"{ColPrefix}OrgID");
            //fet.Org            = Organization.GetOrganization(fet.OrgID);
            fet.MenuCategoryID = row.GetValue<int>($"{ColPrefix}CategoryID");
            fet.MenuCategory   = Category.GetCategory(fet.MenuCategoryID);
            fet.Path           = row.GetValue<string>($"{ColPrefix}Path");
            fet.Link           = row.GetValue<string>($"{ColPrefix}Link");
            fet.Icon           = row.GetValue<string>($"{ColPrefix}Icon");
            fet.LoadChildren   = row.GetValue<string>($"{ColPrefix}LoadChildren");
            fet.HelpCode       = row.GetValue<int>($"{ColPrefix}HelpCode");
            fet.DisplayIndex   = row.GetValue<int>($"{ColPrefix}DispIndex");
            fet.IsActive       = row.GetValue<bool>($"{ColPrefix}IsActive");
            fet.IsDeleted      = row.GetValue<bool>($"{ColPrefix}IsDeleted");
            fet.CreatedBy      = row.GetValue<Int64>($"{ColPrefix}CreatedBy");
            fet.CreateDate     = row.GetValue<DateTime>($"{ColPrefix}CreateDate");
            fet.UpdatedBy      = row.GetValue<Int64>($"{ColPrefix}UpdatedBy");
            fet.UpdateDate     = row.GetValue<DateTime>($"{ColPrefix}UpdateDate");
            fet.functions = new List<Function>();
            fet.functions.Add(Function.Parse(row));
            return fet;
        }

        public static List<Feature> GetAllFeatures()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0
               //OrgID
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetAllFeatures", obj);
            List<Feature> Features = new List<Feature>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Features.Add(Parse(item));
            }
            return Features;
        }  
        public  List<Feature> GetAllFeaturesForCategory()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0
               //OrgID
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetAllFeatures", obj);
            List<Feature> Features = new List<Feature>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Features.Add(Parse(item));
            }
            return Features;
        }

        public static string GetAllFeaturesJson()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0
               //OrgID
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetAllFeaturesJson", obj);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return GetJSonFromTables(ds);
                }
                return "No results found.";
            }
            else
            {
                throw new Exception("No results found.");
            }
        }

        private static string GetJSonFromTables(DataSet ds)
        {
            try
            {
                using (System.IO.StringWriter sw = new System.IO.StringWriter())
                {
                    System.Collections.Generic.List<object> featuresandfunctionsasigned = new System.Collections.Generic.List<object>();
                    foreach (DataRow feature in ds.Tables[0].Rows)
                    {
                        System.Collections.Generic.List<object> Functions = new System.Collections.Generic.List<object>();
                        var funtionsOfFeature = ds.Tables[1].AsEnumerable().Where(record => record["fetDescp"].ToString().Equals(feature["fetDescp"].ToString()));

                        foreach (var function in funtionsOfFeature)
                        {
                            Functions.Add(new
                            {
                                FunctionID = function["funFunctionID"],
                                //Key   = function["fetFeatureID"].ToString() + "_" + function["funFunctionID"].ToString(),
                                Key   = function["fetDescp"].ToString() + "_" + function["funDescp"].ToString(),
                                Descp = function["funDescp"].ToString()
                            });
                        }
                        object featur = new
                        {
                            FeatureID      = Convert.ToInt32(feature["fetFeatureID"]),
                            Key            = feature["fetDescp"].ToString(),
                            Descp          = feature["fetDescp"].ToString(),
                            MenuCategoryID = Convert.ToInt32(feature["fetCategoryID"]),
                            MenuCategory   = Category.GetCategory(Convert.ToInt32(feature["fetCategoryID"])),  //feature["fetCategory"].ToString(),
                            Path           = feature["fetPath"].ToString(),
                            Link           = feature["fetLink"].ToString(),
                            Icon           = feature["fetIcon"].ToString(),
                            LoadChildren   = feature["fetLoadChildren"].ToString(),
                            DisplayIndex   = Convert.ToInt32(feature["fetDispIndex"]),
                            IsActive       = Convert.ToBoolean(feature["fetIsActive"]),
                            IsDeleted      = Convert.ToBoolean(feature["fetIsDeleted"]),
                            Functions
                        };
                        featuresandfunctionsasigned.Add(featur);
                    }
                    return JsonConvert.SerializeObject(featuresandfunctionsasigned);
                }
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public static Feature GetFeature(Int64 FeatureID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0,
               FeatureID
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetFeatureByID", obj);
            Feature Feature = new Feature();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Feature = Parse(ds.Tables[0].Rows[0]);
                }
            }
            return Feature;
        }

        public string Insert()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
            {
                0,
                this.Descp,
                //this.OrgID,
                this.MenuCategoryID,
                this.Path,
                this.Link,
                this.Icon,
                this.LoadChildren,
                this.HelpCode,
                this.DisplayIndex,
                this.IsActive,
                this.IsDeleted,
                this.CreatedBy,
                this.CrudFunctions,
                this.FunctionsList,
                this.AssignToRole
            };
            string res = networkRepo.Post("sp_FruInsertFeature", obj);
            return res;
        }
 
        public string Update()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
            {
                0,
                this.FeatureID,
                this.Descp,
                //this.OrgID,
                this.MenuCategoryID,
                //this.Path,
                //this.Link,
                //this.Icon,
                //this.LoadChildren,
                //this.HelpCode,
                this.DisplayIndex,
                this.IsActive,
                this.IsDeleted,
                this.UpdatedBy,
                this.CrudFunctions,
                this.FunctionsList,
                this.AssignToRole
            };
            string res = networkRepo.Post("sp_FruUpdateFeature", obj);
            return res;
        }

        public string Delete(int FeatureID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
             {
                0,
                FeatureID
            };
            string res = networkRepo.Post("sp_FruDeleteFeature", obj);
            return res;
        }

        public string AssignCrudToFeature(int FeatureID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
             {
                0,
                FeatureID
                //OrgID
            };
            string res = networkRepo.Post("sp_FruAssignCrudToFeature", obj);
            return res;
        }

        public string AssignFunctionToFeature(int FeatureID, int FunctionID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
             {
                0,
                FeatureID,
                FunctionID
                //OrgID
            };
            string res = networkRepo.Post("sp_FruAssignFunctionToFeature", obj);
            return res;
        }

        public string AssignFeatureToRole(int FeatureID, int FunctionID, int RoleID, int OrgID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
             {
                0,
                FeatureID,
                FunctionID,
                RoleID,
                OrgID
            };
            string res = networkRepo.Post("sp_FruAssignFeatureToRole", obj);
            return res;
        }

        public List<Feature> GetFeaturesAssignedToRole(int RoleID, int OrgID)
        {
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                Object[] obj = {
                   0,
                   RoleID,
                   OrgID
                };

                DataSet dataSet = networkRepo.PostDataTable("sp_FruGetFeaturesAssignedToRole", obj);
                return GetFeaturesInfoFromDataset(dataSet, RoleID);

            }
            catch (Exception ex)
            {
            }
            return features;
        }

        public List<Feature> GetFeaturesNotAssignedToRole(int RoleID, int OrgID)
        {
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                Object[] obj = {
                   0,
                   RoleID,
                   OrgID
                };

                DataSet dataSet = networkRepo.PostDataTable("sp_FruGetFeaturesNotAssignedToRole", obj);
                return GetFeaturesInfoFromDataset(dataSet, RoleID);

            }
            catch (Exception ex)
            {
            }
            return features;
        }

        private List<Feature> GetFeaturesInfoFromDataset(DataSet dataSet, int RoleID) 
        {
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                DataTable table = dataSet.Tables[0];
                if (table != null && table.Rows.Count > 0)
                {
                   // List<Feature>
                    features = new List<Feature>();
                    foreach (DataRow item in table.Rows)
                    {
                        Feature feature = new Feature();
                        feature.FeatureID      = Convert.ToInt16(item["fetFeatureID"]);
                        feature.Descp          = item["fetDescp"].ToString();
                        feature.MenuCategoryID = Convert.ToInt32(item["fetCategoryID"]);
                        feature.MenuCategory   = Category.GetCategory(Convert.ToInt16(item["fetFeatureID"]));
                        feature.IsDefault      = Convert.ToBoolean(item["rffIsDefault"]);
                        feature.Path           = item["fetPath"].ToString();
                        feature.Link           = item["fetLink"].ToString();
                        feature.Icon           = item["fetIcon"].ToString();
                        feature.LoadChildren   = item["fetDescp"].ToString();
                        feature.HelpCode       = Convert.ToInt16(item["fetHelpCode"]);
                        feature.DisplayIndex   = Convert.ToInt32(item["fetDispIndex"]);
                        feature.functions      = GetFunctionsbyFeature(Convert.ToInt16(item["fetFeatureID"]), feature.Descp, RoleID);
                        features.Add(feature);
                    }
                   // return features;
                }
//                return features;
            }
            return features;
        }

        public List<Function> GetFunctionsbyFeature(int FeatureID, string FeatureDescp, int RoleID)
        {
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                Object[] objFeature = {
                   0,
                   FeatureID,
                   RoleID
                };
                DataSet dataSet = networkRepo.PostDataTable("sp_FruGetFunctionsByFeatureID", objFeature);
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    DataTable table = dataSet.Tables[0];
                    if (table != null && table.Rows.Count > 0)
                    {
                        return GetFunctionsFromDataRows(table.Rows, FeatureDescp);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public List<Function> GetFunctionByID(int FunctionID, string FeatureDescp)
        {
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                Object[] objFeature = {
                   0,
                   FunctionID
                };
                DataSet dataSet = networkRepo.PostDataTable("sp_FruGetFunctionByID", objFeature);
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    DataTable table = dataSet.Tables[0];
                    if (table != null && table.Rows.Count > 0)
                    {
                        return GetFunctionsFromDataRows(table.Rows, FeatureDescp);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private List<Function> GetFunctionsFromDataRows(DataRowCollection Rows, string FeatureDescp)
        {
            List<Function> functions = new List<Function>();
            foreach (DataRow item in Rows)
            {
                functions.Add(new Function
                {
                    FunctionID = Convert.ToInt16(item["funFunctionID"]),
                    //Key   = function["fetFeatureID"].ToString() + "_" + function["funFunctionID"].ToString(),
                    Key = FeatureDescp + "_" + item["funDescp"].ToString(),
                    Descp = item["funDescp"].ToString()
                });
            }
            return functions;
        }
    }
}
