using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace shoppingFucntion.Models
{
    public class ShoppingCart
    {
        [JsonProperty("shopCartId")]
        public int ShopCartId { get; set; }

        [JsonProperty("shopId")]
        public int ShopId { get; set; }

        [JsonProperty("prodId")]
        public int ProdId { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("shopName")]
        public string ShopName { get; set; }

        [JsonProperty("prodName")]
        public string ProdName { get; set; }

        [JsonProperty("prodImg")]
        public string ProdImg { get; set; }

    }
}
