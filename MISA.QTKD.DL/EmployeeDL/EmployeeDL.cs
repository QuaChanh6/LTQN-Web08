using Dapper;
using MISA.QTKD.Common.Entities;
using MISA.QTKD.DL.Connection;

namespace MISA.QTKD.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        public string GetNewCodeEmployee()
        {
            //khởi tạo kết nối tới db
            Connection<Employee> connect = new Connection<Employee>();

            //khai báo store proceduce
            string storedProceduceName = "Proc_employee_GetMaxEmployeeCode";

            //thực hiện câu lệnh 
            string maxEmployeeCode = connect.QueryFirstOrDefaultProc(storedProceduceName);

            string newEmployeeCode = "NV" + (Int64.Parse(maxEmployeeCode) + 1).ToString();

            connect.CloseConnect();

            // Trả về dữ liệu cho client
            return newEmployeeCode;
        }

    }

}
