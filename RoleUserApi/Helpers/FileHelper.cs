using RoleUserApi.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Helpers
{
    public class FileHelper
    {
        public static IFormFile ToReturnFormFile(FileStream result)
        {
            var ms = new MemoryStream();
            try
            {
                result.CopyTo(ms);
                return new FormFile(ms, 0, ms.Length, "pdf" + DateTime.Now.Ticks, ".pdf");
            }
            finally
            {
                // ms.Dispose();
            }
        }

        public async static Task<string> UploadPdfAsync(string doc, string FolderPath)
        {
            try
            {
                string TempPath = SavePdf(doc);

                FileStream fileRead = new FileStream(TempPath, FileMode.Open);

                IFormFile file = ToReturnFormFile(fileRead);
                if (true)
                {
                    //  string FolderPath = "TournmentImages/";

                    var FileName = await new FileWriter().UploadPdf(file, FolderPath);
                    if (FileName != null)
                    {
                        string docURL = Constants.BlobUrl + FolderPath + FileName;
                        fileRead.Close();
                        //fileRead.Dispose();
                        //  fileRead.
                        //  fileRead.Flush();
                        //System.IO.File.Delete(@"C:\test.txt");
                        File.Delete(TempPath);

                        //  fileRead.Dispose();

                        return docURL;
                        //model.ImgThumbURL = "https://salaciallcdiag.blob.core.windows.net/chipsstoragecontainer/" + PictureName + "?width300";
                        //model.ImgOriginalOrientation = "";
                        //model.ImgOriginalWidth = "";
                    }

                    return null;
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string SavePdf(string BaseFile)
        {
            try
            {
                var bytes = Convert.FromBase64String(BaseFile);
                string filedir = Path.Combine(Directory.GetCurrentDirectory(), "Documents");
                //Debug.WriteLine(filedir);
                //Debug.WriteLine(Directory.Exists(filedir));
                if (!Directory.Exists(filedir))
                {
                    Directory.CreateDirectory(filedir);
                }
                string FilePath = Path.Combine(filedir, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString() + ".pdf");
                //Debug.WriteLine(file);
                //Debug.WriteLine(File.Exists(file));



                // File.WriteAllBytes(FilePath, bytes);
                if (bytes.Length > 0)
                {
                    using (var stream = new FileStream(FilePath, FileMode.Create))
                    {
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Flush();
                    }
                }

                return FilePath;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
