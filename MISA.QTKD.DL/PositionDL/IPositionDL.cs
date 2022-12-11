using MISA.QTKD.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.DL
{
    public interface IPositionDL : IBaseDL<Position>
    {
        public IEnumerable<PositionCount> getCountEmployee();
    }
}
