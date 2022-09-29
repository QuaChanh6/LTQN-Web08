using MISA.QTKD.Common.Enum;

namespace MISA.QTKD.Common.Entities
{
        /// <summary>
        /// Thông báo lỗi
        /// CreatedBy: LTQN
        /// (19/9/2022)
        /// </summary>
    public class ErrorResult
    {
        #region Field
        /// <summary>
        /// Định danh của mã lỗi nội bộ
        /// </summary>
        public AccountErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Thông báo cho Dev biết vấn đề đang gặp phải
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Thông báo cho user
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Thông tin chi tiết hơn về vấn đề
        /// </summary>
        public string MoreInfo { get; set; }
        #endregion

        #region Contructor

        public ErrorResult() { }
        public ErrorResult(AccountErrorCode errorCode, string devMsg, string userMsg, string moreInfo)
        {
            ErrorCode = errorCode;
            DevMsg = devMsg;
            UserMsg = userMsg;
            MoreInfo = moreInfo;
        }
        #endregion

        #region Method
        public string ToStringMsg()
        {
            string msg = $"Lỗi: AccountErrorCode: {ErrorCode}\n  DevMsg: {DevMsg}\n  DserMsg: {UserMsg}\n  DoreInfo: {MoreInfo}\n";
            return msg;
        } 
        #endregion
    }
}
