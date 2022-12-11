using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QTKD.Common.Entities;
using MISA.QTKD.Common.Enums;
using MISA.QTKD.Common.Resources;
using MISA.Web08.BL;

namespace MISA.Web08.QTKD.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HistoryWorkController : BaseController<HistoryWork>
    {
        private IHistoryWorkBL _hisBL;


        public HistoryWorkController(IHistoryWorkBL hisBL) : base(hisBL)
        {
            _hisBL = hisBL;
        }

        [HttpGet("AllWork/{code}")]
        public IActionResult GetAll([FromRoute] string code)
        {
            try
            {
                var records = _hisBL.GetAllWork(code);

                return StatusCode(StatusCodes.Status200OK, records);
            }
            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception, Resource.MoreInfo);
                handleError.SaveError(ex, er.ToStringMsg(HttpContext.TraceIdentifier));

                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_Exception);
            }
        }

        [HttpPut("Work/{employeeCode}")]
        public IActionResult Update([FromRoute] string employeeCode, [FromBody] Employee e)
        {
            try
            {
                _hisBL.Update(employeeCode, e);
 
                return StatusCode(StatusCodes.Status200OK);


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
