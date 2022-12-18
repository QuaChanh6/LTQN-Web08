using MISA.QTKD.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.BL.AppFormBL
{
    public interface IAppFormBL : IBaseBL<AppForm>
    {
        public IEnumerable<Employee> managerDepartment(Guid code);

        public IEnumerable<AppForm> appFormOfManager(string name);
    }
}
