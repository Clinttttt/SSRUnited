using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SSRUnited.Shared.Dtos
{
    public class HumanDto
    {
        [JsonIgnore]
        public int id { get; set; }
        [JsonIgnore]
        public DateTime created_at { get; set; }
        public string? name { get; set; }
        public string? content { get; set; }

      
    }
}
