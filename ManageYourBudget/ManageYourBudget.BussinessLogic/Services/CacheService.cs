using System;
using System.Collections.Generic;
using System.Net;
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
