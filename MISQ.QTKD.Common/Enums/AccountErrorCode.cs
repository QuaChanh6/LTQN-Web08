namespace MISA.QTKD.Common.Enum
{
        /// <summary>
        /// Các lỗi
        /// CreatedBy: LTQN(22/9/2022)
        /// </summary>
    public enum AccountErrorCode
    {
        /// <summary>
        /// Exception
        /// </summary>
       Exception = 1,

       /// <summary>
       /// Trùng mã nhân viên
       /// </summary>
       DuplicateCode = 2,


       /// <summary>
       /// Insert thất bại
       /// </summary>
       InsertFailed = 3,

       /// <summary>
       /// Dữ liệu đầu vào bị lỗi
       /// </summary>
       InvalidInput = 4,

       /// <summary>
       /// Lấy dữ liệu lỗi
       /// </summary>
       GetFailed = 5,

       /// <summary>
       /// Dữ liệu quá dài
       /// </summary>
       DataTooLong = 6,

       /// <summary>
       ///Phân trang/ tìm kiếm lỗi
       /// </summary>
       FilterFailed = 7,

       /// <summary>
       /// Xóa lỗi
       /// </summary>
       DeleteFailed = 8

    }
}
