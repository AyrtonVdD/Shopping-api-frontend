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
using System.Data.SqlClient;

namespace shoppingFucntion
{
    public class ReceiveProductsInfo
    {

        private static string CONNECTIONSTRING = Environment.GetEnvironmentVariable("Connectionstring");

        [FunctionName("GetShops")]
        public async Task<IActionResult> GetShops(
                [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/Shops")] HttpRequest req,
                ILogger log)
        {
            try
            {
                List<Shop> shops = new List<Shop>();

                using (SqlConnection sqlConnection = new SqlConnection(CONNECTIONSTRING))
                {
                    await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "SELECT * FROM Shops";

                        var reader = await sqlCommand.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            Shop shop = new Shop();

                            shop.Id = int.Parse(reader["Id"].ToString());
                            shop.Name = reader["Name"].ToString();
                            shop.Img = reader["Img"].ToString();

                            shops.Add(shop);
                        }
                    }
                }
                return new OkObjectResult(shops);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
        }


        [FunctionName("GetCategories")]
        public async Task<IActionResult> GetCategoyByShopId(
                [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/Categories/{shopId}")] HttpRequest req,
                int shopId, 
                ILogger log)
        {
            try
            {
                List<Category> categories = new List<Category>();

                using (SqlConnection sqlConnection = new SqlConnection(CONNECTIONSTRING))
                {
                    await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "SELECT * FROM Shop_category SC INNER JOIN Shops S ON S.id = SC.shopId INNER JOIN Categorys C ON SC.categoryId = C.catId WHERE S.id = @shopId";
                        sqlCommand.Parameters.AddWithValue("@shopId", shopId);

                        var reader = await sqlCommand.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            Category category = new Category();

                            category.Id = int.Parse(reader["catId"].ToString());
                            category.Name = reader["catName"].ToString();
                            category.Img = reader["catImg"].ToString();

                            categories.Add(category);
                        }
                    }
                }
                return new OkObjectResult(categories);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
        }

        [FunctionName("GetProducten")]
        public async Task<IActionResult> GetProductsByCategoryId(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/Products/{catId}")] HttpRequest req,
        int catId,
        ILogger log)
        {
            try
            {
                List<Producten> producten = new List<Producten>();

                using (SqlConnection sqlConnection = new SqlConnection(CONNECTIONSTRING))
                {
                    await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "SELECT * FROM Category_product CP INNER JOIN Categorys C ON C.catId = CP.categoryId INNER JOIN Producten P ON CP.productId = P.prodId WHERE C.catId = @catId";
                        sqlCommand.Parameters.AddWithValue("@catId", catId);

                        var reader = await sqlCommand.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            Producten product = new Producten();

                            product.Id = int.Parse(reader["prodId"].ToString());
                            product.Name = reader["prodName"].ToString();
                            product.Img = reader["prodImg"].ToString();
                            product.Count = int.Parse(reader["Count"].ToString());

                            producten.Add(product);
                        }
                    }
                }
                return new OkObjectResult(producten);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
        }

    }

}