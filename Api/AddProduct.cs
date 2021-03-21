using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlazorApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Api
{
    public class AddProduct
    {
        string connectionString = Environment.GetEnvironmentVariable("BlobConnectionString");
        private BlobContainerClient containerClient;
        private BlobServiceClient blobServiceClient;
        private string containerName;

        public AddProduct()
        {
            CreateOrGetContainerClientAsync().Wait();
        }

        [FunctionName(nameof(AddProduct))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName:"ProductDb",
                collectionName:"products",
                PartitionKey="/loki30",
                ConnectionStringSetting ="CosmosDbConnectionString")]IAsyncCollector<object> products,
            ILogger log)
        {
            try
            {
                log.LogInformation("Add Product : C# HTTP trigger function processed a request.");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var productItem = JsonConvert.DeserializeObject<Product>(requestBody);

                if (productItem != null)
                {
                     var url = UploadImage(Guid.NewGuid().ToString(), productItem.ImageFiles[0].FileStream).Result;
                     productItem.ImageUrls=new List<string>()
                     {
                         url
                     };
                    await products.AddAsync(productItem);

                    return new OkObjectResult($"Product added \n {productItem}");
                }
            }
            catch (Exception e)
            {
                log.LogInformation("Add Product Error");
            }
            return new BadRequestResult();
        }
        private async Task CreateOrGetContainerClientAsync()
        {
            try
            {
                blobServiceClient = new BlobServiceClient(connectionString);

                containerName = "ProductsImages";

                var isExists = blobServiceClient.GetBlobContainerClient(containerName).Exists();

                if (!isExists.Value)
                    await blobServiceClient.CreateBlobContainerAsync(containerName, PublicAccessType.Blob);

                containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task<string> UploadImage(string name, FileStream stream)
        {
            try
            {
                if (containerClient != null && name !=null && stream !=null)
                {
                    BlobClient blobClient = containerClient.GetBlobClient(name);

                    Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", containerClient.Uri);

                    // using FileStream uploadFileStream = File.OpenRead(name);
                    await blobClient.UploadAsync(stream, true);

                    return blobClient.Uri.AbsoluteUri;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}

