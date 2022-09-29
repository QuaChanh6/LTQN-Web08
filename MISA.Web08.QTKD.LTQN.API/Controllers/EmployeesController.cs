using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.QTKD.Common.Entities;
using MISA.QTKD.Common.Enum;
using MISA.QTKD.Common.Resources;
using MISA.Web08.BL;
using MySqlConnector;
using NLog;
using NLog.Web;

namespace MISA.Web08.QTKD.API.Controllers
{

    /// <summary>
    /// API employee
    /// CreatedBy: LTQN(16/9/2022)
    /// </summary>

    [ApiController]
    public class EmployeesController : BaseController<Employee>
    {
        private IEmployeeBL _employeeBL;


        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }

        /// <summary>
        /// Láy mã nhân viên lớn nhất
        /// CreatedBy: LTQN (16/9/2022)
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        [HttpGet("new-code")]
        public IActionResult NewCodeEmployee()
        {
            try
            {
                var newCode = _employeeBL.GetNewCodeEmployee();
                // Trả về dữ liệu cho client
                return StatusCode(StatusCodes.Status200OK, newCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                                   new ErrorResult(
                                            AccountErrorCode.Exception,
                                            Resource.DevMsg_Exception,
                                            Resource.UserMsg_Exception,
                                            Resource.MoreInfo
                                        )
                                 );
            }

        }



    }
   
}
