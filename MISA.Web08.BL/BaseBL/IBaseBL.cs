using MISA.QTKD.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web08.BL
{
    public interface IBaseBL<T>
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
        public Object Insert(T record);

        /// <summary>
        /// Sửa 1 bản ghi
        /// Createdby: LTQN(29/9/2022)
        /// </summary>
        /// <param name="record">Bản ghi sửa</param>
        /// <returns>id bản ghi</returns>
        public Object Edit(T record);

        /// <summary>
        /// Xóa 1 bản ghi
        /// Createdby: LTQN(29/9/2022)
        /// </summary>
        /// <param name="employeeID">id bản ghi cần xóa</param>
        /// <returns>id bản ghi</returns>
        public Guid Delete(Guid employeeID);
    }
}
