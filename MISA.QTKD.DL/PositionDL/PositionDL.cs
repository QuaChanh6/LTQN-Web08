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
    public class PositionDL : BaseDL<Position>, IPositionDL
    {
        public IEnumerable<PositionCount> getCountEmployee()
        {
            //khai báo store proceduce
            string storedProceduceName = "Proc_position_GetCountEmployee";

            //kết nối đến db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                //thực hiện câu lệnh 
                var result = connect.Query<PositionCount>(storedProceduceName, null, commandType: System.Data.CommandType.StoredProcedure);

                // Trả về dữ liệu cho client
                return result;

            }
        }
    }
}
