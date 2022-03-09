using ChequesApi.Repositories;
using ChequesApi.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChequesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IJWTManagerRepository jWTManagerRepository;
        public UserController(IJWTManagerRepository jWTManagerRepository)
        {
            this.jWTManagerRepository = jWTManagerRepository;
        }
        [HttpGet]
        [Route("userlist")]
        public List<string> Get()
        {
            var users = new List<string>
            {
                "Paul",
                "Raul",
                "Angelica",
              };
            return users;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(UserViewModel user)
        {
            var token = jWTManagerRepository.Authenticate(user);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
