using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSRUnited.Shared.Entity
{
    public class Human
    {
        public int Id { get; set; }
        public string? name { get; set; }
        public string? content { get; set; }
        public DateTime created_at { get; set; }
    }
}
