using MISA.QTKD.Common.Entities;
using MISA.QTKD.DL;


namespace MISA.Web08.BL
{
    public class UserBL : BaseBL<User>, IUserBL
    {
        private IUserDL _user;
        public UserBL(IUserDL user) : base(user)
        {
            _user = user;
        }

        public object Login(string username, string password)
        {
            return _user.Login(username, password);
        }
    }
}
