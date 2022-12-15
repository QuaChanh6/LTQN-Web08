using MISA.QTKD.DL;


namespace MISA.Web08.BL
{
    public class UserBL : IUserBL
    {
        private IUserDL _user;
        public UserBL(IUserDL user)
        {
            _user = user;
        }

        public object Login(string username, string password)
        {
            return _user.Login(username, password);
        }
    }
}
