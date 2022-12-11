using MISA.QTKD.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.DL
{
    public interface IHistoryWorkDL : IBaseDL<HistoryWork>
    {
        public IEnumerable<HistoryWork> GetAllWork(string? code);

        public void Update(string employeeCode, Employee e);
    }
}
