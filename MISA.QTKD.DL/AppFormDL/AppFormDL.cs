using Dapper;
using MISA.QTKD.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.DL.AppFormDL
{
    public class AppFormDL : BaseDL<AppForm>, IAppFormDL
    {
        public IEnumerable<Employee> managerDepartment(Guid code)
        {
            //khai báo store proceduce
            string storedProceduceName = "Proc_getManager";

            //chuẩn bị tham số đầu vào theo câu lênh
            var parameters = new DynamicParameters();
            string IdInput = "v_departmentID";
            parameters.Add(IdInput, code);


            //khởi tạo kết nối tới db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {

                //thực hiện gọi db
                var record = connect.Query<Employee>(storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return record;
            }
        }

        public IEnumerable<AppForm> appFormOfManager(string name)
        {

            //khai báo store proceduce
            string storedProceduceName = "Proc_appform_GetByManager";

            //chuẩn bị tham số đầu vào theo câu lênh
            var parameters = new DynamicParameters();
            string IdInput = "v_manager";
            parameters.Add(IdInput, name);


            //khởi tạo kết nối tới db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {

                //thực hiện gọi db
                var record = connect.Query<AppForm>(storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return record;
            }
        }
    }
}
