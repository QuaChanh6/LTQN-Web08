using Dapper;
using MISA.QTKD.Common;
using MISA.QTKD.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.DL
{
    public class UserDL : BaseDL<User>, IUserDL
    {
        public object Login(string username, string passwword)
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
                var user = connect.Query(storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure);

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

        public int EditRoleUser(User record)
        {
            //khai báo store proceduce
            string storedProceduceName = "Proc_user_Edit";
            //chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("v_username", record.UserName);
            parameters.Add("v_role", record.role);

            MySqlTransaction transaction = null;
            //khởi tạo kết nối tới db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                connect.Open();
                transaction = connect.BeginTransaction();

                try
                {
                    int recordsEffected = connect.Execute(storedProceduceName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);

                    transaction.Commit();
                    //xử lý dữ liệu trả về
                        return recordsEffected;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return 0;
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public int EditPassword(User user)
        {
            //khai báo store proceduce
            string storedProceduceName = "Proc_user_password";
            //chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("v_employeeName", user.NameOfUser);
            parameters.Add("v_passWord", user.PassWord);
            parameters.Add("v_oldPassWord", user.oldPass);

            MySqlTransaction transaction = null;
            //khởi tạo kết nối tới db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                connect.Open();
                transaction = connect.BeginTransaction();

                try
                {
                    int recordsEffected = connect.Execute(storedProceduceName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);

                    transaction.Commit();
                    //xử lý dữ liệu trả về
                    return recordsEffected;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return 0;
                }
                finally
                {
                    connect.Close();
                }
            }
        }
    }
}
