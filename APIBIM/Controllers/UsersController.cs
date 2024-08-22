using APIBIM.DTO;
using APIBIM.Models;
using APIBIM.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIBIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IConfiguration _configuration;
        public UsersController(IUserService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }
        [HttpPost("login")]
        public async Task<IActionResult> login(LoginDTO login)
        {
            try
            {
                String token = _service.login(login);
                if (token == null)
                {
                    return BadRequest(new
                    {
                        error = "Token invalid"
                    });
                }
                return Ok(new { Token = token });
                
            }
            catch (Exception ex)
            {
                return NotFound("Đã xảy ra lỗi trong quá trình đăng nhập , vui lòng thử lại.");
            }       
        }
        [HttpPost]
        [Route("register")]
        [Authorize]
        public async Task<ActionResult<User>> register(UserDTO user)
        {
            try
            {
                User account = _service.createUser(user);
                if (account == null)
                {
                    return BadRequest("Tạo tài khoảng thất bại, vui lòng tạo lại. ");
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                return NotFound("Đã xảy ra lỗi trong quá trình tạo , vui lòng thử lại. ");
            }
        }
    }
}
