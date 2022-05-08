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

        [FunctionName("GetShoppingCart")]
        public async Task<IActionResult> GetProductByProdId(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/ShoppingCart")] HttpRequest req,
        ILogger log)
        {
            try
            {
                List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();

                using (SqlConnection sqlConnection = new SqlConnection(CONNECTIONSTRING))
                {
                    await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "SELECT * FROM ShoppingCart";

                        var reader = await sqlCommand.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            ShoppingCart shoppingCart = new ShoppingCart();

                            shoppingCart.ShopCartId = int.Parse(reader["shopCartId"].ToString());
                            shoppingCart.ShopId = int.Parse(reader["shopId"].ToString());
                            shoppingCart.ProdId = int.Parse(reader["prodId"].ToString());
                            shoppingCart.Count = int.Parse(reader["Count"].ToString());
                            shoppingCart.ShopName = reader["shopName"].ToString();
                            shoppingCart.ProdName = reader["prodName"].ToString();
                            shoppingCart.ProdImg =reader["prodImg"].ToString();

                            shoppingCarts.Add(shoppingCart);
                        }
                    }
                }
                return new OkObjectResult(shoppingCarts);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
        }

        [FunctionName("GetProduct")]
        public async Task<IActionResult> GetProductInCartByProdId(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/Product/{prodId}")] HttpRequest req,
        int prodId,
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
                        sqlCommand.CommandText = "SELECT * FROM Producten WHERE prodId= @prodId";
                        sqlCommand.Parameters.AddWithValue("@prodId", prodId);

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

        [FunctionName("GetShopName")]
        public async Task<IActionResult> GetShopInCartByShopId(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/Shop/{shopId}")] HttpRequest req,
        int shopId,
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
                        sqlCommand.CommandText = "SELECT name FROM Shops WHERE id= @shopId";
                        sqlCommand.Parameters.AddWithValue("@shopId", shopId);

                        var reader = await sqlCommand.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            Shop shop = new Shop();
                            shop.Name = reader["name"].ToString();

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



        [FunctionName("PostProduct")]
        public async Task<IActionResult> PostCards(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/Shoppingcart")] HttpRequest req,
        ILogger log)
        {
            try
            {
                var json = await new StreamReader(req.Body).ReadToEndAsync();
                var shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(json);
                //var shop = JsonConvert.DeserializeObject<Shop>(json);

                using (SqlConnection sqlConnection = new SqlConnection(CONNECTIONSTRING))

                {
                    await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "INSERT INTO ShoppingCart VALUES(@shopId,@prodId,@count,@shopName,@prodName,@prodImg)";
                        sqlCommand.Parameters.AddWithValue("@shopId", shoppingCart.ShopId);
                        sqlCommand.Parameters.AddWithValue("@prodId", shoppingCart.ProdId);
                        sqlCommand.Parameters.AddWithValue("@count", shoppingCart.Count);
                        sqlCommand.Parameters.AddWithValue("@shopName", shoppingCart.ShopName);
                        sqlCommand.Parameters.AddWithValue("@prodName", shoppingCart.ProdName);
                        sqlCommand.Parameters.AddWithValue("@prodImg", shoppingCart.ProdImg);

                        await sqlCommand.ExecuteNonQueryAsync();
                    }
                }
                return new OkObjectResult(shoppingCart);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
        }

        [FunctionName("DelProdShoppingCart")]
        public async Task<IActionResult> DelRegistration(
        [HttpTrigger(AuthorizationLevel.Anonymous, "DELETE", Route = "v1/delProduct/{shopCartId}")] HttpRequest req,
        int shopCartId,
        ILogger log)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(CONNECTIONSTRING))
                {
                    await sqlConnection.OpenAsync();
                    //await sqlCommand.ExceteNonQuerryAsync()
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "DELETE FROM ShoppingCart WHERE shopCartId=" + shopCartId;

                        sqlCommand.ExecuteNonQuery();
                    }

                }
                
                return new OkObjectResult(shopCartId + " is gedeleted");
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
        }

        [FunctionName("UpdateCountOfShopingCart")]
        public static async Task<IActionResult> UpdateAdmin(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "updateShoppingCardItem/{id}")] HttpRequest req,
        int id,
        ILogger log)
        {
            try
            {
                var json = await new StreamReader(req.Body).ReadToEndAsync();
                var shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(json);

                using (SqlConnection sqlConnection = new SqlConnection(CONNECTIONSTRING))
                {
                    await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = $"UPDATE ShoppingCart SET [count] = @count WHERE shopCartId = @id";
                        sqlCommand.Parameters.AddWithValue("@id", id);
                        sqlCommand.Parameters.AddWithValue("@count", shoppingCart.Count);

                        await sqlCommand.ExecuteNonQueryAsync();
                    }
                }

                return new OkObjectResult(shoppingCart);

            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
        }

    }

}