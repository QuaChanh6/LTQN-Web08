using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QTKD.Common.Entities;
using MISA.QTKD.Common.Resources;
using MISA.Web08.BL;

namespace MISA.Web08.QTKD.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserBL _userBL;

        public UsersController(IUserBL userBL)
        {
            _userBL = userBL;
        }

        [HttpPost("login")]
        public IActionResult Users([FromBody] User user)
        {
            try
            {
                string result = _userBL.Login(user.UserName, user.PassWord);

                if (result != "")
                {
                    // Trả về dữ liệu cho client
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Tên đăng nhập hoặc mật khẩu không đúng!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_Exception);
            }
        }
    }
}
