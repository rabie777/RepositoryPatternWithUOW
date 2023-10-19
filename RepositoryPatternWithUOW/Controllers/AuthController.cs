using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.BL.Interfaces;
using RepositoryPatternWithUOW.BL.Repository;
using RepositoryPatternWithUOW.core.Modles;
using System.Reflection.Metadata.Ecma335;

namespace RepositoryPatternWithUOW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            return Ok(new { Token = result.Token, ExpireOn = result.ExpiresOn });
        }

        [HttpPost("GetToken")]
        public async Task<IActionResult> GetTokenAsync(TokenRequstModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(model);
            return Ok(result);

        }
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRoleAsync(RoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);
            if (!string.IsNullOrEmpty(result))
                return BadRequest (result);

            return Ok(model);

        }
    }
}
