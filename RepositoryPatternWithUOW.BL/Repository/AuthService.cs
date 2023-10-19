 
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RepositoryPatternWithUOW.BL.Interfaces;
using RepositoryPatternWithUOW.core.Database;
using RepositoryPatternWithUOW.core.Modles;
using System;
using System.Collections.Generic;   
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.BL.Repository
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly JWT _JWT;
        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> RoleManager, IOptions<JWT> jwt)
        {
             _JWT = jwt.Value;
             _userManager= userManager;
             _RoleManager = RoleManager;
        }

        public async Task<string> AddRoleAsync(RoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserID);
            if(user is null ||! await _RoleManager.RoleExistsAsync(model.Role))
            {
                return "Invalid User Or  Role";
            }  
            if(await _userManager.IsInRoleAsync(user,model.Role))
                return "User Already assigned to this Role";

           var result= await _userManager.AddToRoleAsync(user, model.Role);
            return result.Succeeded ? string.Empty : "Something Went Wrong";
             
        
        }

        public async Task<AuthModel> GetTokenAsync(TokenRequstModel model)
        {
            var auth = new AuthModel();
         var user=await _userManager.FindByEmailAsync(model.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password)) 
            {
                auth.Message = "UserName or Password is not correct";
                return auth;
            }
            
            var jwtSecurityToken = await CreateJwtToken(user);

                auth.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                auth.Email = user.Email;
                auth.IsAuthenticated = true;
                auth.UserName = user.UserName;
                auth.ExpiresOn = jwtSecurityToken.ValidTo;

                var roles= await _userManager.GetRolesAsync(user);
                auth.Roles = roles.ToList();

            return auth;
         }
  
        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
             if(await _userManager.FindByEmailAsync(model.Email) is not null)
                
                return new AuthModel{ Message = "This Email Is Exists" };
                
            if (await _userManager.FindByNameAsync(model.UserName) is not null)
             
                return new AuthModel { Message = "This UserName Is Exists" };


            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
          var result=  await _userManager.CreateAsync(user,model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description} ,";
                }
                return new AuthModel
                {
                    Message = errors
                };
            }

            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName
            };

        }


        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWT.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _JWT.issuer,
                audience: _JWT.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_JWT.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
