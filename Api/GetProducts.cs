using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api
{
    public static class GetProducts
    {
        [FunctionName(nameof(GetProducts))]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest request,
            [CosmosDB(
            databaseName: "ProductDb",
            collectionName: "products",
            PartitionKey = "/loki30",
            ConnectionStringSetting ="CosmosDbConnectionString",
            SqlQuery="SELECT * FROM c")]IEnumerable<dynamic> products, ILogger log)
        {
            if (products ==null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(products);
        }
    }
}
