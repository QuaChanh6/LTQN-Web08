using MISA.QTKD.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.DL
{
    public interface IUserDL : IBaseDL<User>
    {
        public object Login(string username, string passwword);

        public int EditRoleUser(User record);

        public int EditPassword(User user);
    }
}
