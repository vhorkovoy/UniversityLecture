using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UniversityLecture.Core;
using UniversityLecture.Repo.Interfaces;
using UniversityLecture.WEB.Interfaces;
using UniversityLecture.WEB.Helpers;
using Microsoft.Extensions.Options;

namespace UniversityLecture.WEB.Services
{
    public class AuthenticateService : IAuthenticate
    {
        public AuthenticateService(IRepository repo, IOptions<AppSettings> appSettings)
        {
            _AppSettings = appSettings.Value;
            _Repo = repo;
        }
        private readonly IRepository _Repo;
        private readonly AppSettings _AppSettings;

        public string GetToken(string login, string pwd)
        {
            var user = _Repo.GetAll<User>().SingleOrDefault(u => u.Login == login && u.Password == pwd);
            if (user == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_AppSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.ID.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
