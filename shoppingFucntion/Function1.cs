using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;
using frituurFunctions.Models;
using System.Collections.Generic;

namespace shoppingFucntion
{
    public static class ReceiveProductsInfo
    {
        [FunctionName("GetAllShops")]
        public static async Task<IActionResult> GetAllShops(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "shops")] HttpRequest req,
            ILogger log)
        {
            try
            {
                string connectionString = Environment.GetEnvironmentVariable("CosmosConnectionString");
                CosmosClient cosmosClient = new CosmosClient(connectionString);
                Database database = cosmosClient.GetDatabase("Shopping-list");
                Container container = database.GetContainer("Shops");

                List<Shop> allShops = new List<Shop>();

                QueryDefinition query = new QueryDefinition("SELECT * FROM Shops");
                FeedIterator<Shop> iterator = container.GetItemQueryIterator<Shop>(query);
                while (iterator.HasMoreResults)
                {
                    FeedResponse<Shop> response = await iterator.ReadNextAsync();
                    allShops.AddRange(response);
                }

                return new OkObjectResult(allShops);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}