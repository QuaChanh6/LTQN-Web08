using MISA.QTKD.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.BL
{
    public interface IPositionBL : IBaseBL<Position>
    {
        public IEnumerable<PositionCount> getCountEmployee();
    }
}
