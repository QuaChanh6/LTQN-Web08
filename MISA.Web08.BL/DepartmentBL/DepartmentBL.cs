using MISA.QTKD.Common.Entities;
using MISA.QTKD.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.BL.DepartmentBL
{
    public class DepartmentBL : BaseBL<Department>, IDepartmentBL
    {

        private IDepartmentDL _depDL;

        public DepartmentBL(IDepartmentDL depDL) : base(depDL)
        {
            _depDL = depDL;
        }

        public IEnumerable<DepartmentCount> getCountEmployee()
        {
            return _depDL.getCountEmployee();
        }
    }
}
