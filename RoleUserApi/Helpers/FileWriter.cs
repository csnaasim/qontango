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
    public class FileWriter
    {
        public async Task<string> UploadPdf(IFormFile file, string FolderPath)
        {
            if (CheckIfPdfFile(file))
            {
                return await WriteFile(file, FolderPath);
            }

            return null;
        }

        public static bool CheckIfPdfFile(IFormFile file)
        {
            //try
            //{
            //    byte[] filebytes;
            //    using (var ms = new MemoryStream())
            //    {
            //        file.CopyTo(ms);
            //        filebytes = ms.ToArray();
            //    }

            //}
            //catch (Exception ex)
            //{

            //}
            if (file.FileName.Contains(".pdf"))
            {
                return true;
            }
            return false;
        }

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
