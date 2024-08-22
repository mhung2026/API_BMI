using APIBIM.DTO;
using APIBIM.Models;
using APIBIM.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace APIBIM.Service
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepo _repository;

        public UserService(IUserRepo repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        public User createUser(UserDTO dto)
        {
            throw new NotImplementedException();
        }
        public String login(LoginDTO dto)
        {
            User user = _repository.login(dto);
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
