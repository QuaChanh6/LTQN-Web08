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
    public class SalaryDL : BaseDL<Salary>, ISalaryDL
    {
        public int setDataImport(List<List<string>> data)
        {
            string query = "";

            for(int i=0; i<data.Count; i++)
            {
                    string Code = data[i][0];
                    int numberWork = Int32.Parse(data[i][1]);
                    int bonusDate = Int32.Parse(data[i][2]);
                    int adMoney = Int32.Parse(data[i][3]);
                    query +=
                    $"UPDATE salary s SET s.NumberWork = {numberWork},s.BonusDate = {bonusDate},s.AdvanceMoney = {adMoney} WHERE s.SalaryCode = '{Code}';";
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
    }
}
