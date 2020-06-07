using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Common.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace ManageYourBudget.BussinessLogic.Services
{
    public class CacheService: ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetState(string ip, string state)
        {
            await Set(CreateExternalProviderKey(ip), state);
        }

        public async Task<string> GetState(string ip)
        {
            var key = CreateExternalProviderKey(ip);
            var state = await Get(key);
            await _cache.RemoveAsync(key);
            return state;
        }

        public async Task<T> Get<T>(string key)
        {
            var entry = await _cache.GetAsync(key);
            if (entry == null || entry.Length == 0)
            {
                return default;
            }
            using (var ms = new MemoryStream(entry))
            {
                IFormatter br = new BinaryFormatter();
                return (T)br.Deserialize(ms);
            }
        }

        public async Task Set<T>(string key, T value, TimeSpan expirationTime = default)
        {
            if (expirationTime == default)
            {
                expirationTime = TimeSpan.FromHours(1);
            }

            using (var stream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, value);
                var cacheEntryOptions = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(expirationTime);
                await _cache.SetAsync(key, stream.ToArray(), cacheEntryOptions);
            }
        }

        private async Task Set(string key, string entry)
        {
            await _cache.SetAsync(key, entry.ToByteArray());
        }

        private async Task<string> Get(string key)
        {
            var entry = (await _cache.GetAsync(key))?.ToUncodedString();
            return entry;
        }

        private string CreateExternalProviderKey(string ip)
        {
            return $"{ip}-state";
        }
    }
}
