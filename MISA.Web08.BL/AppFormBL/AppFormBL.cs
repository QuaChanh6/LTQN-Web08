using MISA.QTKD.Common.Entities;
using MISA.QTKD.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.BL.AppFormBL
{
    public class AppFormBL : BaseBL<AppForm>, IAppFormBL
    {
        private IAppFormDL _dl;
        public AppFormBL(IAppFormDL dl) : base(dl)
        {
            _dl = dl;
        }

        public IEnumerable<Employee> managerDepartment(Guid code)
        {
            return _dl.managerDepartment(code);
        }

        public IEnumerable<AppForm> appFormOfManager(string name)
        {
            return _dl.appFormOfManager(name);
        }
    }
}
