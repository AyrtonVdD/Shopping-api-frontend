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
using shoppingFucntion.Models;
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

                var json = await new StreamReader(req.Body).ReadToEndAsync();
                allShops = JsonConvert.DeserializeObject<List<Shop>>(json);

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

        [FunctionName("GetAllCategorieen")]
        public static async Task<IActionResult> GetAllCategorieen(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "categorieen")] HttpRequest req,
            ILogger log)
        {
            try
            {
                string connectionString = Environment.GetEnvironmentVariable("CosmosConnectionString");
                CosmosClient cosmosClient = new CosmosClient(connectionString);
                Database database = cosmosClient.GetDatabase("Shopping-list");
                Container container = database.GetContainer("Shops");

                List<Category> allCategorys = new List<Category>();

                //string requestBody = new StreamReader(req.Body).ReadToEndAsync();
                //JsonResponseProducten data = JsonConvert.DeserializeObject<JsonResponseProducten>(requestBody);
                //var Root = data.ProdId;

                QueryDefinition query = new QueryDefinition("SELECT * FROM c.category");
                FeedIterator<Category> iterator = container.GetItemQueryIterator<Category>(query);
                while (iterator.HasMoreResults)
                {
                    FeedResponse<Category> response = await iterator.ReadNextAsync();
                    allCategorys.AddRange(response);
                }

                return new OkObjectResult(allCategorys);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [FunctionName("GetAllProdcuten")]
        public static async Task<IActionResult> GetAllProducten(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "producten")] HttpRequest req,
        ILogger log)
        {
            try
            {
                string connectionString = Environment.GetEnvironmentVariable("CosmosConnectionString");
                CosmosClient cosmosClient = new CosmosClient(connectionString);
                Database database = cosmosClient.GetDatabase("Shopping-list");
                Container container = database.GetContainer("Shops");

                List<Producten> allProducs = new List<Producten>();


                QueryDefinition query = new QueryDefinition("SELECT c.producten FROM c in p.category");
                FeedIterator<Producten> iterator = container.GetItemQueryIterator<Producten>(query);
                while (iterator.HasMoreResults)
                {
                    FeedResponse<Producten> response = await iterator.ReadNextAsync();
                    allProducs.AddRange(response);
                }

                return new OkObjectResult(allProducs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [FunctionName("GetAllCategorieenByShopId")]
        public static async Task<IActionResult> GetAllCategorienByShopId(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "catergory/{shopId}")] HttpRequest req,
        string shopId,
        ILogger log)
        {
            try
            {
                string connectionString = Environment.GetEnvironmentVariable("CosmosConnectionString");
                CosmosClient cosmosClient = new CosmosClient(connectionString);
                Database database = cosmosClient.GetDatabase("Shopping-list");
                Container container = database.GetContainer("Shops");

                List<Category> allCategorienByShopId = new List<Category>();

                QueryDefinition query = new QueryDefinition("SELECT c.name, c.category FROM c where c.id = @shopId");
                query.WithParameter("@shopId", shopId);
                FeedIterator<Category> iterator = container.GetItemQueryIterator<Category>(query);

                while (iterator.HasMoreResults)
                {
                    FeedResponse<Category> response = await iterator.ReadNextAsync();
                    allCategorienByShopId.AddRange(response);
                }

                return new OkObjectResult(allCategorienByShopId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        

    }

    //[FunctionName("GetAllCategories")]
    //public static async Task<IActionResult> GetAllCategories(
    //[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Categories")] HttpRequest req,
    //ILogger log)
    //{
    //    try
    //    {
    //        string connectionString = Environment.GetEnvironmentVariable("CosmosConnectionString");
    //        CosmosClient cosmosClient = new CosmosClient(connectionString);
    //        Database database = cosmosClient.GetDatabase("Shopping-list");
    //        Container container = database.GetContainer("Shops");

    //        List<Shop> allShops = new List<Shop>();

    //        QueryDefinition query = new QueryDefinition("SELECT * FROM Categorieen");
    //        FeedIterator<Shop> iterator = container.GetItemQueryIterator<Shop>(query);
    //        while (iterator.HasMoreResults)
    //        {
    //            FeedResponse<Shop> response = await iterator.ReadNextAsync();
    //            allShops.AddRange(response);
    //        }

    //        return new OkObjectResult(allShops);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

}