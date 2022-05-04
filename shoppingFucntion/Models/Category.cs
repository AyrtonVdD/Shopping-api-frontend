using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace shoppingFucntion.Models
{
    public class Category
    {
        [JsonProperty("catId")]
        public int Id { get; set; }

        [JsonProperty("catName")]
        public string Name { get; set; }

        [JsonProperty("catImg")]
        public string Img { get; set; }

    }
}
