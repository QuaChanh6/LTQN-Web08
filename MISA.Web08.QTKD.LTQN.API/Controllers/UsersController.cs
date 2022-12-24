using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QTKD.Common.Entities;
using MISA.QTKD.Common.Resources;
using MISA.Web08.BL;

namespace MISA.Web08.QTKD.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : BaseController<User>
    {
        private IUserBL _userBL;

        public UsersController(IUserBL salBL) : base(salBL)
        {
            _userBL = salBL;
        }

        [HttpPost("login")]
        public IActionResult Users([FromBody] User user)
        {
            try
            {
                if(user.UserName != null && user.PassWord != null)
                {
                    var result = _userBL.Login(user.UserName, user.PassWord);
                    if (result != null)
                    {
                        // Trả về dữ liệu cho client
                        return StatusCode(StatusCodes.Status200OK, result);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "Tên đăng nhập hoặc mật khẩu không đúng!");
                    }

                }
                return StatusCode(StatusCodes.Status400BadRequest, "Tên đăng nhập hoặc mật khẩu không đúng!");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_Exception);
            }
        }

        [HttpPut("Role")]
        public IActionResult Usernew([FromBody] User user)
        {
            try
            {
                    var result = _userBL.EditRoleUser(user);
                    if (result != 0)
                    {
                        // Trả về dữ liệu cho client
                        return StatusCode(StatusCodes.Status200OK, result);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "Sửa bản ghi thất bại!");
                    }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_UpdateFailed);
            }
        }

    }
}
