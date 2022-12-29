using MISA.QTKD.Common.Entities;
using MISA.QTKD.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.BL.SalaryBL
{
    public interface ISalaryBL : IBaseBL<Salary>
    {
        public int setDataImport(List<List<string>> data);

        public IEnumerable<Salary> GetAllSalary(Guid department, string? keyword);

        public int reset();
    }
}
