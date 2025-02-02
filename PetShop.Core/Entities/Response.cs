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
        public bool Success { get; set; }
        public T Data { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ExpandoObject? Errors { get; set; }
    }
}
