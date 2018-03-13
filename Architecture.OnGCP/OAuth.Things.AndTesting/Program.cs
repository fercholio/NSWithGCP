using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System;
using System.Diagnostics;
using System.IO;

namespace OAuth.Things.AndTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectId = "utility-vista-196902";

            // If you don't specify credentials when constructing the client, the
            // client library will look for credentials in the environment.
            //var jsonCredentials = System.IO.File.ReadAllText(@".\client_secret_.json");
            var credential = GoogleCredential.GetApplicationDefault();
            // Instantiates a client.
            StorageClient storageClient = StorageClient.Create(credential);

            // The name for the new bucket.
            string bucketName = projectId + "-test-bucket";
            try
            {
                // Creates the new bucket.
                storageClient.UploadObject(bucketName, "name.txt", "application/text", File.OpenRead(@".\client_secret_.json"), null);
                Console.WriteLine($"Bucket {bucketName} created.");
            }
            catch (Google.GoogleApiException e)
            when (e.Error.Code == 409)
            {
                // The bucket already exists.  That's fine.
                Console.WriteLine(e.Error.Message);
            }
        }
    }
}
