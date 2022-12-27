using MISA.QTKD.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.BL
{
    public interface IUserBL : IBaseBL<User>
    {
        public object Login(string username, string password);
        public int EditRoleUser(User record);

        public int EditPassword(User user);
    }
}
