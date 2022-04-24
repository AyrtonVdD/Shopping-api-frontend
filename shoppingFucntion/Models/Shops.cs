using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace frituurFunctions.Models
{
    public class Shop
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

    }
}
