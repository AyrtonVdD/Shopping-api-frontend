using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace frituurFunctions.Models
{
    public class Category
    {
        [JsonProperty("catId")]
        public string Id { get; set; }

        [JsonProperty("catName")]
        public string Name { get; set; }

    }
}
