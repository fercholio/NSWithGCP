using System;
using System.IO;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace WebAPI.EndRequest.Services
{
    public class SaveFile
    {
        private readonly string _bucketName;        

        public SaveFile(string bucketName)
        {
            _bucketName = bucketName;
        }

        // [START uploadimage]
        public String UploadFile(FileStream file, long id)
        {
            try
            {
                var credential = GoogleCredential.GetApplicationDefault();
                // Instantiates a client.
                StorageClient storage = StorageClient.Create(credential);

                var MediaLink = storage.UploadObject(_bucketName, id.ToString(), "application/pdf", file,new UploadObjectOptions { PredefinedAcl = PredefinedObjectAcl.PublicRead }).MediaLink;
                Console.WriteLine($"Uploaded {id.ToString()}.");                
                //NICE TO HAVE
                //Add Loggin to UploadService

                return MediaLink;
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
        // [END uploadimage]

        //public async Task DeleteUploadedImage(long id)
        //{
        //    try
        //    {
        //        await _storageClient.DeleteObjectAsync(_bucketName, id.ToString());
        //    }
        //    catch (Google.GoogleApiException exception)
        //    {
        //        // A 404 error is ok.  The image is not stored in cloud storage.
        //        if (exception.Error.Code != 404)
        //            throw;
        //    }
        //}
    }
}
