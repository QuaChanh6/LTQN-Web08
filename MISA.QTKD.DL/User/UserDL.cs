using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.DL.User
{
    public class UserDL : IUserDL
    {
        public string Login(string username, string passwword)
        {
            //khai báo store proceduce
            string storedProceduceName = "Proc_user_Login";

            var parameters = new DynamicParameters();

            parameters.Add("v_userName", username);
            parameters.Add("v_passWord", passwword);

            //kết nối đến db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                //thực hiện câu lệnh 
                string user = connect.QueryFirstOrDefault<string>(storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                if(user != null)
                {
                    return user;
                }
                // Trả về dữ liệu cho client
                return "";

            }
            //khởi tạo kết nối tới db
            //Connection<Employee> connect = new Connection<Employee>();
        }
    }
}
