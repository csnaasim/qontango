using RoleUserApi.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Helpers
{
    public class ImageHelper
    {
        public static IFormFile ToReturnFormFile(FileStream result)
        {
            var ms = new MemoryStream();
            try
            {
                result.CopyTo(ms);
                return new FormFile(ms, 0, ms.Length, "cntPic"+DateTime.Now.Ticks, ".jpeg");
            }
            finally
            {
                // ms.Dispose();
            }
        }
        
        public static string SaveImage(string BaseFile)
        {
            try
            {

                var bytes = Convert.FromBase64String(BaseFile);
                string filedir = Path.Combine(Directory.GetCurrentDirectory(), "Pictures");
                //Debug.WriteLine(filedir);
                //Debug.WriteLine(Directory.Exists(filedir));
                if (!Directory.Exists(filedir))
                {
                    Directory.CreateDirectory(filedir);
                }
                string FilePath = Path.Combine(filedir, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString() + ".jpeg");
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

        public void CompressImage(string inputPath)
        {
            const int size = 500;
            const long quality = 50L;


            string outputPath = $@"D:\Images\land_{quality}.jpg";

            using (var image = new Bitmap(System.Drawing.Image.FromFile(inputPath)))
            {
                var resized = new Bitmap(size, size);
                using (var graphics = Graphics.FromImage(resized))
                {
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.DrawImage(image, 0, 0, size, size);
                    using (var output = File.Open(outputPath, FileMode.Create))
                    {
                        var qualityParamId = Encoder.Quality;
                        var encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                        var codec = ImageCodecInfo.GetImageDecoders()
                            .FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);
                        resized.Save(output, codec, encoderParameters);
                    }
                }
            }
        }
        public async static Task<string> UploadImageAsync(string Image, string FolderPath)
        {
            try
            {
                string TempPath = ImageHelper.SaveImage(Image);

                FileStream fileRead = new FileStream(TempPath, FileMode.Open);


                //var resized = new Bitmap(width, height);
                //using (var graphics = Graphics.FromImage(resized))
                //{
                //    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                //    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //    graphics.CompositingMode = CompositingMode.SourceCopy;
                //    graphics.DrawImage(image, 0, 0, width, height);
                //    using (var output = File.Open(
                //        OutputPath(path, outputDirectory, SystemDrawing), FileMode.Create))
                //    {
                //        var qualityParamId = Encoder.Quality;
                //        var encoderParameters = new EncoderParameters(1);
                //        encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                //        var codec = ImageCodecInfo.GetImageDecoders()
                //            .FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
                //        resized.Save(output, codec, encoderParameters);
                //    }
                //}


                IFormFile file = ImageHelper.ToReturnFormFile(fileRead);



                //var optimizer = new ImageOptimizer();
                //optimizer.LosslessCompress(Test.FileName);

                //model.ImgOriginalSize = Convert.ToInt32((Test.Length) / 1000);
                /*Azure Fire Works Start*/
                //int UserId = 12;
                //var IUser = ServiceFactory<IUser>.GetService(typeof(User));
                //var UserDetail = IUser.GetUserStats(UserId);
                if (true)
                {
                    //  string FolderPath = "TournmentImages/";

                    var PictureName = await new ImageWriter().UploadImage(file, FolderPath);
                    if (PictureName != null)
                    {
                        string ImageURL = Constants.BlobUrl + FolderPath + PictureName;
                        fileRead.Close();
                        //fileRead.Dispose();
                        //  fileRead.
                        //  fileRead.Flush();
                        //System.IO.File.Delete(@"C:\test.txt");
                        File.Delete(TempPath);

                        //  fileRead.Dispose();

                        return ImageURL;
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
                /*Azure Fire Works End*/
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool AzureDelete(string Name)
        {
            try
            {

                StorageCredentials creden = new StorageCredentials(Constants.Accountname, Constants.Accesskey);

                CloudStorageAccount acc = new CloudStorageAccount(creden, useHttps: true);

                CloudBlobClient client = acc.CreateCloudBlobClient();

                CloudBlobContainer cont = client.GetContainerReference(Constants.Containername);

                cont.CreateIfNotExists();


                cont.SetPermissions(new BlobContainerPermissions
                {

                    PublicAccess = BlobContainerPublicAccessType.Blob


                });

                CloudBlockBlob cblob = cont.GetBlockBlobReference(Name);



                cblob.DeleteIfExists();
                // cblob.UploadFromFile("");


            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
