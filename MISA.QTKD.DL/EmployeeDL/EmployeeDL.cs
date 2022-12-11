using Dapper;
using MISA.QTKD.Common.Entities;
using MySqlConnector;

namespace MISA.QTKD.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {

        /// <summary>
        /// Xóa nhiều bản ghi
        /// CreatedBy: LTQN(27/9/2022)
        /// </summary>
        /// <param name="ids">chuỗi id</param>
        /// <returns>số bản ghi bị ảnh hưởng</returns>
        public int DeleteMultiple(List<string> ids)
        {
 
            //khai báo store proceduce
            string storedProceduceName = "Proc_employee_Delete";

            MySqlTransaction transaction = null;
            //khởi tạo kết nối tới db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                connect.Open();
                transaction = connect.BeginTransaction();
               
                try
                {
                    foreach (var id in ids)
                    {
                        //chuẩn bị tham số đầu vào
                        var parameters = new DynamicParameters();
                        parameters.Add("v_EmployeeID", new Guid(id));
                        int recordsEffected = connect.Execute(storedProceduceName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);   

                    }
                    transaction.Commit();
                    return ids.Count;
                 
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

        /// <summary>
        /// Thêm mã nhân viên mới nhất
        /// CreatedBy: LTQN(27/9/2022)
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
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

        public IEnumerable<GenderCount> getCountGender()
        {
            //khai báo store proceduce
            string storedProceduceName = "Proc_employee_GetCountGender";

            //kết nối đến db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                //thực hiện câu lệnh 
                var result = connect.Query<GenderCount>(storedProceduceName, null, commandType: System.Data.CommandType.StoredProcedure);

                // Trả về dữ liệu cho client
                return result;

            }
        }

        public IEnumerable<StatusCount> getCountStatus()
        {
            //khai báo store proceduce
            string storedProceduceName = "Proc_employee_GetCountStatus";

            //kết nối đến db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                //thực hiện câu lệnh 
                var result = connect.Query<StatusCount>(storedProceduceName, null, commandType: System.Data.CommandType.StoredProcedure);

                // Trả về dữ liệu cho client
                return result;

            }
        }
    }

}
