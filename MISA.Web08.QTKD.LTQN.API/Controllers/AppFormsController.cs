using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QTKD.Common.Entities;
using MISA.QTKD.Common.Enums;
using MISA.QTKD.Common.Resources;
using MISA.Web08.BL.AppFormBL;

namespace MISA.Web08.QTKD.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppFormsController : BaseController<AppForm>
    {
        private IAppFormBL _bl;

        public AppFormsController(IAppFormBL bl) : base(bl)
        {
            _bl = bl;
        }

        [HttpGet("manager/{code}")]
        public IActionResult managerDepartment([FromRoute] Guid code)
        {
            try
            {
                var records = _bl.managerDepartment(code);

                return StatusCode(StatusCodes.Status200OK, records);
            }
            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception, Resource.MoreInfo);
                handleError.SaveError(ex, er.ToStringMsg(HttpContext.TraceIdentifier));

                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_Exception);
            }
        }

        [HttpGet("filtermanager")]
        public IActionResult appFormOfManager([FromQuery] string name)
        {
            try
            {
                var records = _bl.appFormOfManager(name);

                return StatusCode(StatusCodes.Status200OK, records);
            }
            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception, Resource.MoreInfo);
                handleError.SaveError(ex, er.ToStringMsg(HttpContext.TraceIdentifier));

                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_Exception);
            }
        }

    }
}
