using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AzureCosmosPlayAreaDocumentDb.Models
{
    public class Course
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("modules")]
        public string Modules { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }
    }
}