using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.DL
{
    public interface IUserDL
    {
        public object Login(string username, string passwword);
    }
}
