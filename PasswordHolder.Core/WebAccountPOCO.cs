using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PasswordHolder.Core
{
    public class WebAccountPOCO
    {
        [JsonPropertyName("siteName")]
        public string SiteName { get; set; }
        [JsonPropertyName("idAndPass")]
        public Dictionary<string, string> IdAndPass { get; set; }
    }
}
