using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace shoppingFucntion.Models
{
    public class Producten
    {
        [JsonProperty("prodId")]
        public int Id { get; set; }
        [JsonProperty("prodName")]
        public string Name { get; set; }
        [JsonProperty("prodImg")]
        public string Img { get; set; }
        
        [JsonProperty("count")]
        public int Count { get; set; }


    }
}
