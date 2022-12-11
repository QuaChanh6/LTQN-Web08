using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QTKD.Common.Entities;
using MISA.QTKD.Common.Resources;
using MISA.Web08.BL;
using MISA.Web08.BL.DepartmentBL;

namespace MISA.Web08.QTKD.API.Controllers
{

    [ApiController]
    public class DepartmentsController : BaseController<Department>
    {
        private IDepartmentBL _depBL;
        #region contructor
        public DepartmentsController( IDepartmentBL depBL) : base(depBL)
        {
            _depBL = depBL;
        } 
        #endregion

        [HttpGet("countEmployee")]
        public IActionResult Department()
        {
            try
            {
                var result = _depBL.getCountEmployee();

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_Exception);
            }
        }

    }
}
