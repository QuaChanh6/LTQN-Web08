using MISA.QTKD.Common.Entities;
using MISA.QTKD.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        private IEmployeeDL _employeeDL;

        public EmployeeBL(IEmployeeDL employeeDL) :base(employeeDL)
        {
            _employeeDL = employeeDL;
        }


        /// <summary>
        /// Lấy mã nhân viên mới nhất
        /// CreatedBy: LTQN(27/9/2022)
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        public string GetNewCodeEmployee()
        {
            return _employeeDL.GetNewCodeEmployee();
        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">chuỗi id</param>
        /// <returns>số bản ghi bị ảnh hưởng</returns>
        public int DeleteMultiple(List<string> ids)
        {
            return _employeeDL.DeleteMultiple(ids);
        }

        public IEnumerable<GenderCount> getCountGender()
        {
            return _employeeDL.getCountGender();
        }

        public IEnumerable<StatusCount> getCountStatus()
        {
            return _employeeDL.getCountStatus();
        }
    }
}
