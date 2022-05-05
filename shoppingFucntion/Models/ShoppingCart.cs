using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace shoppingFucntion.Models
{
    public class ShoppingCart
    {
        [JsonProperty("shoppingCartId")]
        public int ShoppingCartId { get; set; }

        [JsonProperty("shopId")]
        public int ShopId { get; set; }

        [JsonProperty("prodId")]
        public int ProdId { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

    }
}
