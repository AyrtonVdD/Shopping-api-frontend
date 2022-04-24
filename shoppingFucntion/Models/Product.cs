using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace frituurFunctions.Models
{
    public class Product
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public Category Category { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }        

        [JsonProperty("count")]
        public int Count { get; set; }



    }
}
