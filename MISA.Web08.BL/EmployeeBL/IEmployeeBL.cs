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
        /// Lấy thông tin tất cả nhân viên
        /// CreatedBy: LTQN(27/9/2022)
        /// </summary>
        /// <returns>Danh sách nhân viên</returns>
        //public IEnumerable<Employee> GetAllEmployee();


        /// <summary>
        /// Lấy thông tin 1 nhân viên bằng id
        /// CreatedBy: LTQN(27/9/2022)
        /// </summary>
        /// <param name="employeeID">id nhân viên</param>
        /// <returns>Thông tin nhân viên</returns>
        //public IEnumerable<Employee> GetEmployeeByID(Guid employeeID);

        /// <summary>
        /// Lấy thông tin nhân viên phân trang/tìm kiếm
        /// CreatedBy: LTQN(27/9/2022)
        /// </summary>
        /// <param name="keyword">Tìm kiếm</param>
        /// <param name="sort">Sắp xếp</param>
        /// <param name="limit">Số bản ghi cần lấy</param>
        /// <param name="pageNumber">Số trang</param>
        /// <returns>Trả về đối tượng PagingData</returns>
       // public PagingData<Employee> FilterEmployee(string? keyword, string? sort, int limit, int pageNumber);

        /// <summary>
        /// Lấy mã nhân viên mới nhất
        /// CreatedBy: LTQN(27/9/2022)
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        public string GetNewCodeEmployee();

        /// <summary>
        /// Thêm nhân viên
        /// CreatedBy: LTQN(27/9/2022)
        /// </summary>
        /// <param name="employee">Nhân viên cần thêm</param>
        /// <returns>trả về 1 nếu thành công, rỗng nếu thất bại</returns>
        //public Guid InsertEmployee(Employee employee);

        /// <summary>
        /// Sửa một nhân viên
        /// CreatedBy: LTQN(27/9/2022)
        /// </summary>
        /// <param name="employeeID">id nhân viên cần sửa</param>
        /// <param name="employee">Nhân viên cần sửa</param>
        /// <returns>trả về id nhân viên nếu thành công ,rỗng nếu thất bại</returns>
       // public Guid EditEmployee(Guid employeeID, Employee employee);

        /// <summary>
        /// Xóa một nhân viên
        /// CreatedBy: LTQN(27/9/2022)
        /// </summary>
        /// <param name="employeeID">ID nhân viên cần xóa</param>
        /// <returns>trả về id nhân viên nếu thành công, 0 nếu thất bại</returns>
       // public Guid DeleteEmployee(Guid employeeID);
    }
    }
