using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace api.Models
{
    public class Result
    {
        [JsonPropertyName("output")]
        public string Output { get; set; }
    }
}
