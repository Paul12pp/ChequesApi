using ChequesApi.Models;
using ChequesApi.Models.Entities;
using ChequesApi.ViewModels.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ChequesApi.Repositories
{
    public class JWTManagerRepository : Repository<User>, IJWTManagerRepository
    {
        private readonly IConfiguration configuration;
        Dictionary<string, string> UserRecords = new Dictionary<string, string>
        {
            {"user1", "password1" },
            {"user2", "password2" },
            {"user3", "password3" },
            {"user4", "password4" },
        };
        public JWTManagerRepository(IConfiguration configuration, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.configuration = configuration;

        }
        public Tokens Authenticate(UserViewModel user)
        {
            var users = this.FindAll();
            if (!users.Any(x => x.UserName == user.UserName && x.Password == user.Password))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[]
                {
                     new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptior);
            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }
    }
}
