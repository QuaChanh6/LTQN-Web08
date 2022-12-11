using MISA.QTKD.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.DL
{
    /// <summary>
    /// Interface Department data layer
    /// CreatedBy: LTQN(29/9/2022)
    /// </summary>
    public interface IDepartmentDL : IBaseDL<Department>
    {
        public IEnumerable<DepartmentCount> getCountEmployee();
    }
}
