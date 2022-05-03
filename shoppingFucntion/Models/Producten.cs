using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace shoppingFucntion.Models
{
    public class Producten
    {
        [JsonProperty("prodId")]
        public string ProdId { get; set; }
        [JsonProperty("prodName")]
        public string ProdName { get; set; }
        [JsonProperty("prodImg")]
        public string ProdImg { get; set; }
        
        [JsonProperty("count")]
        public int Count { get; set; }


    }
}
