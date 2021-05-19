using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace api.Models
{
    public class CompilerInput
    {
        [JsonPropertyName("clientId")]
        public string ClientId { get; set; }

        [JsonPropertyName("clientSecret")]
        public string ClientSecret { get; set; }

        [JsonPropertyName("script")]
        public string Script { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("versionIndex")]
        public string VersionIndex { get; set; }

        public CompilerInput(string clientId, string clientSecret, string script,
                            string language, string versionIndex)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            Script = script;
            Language = language;
            VersionIndex = versionIndex;
        }
    }
}
