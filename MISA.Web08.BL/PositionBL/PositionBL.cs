using MISA.QTKD.Common.Entities;
using System;
using MISA.QTKD.DL;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.BL
{
    public class PositionBL : BaseBL<Position>, IPositionBL
    {
        private IPositionDL _pos; 
        public PositionBL(IPositionDL posDL) : base(posDL)
        {
            _pos = posDL;
        }

        public IEnumerable<PositionCount> getCountEmployee()
        {
            return _pos.getCountEmployee();
        }
    }
}
