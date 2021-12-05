using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PadService.Models;
using PadService.CacheService;
using PadService.Models.Helpers;
using PadService.Repositories;

namespace PadService.Controllers
{
   [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly RedisCache _cache;
        private readonly MongoRepository _userRepository;
        public UserController(ILogger<UserController> logger, RedisCache cache, MongoRepository mongoRepository)
        {
            _logger = logger;
            _cache = cache;
            _userRepository = mongoRepository;
        }

        [HttpGet]
        [Route("/all/json")]
        public async Task<IEnumerable<UserDTO>> GetJson()
        {
            var users = await _cache.getAllUsersAsync();
            return users;
        }

        [HttpGet]
        [Route("/all/xml")]
        public async Task<string> GetXml()
        {
            var users = await _cache.getAllUsersAsync();

            var usersString = users.ToXml();

            return usersString;
        }

        [HttpPost]
        [Route("/register")]
        public string Register(UserDTO user)
        {
            var response = _userRepository.Insert(user);

            if (response != null)
            {
                return $"User was succesfully registed!\n{response.Id}";
            }
            return "Something went wrong";
        }

        [HttpGet]
        [Route("{id}")]
        public UserDTO Get(string id)
        {
            var user = _userRepository.GetById(Guid.Parse(id));

            return user;
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(string id)
        {
            _userRepository.Delete(Guid.Parse(id));
        }
    }
}
