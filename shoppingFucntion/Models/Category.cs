using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace shoppingFucntion.Models
{
    public class Category
    {
        [JsonProperty("catId")]
        public string CatId { get; set; }

        [JsonProperty("catName")]
        public string CatName { get; set; }

        [JsonProperty("catImg")]
        public string CatImg { get; set; }

        [JsonProperty("producten")]
        public Producten producten { get; set; }

        //public class JsonResponseProducten
        //{
        //    public List<Constant> producten { get; set; }
        //}

        //public class Constant
        //{
        //    [JsonProperty("prodId")]
        //    public string prodId { get; set; }

        //    [JsonProperty("prodName")]
        //    public string prodName { get; set; }

        //    [JsonProperty("prodImg")]
        //    public string prodImg { get; set; }
        //}
    }
}
