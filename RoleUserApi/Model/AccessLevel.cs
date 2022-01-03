using RoleUserApi.Model.Repositories.Implementation;
using RoleUserApi.Model.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class AccessLevel : ICrud
    {
        
            public int AccessID { get; set; }
            public string AccessName { get; set; }
            public Organization Organization { get; set; }
            //public int RoleID { get; set; }
            //public int SiteID { get; set; }
            //public int FriendlyNameID { get; set; }
            public Role Role { get; set; }
            //public List<Site> Sites { get; set; }
            public EntryLog EntryLog { get; set; }

            public string Delete(int id)
            {
                INetworkRepo networkRepo = new NetworkRepo();
                Object[] obj = {
               0,
               id
            };
                string res = networkRepo.Post("sp_DeleteAccessLevel", obj);
                return res;
            }

            public string Insert()
            {
                //AccessLevel this = (AccessLevel)obj;
                INetworkRepo networkRepo = new NetworkRepo();
                Object[] objAccessLevel = {
               0,
               this.Organization.OrgID,
               this.AccessName
               //this.Role.RoleID,
               //this.EntryLog.CreateDate,
               //this.EntryLog.CreatedBy
               //this.EntryLog.UpdatedDate,
               //this.EntryLog.UpdatedBy
            };
                DataTable tbl = networkRepo.PostDataTable("sp_InsertAccessLevel", objAccessLevel).Tables[0];
                int accessID = Convert.ToInt32(tbl.Rows[0]["AccessLevelID"]);
                //foreach (var item in this.Sites)
                //{
                //    Object[] objAL = {
                //       0,
                //       accessID,
                //       this.Role.RoleID,
                //       item.SiteCode
                //    };
                //    string res1 = networkRepo.Post("sp_InsertAccessLevelRole", objAL);
                //}
                return "Insert Successful";
            }

            public string Select()
            {
                INetworkRepo networkRepo = new NetworkRepo();
                Object[] obj = {
               0 };
                string res = networkRepo.Post("sp_GetAllAccessLevels", obj);
                return res;
            }

            public string Select(int id)
            {
                INetworkRepo networkRepo = new NetworkRepo();
                Object[] obj = {
               0,
               id
            };
                string res = networkRepo.Post("", obj);
                return res;
            }

            /// <summary>
            ///  We'll do it later
            /// </summary>
            /// <returns></returns>
            public string Update()
            {
                INetworkRepo networkRepo = new NetworkRepo();
                Object[] objAccessLevel = {
               0,
               this.AccessID,
               this.Organization.OrgID,
               this.AccessName,
               //this.EntryLog.CreateDate,
               //this.EntryLog.CreatedBy,
               //this.EntryLog.UpdatedDate,
               //this.EntryLog.UpdatedBy
            };
                string res = networkRepo.Post("sp_UpdateAccessLevel", objAccessLevel);

                object[] obj = {
            0,
            this.AccessID
            };
                DataSet ds = networkRepo.PostDataTable("sp_DeleteAccessLevelRole", obj);
                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null)
                    {
                    //    foreach (var item in this.Sites)
                    //    {
                    //        Object[] objAL = {
                    //   0,
                    //   this.AccessID,
                    //   this.Role.RoleID,
                    //   item.SiteCode
                    //};
                    //        string res1 = networkRepo.Post("sp_InsertAccessLevelRole", objAL);
                    //    }

                    }
                }
                return "Updated Successfully";
            }
        }
    }

