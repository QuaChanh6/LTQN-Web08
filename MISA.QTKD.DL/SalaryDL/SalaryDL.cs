using Dapper;
using MISA.QTKD.Common.Entities;
using MISA.QTKD.Common.Resources;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.DL
{
    public class SalaryDL : BaseDL<Salary>, ISalaryDL
    {
        public int setDataImport(List<List<string>> data)
        {
            string query = "";
            var month = DateTime.Now.ToString("MM");
            var year = DateTime.Now.Year.ToString();
            var a = month + "N" + year;
            for (int i=0; i<data.Count; i++)
            {
                    string Code = data[i][0];
                    int numberWork = Int32.Parse(data[i][1]);
                    int bonusDate = Int32.Parse(data[i][2]);
                    int adMoney = Int32.Parse(data[i][3]);
                    query +=
                    $"UPDATE salary s SET s.NumberWork = {numberWork},s.BonusDate = {bonusDate},s.AdvanceMoney = {adMoney} WHERE s.SalaryCode = '{Code}' AND s.Month = '{a}';";
            }

            //kết nối đến db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                //thực hiện câu lệnh 
                var result = connect.Execute(query);

                // Trả về dữ liệu cho client
                return result;

            }
        }

        public IEnumerable<Salary> GetAllSalary(Guid department, string? keyword)
        {

            //khai báo store proceduce
            string storedProceduceName = "Proc_salary_getSalaryOfManager";
            var parameters = new DynamicParameters();
            parameters.Add("v_department", department);
            var month = DateTime.Now.ToString("MM");
            var year = DateTime.Now.Year.ToString();
            var a = month + "N" + year;
            parameters.Add("v_month", a);
            if (keyword != null)
            {
                string whereClause = $"s.EmployeeName LIKE '%{keyword}%' OR s.SalaryCode LIKE '%{keyword}%'";
                parameters.Add("v_Where", whereClause);
            }
            else
            {
                parameters.Add("v_Where", "");
            }
 


            //MySqlTransaction transaction = null;
            //khởi tạo kết nối tới db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                var records = connect.Query<Salary>(storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return records;
            }

        }

        public int reset()
        {
            IEnumerable<Employee> emps = this.GetAllE("", null);
           string query = "";
            var month = DateTime.Now.ToString("MM");
            var year = DateTime.Now.Year.ToString();
            var a = month + "N" + year;
            foreach (Employee emp in emps)
            {
                var id = Guid.NewGuid();
                query = query + "INSERT INTO salary(SalaryID, SalaryCode, EmployeeName, AdvanceMoney, Bonus, NumberWork, BonusDate, Dayoff, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy, Allowance, Month)" +
                        $"VALUES('{id}', '{emp.EmployeeCode}', '{emp.EmployeeName}', 0, 0, 0, 0, 0, now(), 'admin', now(), 'admin', 50000, '{a}');";

            }
           


            //MySqlTransaction transaction = null;
            //khởi tạo kết nối tới db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                //thực hiện câu lệnh 
                var result1 = connect.Execute(query);
                return result1;
            }
        }


        public IEnumerable<Salary> GetByCode(string code)
        {

            //khai báo store proceduce
            string storedProceduceName = String.Format(Resource.Proc_GetCode, typeof(Salary).Name);

            //chuẩn bị tham số đầu vào theo câu lênh
            var parameters = new DynamicParameters();
            string IdInput = $"v_{typeof(Salary).Name}Code";
                parameters.Add("v_month", null);

            parameters.Add(IdInput, code);


            //khởi tạo kết nối tới db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {

                //thực hiện gọi db
                var record = connect.Query<Salary>(storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return record;
            }


        }



        public IEnumerable<Employee> GetAllE(string? keyword, string? department)
        {

            //khai báo store proceduce
            string storedProceduceName = String.Format(Resource.Proc_GetAll, typeof(Employee).Name);
            var parameters = new DynamicParameters();
            if (keyword != null)
            {
                string whereClause = $"EmployeeName LIKE '%{keyword}%'";

                parameters.Add("v_Where", whereClause);
            }
            else
            {
                parameters.Add("v_Where", "");
            }

            parameters.Add("v_department", department);



            //MySqlTransaction transaction = null;
            //khởi tạo kết nối tới db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                var records = connect.Query<Employee>(storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return records;
            }

        }
    }
}
