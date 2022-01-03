using RoleUserApi.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Threading.Tasks;

namespace RoleUserApi.Helpers
{
    public class ImageWriter
    {
        public async Task<string> UploadImage(IFormFile file, string FolderPath)
        {
            if (CheckIfImageFile(file))
            {
                return await WriteFile(file, FolderPath);
            }

            return null;
        }

        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            return WriterHelper.GetImageFormat(fileBytes) != WriterHelper.ImageFormat.unknown;
        }


        //public async Task<string> WriteFile(IFormFile file)
        //{
        //    string fileName;

        //    try
        //    {

        //        var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
        //        fileName = DateTime.Now.ToFileTime().ToString() + extension;               
        //        AzureUpload(id.ToString(), file, fileName);
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }

        //    return fileName;
        //}

        public async Task<string> WriteFile(IFormFile file, string FolderPath)
        {
            string fileName;

            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.ToFileTime().ToString() + extension;
                AzureUpload(FolderPath, file, fileName);
            }
            catch (Exception e)
            {
                return null;
            }

            return fileName;
        }

        public static bool AzureUpload(string Folder, IFormFile file, string Name)
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

                CloudBlockBlob cblob = cont.GetBlockBlobReference(Folder + Name);



                cblob.UploadFromStream(file.OpenReadStream());
                // cblob.UploadFromFile("");


            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
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
