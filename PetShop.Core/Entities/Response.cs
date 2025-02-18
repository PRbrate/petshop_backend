using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PetShop.Core.Entities
{
    public class Response<T>
    {
        public bool Success { get; set; } = true;
        public T Data { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Errors { get; set; }
        public ExpandoObject? ExternalErrors{ get; set; }
    }
}
