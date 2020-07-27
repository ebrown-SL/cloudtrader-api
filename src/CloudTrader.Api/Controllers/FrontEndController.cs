using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudTrader.Api.Service.Models;
using CloudTrader.Api.Data;
using CloudTrader.Api.Service.Interfaces;

namespace CloudTrader.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FrontEndController : Controller
    {
        private readonly IUserRepository _userRepository;

        public FrontEndController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("getUser{id}")]
        public async Task<User> GetUser(int id)
        {
            var user = await _userRepository.GetUser(id);
            return user;
        }

        
    }
}
