using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace shoppingFucntion.Models
{
    public class Shop
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("img")]
        public string Img { get; set; }

        //public List<Category> Category { get; set; }

        //public class JsonResponse
        //{
        //    public List<Category> categories { get; set; }
        //}

        //public class Constant
        //{
        //    [JsonProperty("catId")]
        //    public string catId { get; set; }

        //    [JsonProperty("catName")]
        //    public string catName { get; set; }

        //    [JsonProperty("catImg")]
        //    public string catImg { get; set; }
        //}

    }
}
