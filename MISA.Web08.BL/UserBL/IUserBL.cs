using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.BL
{
    public interface IUserBL
    {
        public object Login(string username, string password);
    }
}
