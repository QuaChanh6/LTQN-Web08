using MISA.QTKD.Common.Entities;
using MISA.QTKD.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        private IEmployeeDL _employeeDL;

        public EmployeeBL(IEmployeeDL employeeDL) :base(employeeDL)
        {
            _employeeDL = employeeDL;
        }

        public string GetNewCodeEmployee()
        {
            return _employeeDL.GetNewCodeEmployee();
        }

    }
}
