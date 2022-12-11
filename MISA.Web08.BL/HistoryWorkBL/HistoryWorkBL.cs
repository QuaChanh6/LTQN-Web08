using MISA.QTKD.Common.Entities;
using MISA.QTKD.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.BL
{

    public class HistoryWorkBL : BaseBL<HistoryWork>, IHistoryWorkBL
    {
        private IHistoryWorkDL _historyWorkDL;
        public HistoryWorkBL(IHistoryWorkDL historyWorkDL) : base(historyWorkDL)
        {
            _historyWorkDL = historyWorkDL;
        }

        public IEnumerable<HistoryWork> GetAllWork(string? code)
        {
            return _historyWorkDL.GetAllWork(code);
        }

        public int Update(string employeeCode, Employee e)
        {
            return _historyWorkDL.Update(employeeCode, e);
        }
    }
}
