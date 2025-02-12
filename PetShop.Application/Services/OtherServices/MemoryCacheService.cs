using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Services.OtherServices
{
    public class MemoryCacheService
    {

        private readonly IMemoryCache _cache;
        private readonly TimeSpan _TimeExpire = TimeSpan.FromMinutes(5);

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public void StoryCode(string email, string code)
        {
            _cache.Set(email, code, _TimeExpire);
        }

        public string GetCode(string email)
        {
            _cache.TryGetValue(email, out string code);
            return code;
        }

        public void RemoveCode(string email)
        {
            _cache.Remove(email);
        }
    }
}
