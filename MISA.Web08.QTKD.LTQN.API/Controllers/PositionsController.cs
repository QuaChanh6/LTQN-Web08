using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QTKD.Common.Entities;
using MISA.QTKD.Common.Resources;
using MISA.Web08.BL;

namespace MISA.Web08.QTKD.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PositionsController : BaseController<Position>
    {
        private IPositionBL _posBL;
        public PositionsController(IPositionBL posBL) : base(posBL)
        {
            _posBL = posBL;
        }

        [HttpGet("countEmployee")]
        public IActionResult Department()
        {
            try
            {
                var result = _posBL.getCountEmployee();

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_Exception);
            }
        }

    }
}
