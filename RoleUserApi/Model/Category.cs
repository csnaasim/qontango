using System;
using System.Data;
using System.Collections.Generic;
using RoleUserApi.Model.Repositories.Interface;
using RoleUserApi.Model.Repositories.Implementation;
using System.Linq;
namespace RoleUserApi.Model
{
    public class Category 
    {
        public Int16 CategoryID { get; set; }
        public string Descp { get; set; }
        //public int OrgID { get; set; }
        //public Organization Org { get; set; }
        public int DispIndex { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }

        // properties define by  ali butt
        public List<Feature> Features { get; set; }
        public List<Feature> FilterFeatures { get; set; }
        public List<Function> Functions { get; set; }
        public Feature Feature { get; set; }

       
        public int FeatureID { get; set; }
        public int FunctionID { get; set; }


        public static Category ParseMasterRolebyCategory (DataRow row, string ColPrefix = "")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}catCategoryID",
                $"{ColPrefix}catDescp",
                $"{ColPrefix}fetFeatureID",
                $"{ColPrefix}fetDescp",
                $"{ColPrefix}fetCategoryID",
                $"{ColPrefix}fefFeatID",
                $"{ColPrefix}fefFunctionID",
                $"{ColPrefix}funDescp",
                $"{ColPrefix}funFunctionID",

        };
            if (!row.HasColumns(requiredColumns))
                return null;
            Category cat = new Category();
            cat.CategoryID = row.GetValue<short>($"{ColPrefix}catCategoryID");
            cat.Descp = row.GetValue<string>($"{ColPrefix}catDescp");
            cat.Features = new List<Feature>();
            cat.Features.Add(Feature.Parse(row));
            //cat.Features=fet.GetFeaturesAssignedToRole(RoleID, OrgID);
            //cat.Feature. = row.GetValue<string>($"{ColPrefix}fetDescp");
            //    org.City = row.GetValue<string>($"{ColPrefix}fetCategoryID");
            //  org.State = row.GetValue<string>($"{ColPrefix}fefFeatID");
            //cat.FunctionID = row.GetValue<int>($"{ColPrefix}fefFunctionID");
            cat.FunctionID = row.GetValue<short>($"{ColPrefix}funFunctionID");
         //   cat.Functions = fun.GetFunctionNew( cat.FunctionID);   
          

            return cat;
        }

        public static List<Category> getAllCategoriesMasterRole(int RoleID , int OrgID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0
               //OrgID
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruMasterRole", obj);
        //    List<Category> Categories = SelectAllCategoriesFeatureWise();
            List<Category> Categories = new List<Category> { };
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                int catId = item.GetValue<int>("catCategoryID");
                Category Category = Categories.Find(cat => cat.CategoryID==catId);

                if (Category == null)
                {
                    Categories.Add(ParseMasterRolebyCategory(item));
                }
                else
                {
                    int fetId = item.GetValue<int>("fetFeatureID");
                   // Feature feature = Category.Features.Find(fet => fet.FeatureID == fetId);
                    Feature feature= Category.Features.Where<Feature>(fet=> fet.FeatureID==fetId).FirstOrDefault();
                    if (feature == null)
                    {
                        Category.Features.Add(Feature.Parse(item));
                    }
                    else
                    {
                        int functionId = item.GetValue<int>("funFunctionID");
                        Function function = feature.functions.Find(fet => fet.FunctionID == functionId);
                       // Function function = Category.Functions.Where<Function>(func=>func.FunctionID==functionId).First();
                        if (function == null)                        {
                            feature.functions.Add(Function.Parse(item));
                        }
                        else
                        {
                            feature.functions.Add(Function.Parse(item));
                        }
                    }

                }
                //Categories.Add(ParseMasterRolebyCategory(item,6,OrgID));
            }
            return Categories;
        }



        public static Category Parse(DataRow row, string ColPrefix = "cat")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}CategoryID",
                $"{ColPrefix}Descp",
                //$"{ColPrefix}OrgID",
                //$"Org",
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
                Category cat   = new Category();
                cat.CategoryID = row.GetValue<Int16>($"{ColPrefix}CategoryID");
                cat.Descp      = row.GetValue<string>($"{ColPrefix}Descp");
                //cat.OrgID      = row.GetValue<int>($"{ColPrefix}OrgID");
                //cat.Org        = Organization.GetOrganization(cat.OrgID);
                cat.DispIndex  = row.GetValue<int>($"{ColPrefix}DispIndex");
                cat.IsActive   = row.GetValue<bool>($"{ColPrefix}IsActive");
                cat.IsDeleted  = row.GetValue<bool>($"{ColPrefix}IsDeleted");
                cat.CreatedBy  = row.GetValue<Int64>($"{ColPrefix}CreatedBy");
                cat.CreateDate = row.GetValue<DateTime>($"{ColPrefix}CreateDate");
                cat.UpdatedBy  = row.GetValue<Int64>($"{ColPrefix}UpdatedBy");
                cat.UpdateDate = row.GetValue<DateTime>($"{ColPrefix}UpdateDate");
                return cat;
        }



        public static Category ParseFeatureWise(DataRow row, string ColPrefix = "cat")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}CategoryID",
                $"{ColPrefix}Descp",
                //$"{ColPrefix}OrgID",
                //$"Org",
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
            Category cat = new Category();
            cat.CategoryID = row.GetValue<Int16>($"{ColPrefix}CategoryID");
            cat.Descp = row.GetValue<string>($"{ColPrefix}Descp");
            //cat.OrgID      = row.GetValue<int>($"{ColPrefix}OrgID");
            //cat.Org        = Organization.GetOrganization(cat.OrgID);
            cat.DispIndex = row.GetValue<int>($"{ColPrefix}DispIndex");
            cat.IsActive = row.GetValue<bool>($"{ColPrefix}IsActive");
            cat.IsDeleted = row.GetValue<bool>($"{ColPrefix}IsDeleted");
            cat.CreatedBy = row.GetValue<Int64>($"{ColPrefix}CreatedBy");
            cat.CreateDate = row.GetValue<DateTime>($"{ColPrefix}CreateDate");
            cat.UpdatedBy = row.GetValue<Int64>($"{ColPrefix}UpdatedBy");
            cat.UpdateDate = row.GetValue<DateTime>($"{ColPrefix}UpdateDate");
            cat.FeatureID = row.GetValue<int>("fetFeatureID");
            cat.FilterFeatures = cat.Feature.GetAllFeaturesForCategory();
            if ( cat.FilterFeatures.Where<Feature>(fet => fet.FeatureID == cat.FeatureID).First() != null)
            {
                cat.Feature =     cat.FilterFeatures.Where<Feature>(fet => fet.FeatureID == cat.FeatureID).First();
                cat.Features.Add(cat.Feature);
            }
           
                
            return cat;
        }



        public static List<Category> SelectAllCategories()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0
               //OrgID
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetAllMenuCategories", obj);
            List<Category> Categories = new List<Category>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Categories.Add(Parse(item));
            }
            return Categories;
        }
        public static List<Category> SelectAllCategoriesFeatureWise()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0
               //OrgID
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetAllMenuCategoriesFeatureWise", obj);
            List<Category> Categories = new List<Category>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Categories.Add(ParseFeatureWise(item));
            }
            return Categories;
        }

        public static Category GetCategory(Int64 CategoryID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0,
               CategoryID
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetMenuCategoryByID", obj);
            Category Category = new Category();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Category = Parse(ds.Tables[0].Rows[0]);
                }
            }
            return Category;
        }

        public string Select(int OrgID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
             {0
              //OrgID
            };
            string res = networkRepo.Post("sp_FruGetAllMenuCategories", obj);
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
				//this.CategoryID,
				this.Descp,
				//this.OrgID,
				this.DispIndex,
				this.IsActive,
				this.IsDeleted,
				this.CreatedBy
				//this.CreateDate,
				//this.UpdatedBy,
				//this.UpdateDate,
            };
            string res = networkRepo.Post("sp_FruInsertMenuCategory", obj);
            return res;
        }

        public string Update()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
            {
                0,
				this.CategoryID,
				this.Descp,
				//this.OrgID,
				this.DispIndex,
				this.IsActive,
				this.IsDeleted,
				//this.CreatedBy,
				//this.CreateDate,
				this.UpdatedBy
				//this.UpdateDate,
            };
            string res = networkRepo.Post("sp_FruUpdateMenuCategory", obj);
            return res;
        }

        public string Delete(int CategoryID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
             {
                0,
                CategoryID
            };
            string res = networkRepo.Post("sp_FruDeleteMenuCategory", obj);
            return res;
        }
    }
}      
