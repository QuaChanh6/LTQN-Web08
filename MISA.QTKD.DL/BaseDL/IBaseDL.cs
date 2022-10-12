using MISA.QTKD.Common;
using MISA.QTKD.Common.Entities;

namespace MISA.QTKD.DL
{
    /// <summary>
    /// Interface data layer
    /// CreatedBy: LTQN(29/9/2022)
    /// </summary>
    /// <typeparam name="T">Generic</typeparam>
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Lấy danh sách bản ghi trong 1 bảng
        /// Createdby: LTQN(29/9/2022)
        /// </summary>
        /// <returns>danh sách các bản ghi</returns>
        public IEnumerable<T> GetAll();

        /// <summary>
        /// Lấy bản ghi thông qua Id
        /// Createdby: LTQN(29/9/2022)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<T> GetById(Guid id);

        /// <summary>
        /// Lọc/ tìm kiếm/ phân trang trong 1 bảng
        /// Createdby: LTQN(29/9/2022)
        /// </summary>
        /// <param name="keyword">Từ cần tìm</param>
        /// <param name="sort">sắp xếp</param>
        /// <param name="limit">Số lượng bản ghi trong 1 trang</param>
        /// <param name="pageNumber">số trang</param>
        /// <returns></returns>
        public PagingData<T> Filter(string? keyword, string? sort, int limit, int pageNumber);

        /// <summary>
        /// Thêm 1 bản ghi
        /// Createdby: LTQN(29/9/2022)
        /// </summary>
        /// <param name="record">Bản ghi cần thêm</param>
        /// <returns>id bản ghi</returns>
        public Response Insert(T record);

        /// <summary>
        /// Sửa 1 bản ghi
        /// Createdby: LTQN(29/9/2022)
        /// </summary>
        /// <param name="record">Bản ghi sửa</param>
        /// <returns>id bản ghi</returns>
        public Response Edit(T record);

        /// <summary>
        /// Xóa 1 bản ghi
        /// Createdby: LTQN(29/9/2022)
        /// </summary>
        /// <param name="employeeID">id bản ghi cần xóa</param>
        /// <returns>id bản ghi</returns>
        public Guid Delete(Guid employeeID);

        /// <summary>
        /// Kiểm tra mã trùng
        /// CreatedBy: LTQN(9/10/2022)
        /// </summary>
        /// <param name="record">bản ghi nhân viên</param>
        /// <returns>IDs nếu bị trùng</returns>
        public Guid checkDuplicateEmployeeCode(T record);
    }
}
