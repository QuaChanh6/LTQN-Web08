using MISA.QTKD.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.DL
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {

        ///public PagingData<Employee> FilterEmployee(string? keyword, string? sort, int limit, int pageNumber);

        /// <summary>
        /// Lấy mã nhân viên mới nhất
        /// CreatedBy: LTQN(27/9/2022)
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        public string GetNewCodeEmployee();

    }
}
