using MISA.QTKD.Common.Entities;
using MISA.QTKD.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.BL.SalaryBL
{
    public class SalaryBL : BaseBL<Salary>, ISalaryBL
    {
        private ISalaryDL _salDL;
        public SalaryBL(ISalaryDL salDL) : base(salDL)
        {
            _salDL = salDL;
        }
        public int setDataImport(List<List<string>> data)
        {
            return _salDL.setDataImport(data);
        }

        public IEnumerable<Salary> GetAllSalary(Guid department, string? keyword)
        {
            return _salDL.GetAllSalary(department, keyword);
        }
    }
}
