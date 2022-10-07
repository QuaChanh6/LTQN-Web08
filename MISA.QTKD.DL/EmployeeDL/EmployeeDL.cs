using Dapper;
using MISA.QTKD.Common.Entities;
using MySqlConnector;

namespace MISA.QTKD.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">chuỗi id</param>
        /// <returns>số bản ghi bị ảnh hưởng</returns>
        public int DeleteMultiple(string ids)
        {
            //chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("v_listID", ids);
            //khai báo store proceduce
            string storedProceduceName = "Proc_employee_DeleteMultiple";

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
                    return recordsEffected;
                 
                }
                catch (Exception)
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

        public string GetNewCodeEmployee()
        {

            //khai báo store proceduce
            string storedProceduceName = "Proc_employee_GetMaxEmployeeCode";

            //kết nối đến db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
            //thực hiện câu lệnh 
            string maxEmployeeCode = connect.QueryFirstOrDefault<string>(storedProceduceName, commandType: System.Data.CommandType.StoredProcedure);

            string newEmployeeCode = "NV" + (Int64.Parse(maxEmployeeCode) + 1).ToString();

            // Trả về dữ liệu cho client
            return newEmployeeCode;

            }
                //khởi tạo kết nối tới db
                //Connection<Employee> connect = new Connection<Employee>();


        }


    }

}
