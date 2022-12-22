using MISA.QTKD.Common;
using MISA.QTKD.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.DL
{
    public interface ISalaryDL : IBaseDL<Salary>
    {
        public int setDataImport(List<List<string>> data);

        public IEnumerable<Salary> GetAllSalary(Guid department, string? keyword);
    }
}
