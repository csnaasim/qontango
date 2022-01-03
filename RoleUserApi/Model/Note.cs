using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RoleUserApi.Model.Repositories.Implementation;
using RoleUserApi.Model.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class Note
    {
        public string AGENCY_ID { get; set; }
        public int DEBTOR_ID { get; set; }
        public int NOTE_ID { get; set; }
        public string NOTE { get; set; }
        public int EMP_ID { get; set; }
        public DateTime Inserted_At { get; set; }
        public string Priority { get; set; }
        public string AliasName { get; set; }
        public Debtor Debtor { get; set; }

        public bool Suppress { get; set; }
        public string url { get; set; }

        #region Functions
      
     
        public static Note Parse(DataRow row, string basicpath, string ColPrefix = "")
        {
           
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}AGENCY_ID",
                $"{ColPrefix}DEBTOR_ID",
                $"{ColPrefix}NOTE_ID",
                $"{ColPrefix}NOTE",
                $"{ColPrefix}EMP_ID",
                $"{ColPrefix}StampTime",
                $"{ColPrefix}Priority",
              

        };
            if (!row.HasColumns(requiredColumns))
                return null;
            Note acc = new Note();
            acc.AGENCY_ID = row.GetValue<string>($"{ColPrefix}AGENCY_ID");
            acc.DEBTOR_ID = row.GetValue<int>($"{ColPrefix}DEBTOR_ID");
            acc.NOTE_ID = row.GetValue<int>($"{ColPrefix}NOTE_ID");
            acc.NOTE = row.GetValue<string>($"{ColPrefix}NOTE");
            acc.EMP_ID = row.GetValue<int>($"{ColPrefix}EMP_ID");
            acc.Inserted_At = row.GetValue<DateTime>($"{ColPrefix}StampTime");
            acc.Priority = row.GetValue<string>($"{ColPrefix}Priority");
            acc.AliasName = row.GetValue<string>($"{ColPrefix}AliasName");
            acc.url = row.GetValue<string>($"{ColPrefix}pdfPath");
            acc.Debtor = Debtor.Parse(row);
          string  filePath = basicpath + "\\" + acc.DEBTOR_ID+"\\"+acc.url;
            if (!File.Exists(filePath))
            {
                acc.url = null;
            }
            else
            {
                byte[] url = geturlpath(filePath);

                string base64String = Convert.ToBase64String(url, 0, url.Length);
                string url1 = "data:application/pdf;base64," + base64String;
                acc.url = url1;
            }
          
            return acc;
        }
        public  static dynamic geturlpath(string filePath)
        {


            byte[] bytes = System.IO.File.ReadAllBytes(filePath);

            return bytes;
            //using (Image image = Image.FromFile(filePath))
            //{
            //    using (MemoryStream m = new MemoryStream())
            //    {
            //        image.Save(m, image.RawFormat);
            //        byte[] imageBytes = m.ToArray();

            //        // Convert byte[] to Base64 String
            //        string base64String = Convert.ToBase64String(imageBytes);
            //        return base64String;
            //    }
            //}
        }
         public static int AddNote(Note ntobj , byte[] imageBytes, string debtorpathname, string notepath)
        {
            if(debtorpathname == "")
            {
                debtorpathname = null;
            }

            int stat = 0;
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,
               ntobj.AGENCY_ID,
               ntobj.Debtor.Debtor_ID,
               ntobj.NOTE,
               ntobj.EMP_ID,
               ntobj.Inserted_At,
               debtorpathname,
               ntobj.Suppress

            };
                DataSet ds = networkRepo.PostDataTable("sp_FourAddNotesbydebtorid", obj);
                if (imageBytes !=null)
                {
                    System.IO.File.WriteAllBytes(notepath, imageBytes);
                }
              

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            stat = item.GetValue<int>("STATUS");
                        }


                      
                        return stat;
                    }

                    else
                    {
                        //throw new Exception("No results found.");
                        return 0;
                    }





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



        public static int UpdateNote(Note ntobj)
        {
          

            int stat = 0;
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,
               ntobj.NOTE_ID,       
               ntobj.NOTE,             
               ntobj.Inserted_At
               

            };
                DataSet ds = networkRepo.PostDataTable("sp_FourUpdateNotebyNoteid", obj);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            stat = item.GetValue<int>("STATUS");
                        }



                        return stat;
                    }

                    else
                    {
                        //throw new Exception("No results found.");
                        return 0;
                    }





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



        #endregion Functions
    }
}
