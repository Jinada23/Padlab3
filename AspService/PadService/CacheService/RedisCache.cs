using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PadService.Models;
using PadService.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PadService.CacheService
{
    public class RedisCache
    {
        private readonly IDistributedCache _cache;
        private readonly MongoRepository _userRepository;

        public RedisCache(IDistributedCache cache, MongoRepository mongoRepository)
        {
            _userRepository = mongoRepository;
            _cache = cache;
        }
        public async Task<List<UserDTO>> getAllUsersAsync()
        {
            var cacheKey = "GET_ALL_USERS";
            List<UserDTO> users = new List<UserDTO>();

            // Get data from cache
            var cachedData = await _cache.GetAsync(cacheKey);
            if (cachedData != null)
            {
                // If data found in cache, encode and deserialize cached data
                var cachedDataString = Encoding.UTF8.GetString(cachedData);
                users = JsonConvert.DeserializeObject<List<UserDTO>>(cachedDataString);
            }
            else
            {
                // If not found, then fetch data from database
                users = _userRepository.GetAll();

                // serialize data
                var cachedDataString = JsonConvert.SerializeObject(users);
                var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                // set cache options 
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(20))
                    .SetSlidingExpiration(TimeSpan.FromSeconds(20));

                // Add data in cache
                await _cache.SetAsync(cacheKey, newDataToCache, options);
            }

            return users;

        }
    }
}
