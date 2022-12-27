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

        public int EditRoleUser(User record)
        {
            return _user.EditRoleUser(record);
        }

        public int EditPassword(User user)
        {
            return _user.EditPassword(user);
        }
    }
}
