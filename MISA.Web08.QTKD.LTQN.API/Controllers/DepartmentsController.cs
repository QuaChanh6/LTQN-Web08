using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QTKD.Common.Entities;
using MISA.Web08.BL;

namespace MISA.Web08.QTKD.API.Controllers
{

    [ApiController]
    public class DepartmentsController : BaseController<Department>
    {
        #region contructor
        public DepartmentsController(IBaseBL<Department> baseBL) : base(baseBL)
        {
        } 
        #endregion
    }
}
