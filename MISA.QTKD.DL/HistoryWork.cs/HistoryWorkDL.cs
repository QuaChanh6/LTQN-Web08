using Dapper;
using MISA.QTKD.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.DL
{
    public class HistoryWorkDL : BaseDL<HistoryWork>, IHistoryWorkDL
    {
        public IEnumerable<HistoryWork> GetAllWork(string? code)
        {
            //khai báo store proceduce
            string storedProceduceName = "Proc_historywork_GetAll";
            var parameters = new DynamicParameters();

            parameters.Add("v_EmployeeCode", code);

            //MySqlTransaction transaction = null;
            //khởi tạo kết nối tới db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                var records = connect.Query<HistoryWork>(storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return records;
            }
        }

        public int Update(string employeeCode, Employee e)
        {

            //khai báo store proceduce
            string storedProceduceName = "Proc_historywork_Edit";
            var parameters = new DynamicParameters();

            parameters.Add("v_EmployeeCode", employeeCode);
            parameters.Add("v_PositionW", e.PositionName);
            parameters.Add("v_DepartmentW", e.DepartmentName);
            parameters.Add("v_DateStart", DateTime.Now);


            //MySqlTransaction transaction = null;
            //khởi tạo kết nối tới db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                var records = connect.QueryFirstOrDefault(storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return records;
            }
        }
    
    }
}
