using AutoMapper;
using LectureSchedule.Domain.Identity;
using LectureSchedule.Service.DTO;
using LectureSchedule.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LectureSchedule.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration,
                            UserManager<User> userManager,
                            IMapper mapper)
        {
            this._configuration = configuration;
            this._userManager = userManager;
            this._mapper = mapper;
            this._key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["EncryptionKey"]));
        }

        public async Task<string> GetToken(UpdateUserDTO updateUserDTO)
        {
            var user = _mapper.Map<User>(updateUserDTO);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(user);
            //Adds to the claims object, all the roles obtained by _userManager.GetRolesAsync
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}
