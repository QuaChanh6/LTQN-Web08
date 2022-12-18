using MISA.QTKD.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.DL
{
    public interface IAppFormDL : IBaseDL<AppForm>
    {
        public IEnumerable<Employee> managerDepartment(Guid code);

        public IEnumerable<AppForm> appFormOfManager(string name);
    }
}
