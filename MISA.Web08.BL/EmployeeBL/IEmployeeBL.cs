using MISA.QTKD.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.BL
{
    public interface IEmployeeBL : IBaseBL<Employee>
    {
        /// <summary>
        /// Lấy mã nhân viên mới nhất
        /// CreatedBy: LTQN(27/9/2022)
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        public string GetNewCodeEmployee();

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">chuỗi id</param>
        /// <returns>số bản ghi bị ảnh hưởng</returns>
        public int DeleteMultiple(string ids);


    }
}
