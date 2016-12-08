//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Auth;
//using Microsoft.WindowsAzure.Storage.Blob;
//using System.IO;

//namespace proje_obs
//{
//    public static class _Storage
//    {
//        static string accountName;
//        static string accountKey;
//        static CloudBlobContainer sampleContainer;

//        static _Storage()
//        {
//            accountName = "obsprojectgroup3";
//            accountKey = "yhSjJTOgo734wcNcgAsMRCHA3VFzHJnpXV4no4SBZo8gsb8bnUBDDJOXrtWdKI4Smsepccq80kAkApnnnHr44A==";
//            StorageCredentials creds = new StorageCredentials(accountName, accountKey);
//            CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);

//            CloudBlobClient client = account.CreateCloudBlobClient();

//            sampleContainer = client.GetContainerReference("samples");
//            sampleContainer.CreateIfNotExists();
//        }

//        public static bool CheckExists(String filename)
//        {
//            CloudBlockBlob blob = sampleContainer.GetBlockBlobReference(filename);
//            return blob.Exists();
//        }

//        public static String ReturnUrl(String filename)
//        {
//            CloudBlockBlob blob = sampleContainer.GetBlockBlobReference(filename);
//            return blob.Uri.AbsoluteUri;
//        }

//        public static bool UploadFile(HttpPostedFileBase file, String filename)
//        {
//            try
//            {
//                CloudBlockBlob blob = sampleContainer.GetBlockBlobReference(filename);
                
//                blob.UploadFromStream(file.InputStream);

//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex);
//                return false;
//            }
//            return true;
//        }
//    }
//}