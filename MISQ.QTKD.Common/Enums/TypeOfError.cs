using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.Common.Enums
{
    public enum TypeOfError
    {
        /// <summary>
        /// Lỗi exception chung
        /// </summary>
        Exception = 500,

        /// <summary>
        /// Lỗi xóa
        /// </summary>
        DeleteFailed = 0,

        /// <summary>
        /// Lỗi dữ liệu quá dài
        /// </summary>
        DataTooLong = 1,

        /// <summary>
        /// Lỗi trùng mã
        /// </summary>
        DuplicateCode = 2,

        /// <summary>
        /// Lỗi lấy dữ liệu
        /// </summary>
        GetRecordFailed = 3,

        /// <summary>
        /// Lỗi thêm dữ liệu
        /// </summary>
        InsertFailed = 4,

        /// <summary>
        /// Lỗi validate
        /// </summary>
        ValidateFailed = 5,

        /// <summary>
        /// Lỗi lọc/ phân trang/ tìm kiếm
        /// </summary>
        FilterFailed = 6
    }
}
