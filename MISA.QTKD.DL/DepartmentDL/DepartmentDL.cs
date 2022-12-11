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
    /// <summary>
    /// Departmnet data layer
    /// CreatedBy: LTQN(29/9/2022)
    /// </summary>
    public class DepartmentDL : BaseDL<Department>, IDepartmentDL
    {
        public IEnumerable<DepartmentCount> getCountEmployee()
        {
            //khai báo store proceduce
            string storedProceduceName = "Proc_department_GetCountEmployee";

            //kết nối đến db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                //thực hiện câu lệnh 
                var result = connect.Query<DepartmentCount>(storedProceduceName, null, commandType: System.Data.CommandType.StoredProcedure);

                // Trả về dữ liệu cho client
                return result;

            }
        }

    }
}
