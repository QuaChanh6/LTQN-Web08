using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QTKD.Common.Entities;
using MISA.QTKD.Common.Enum;
using MISA.QTKD.Common.Enums;
using MISA.QTKD.Common.Resources;
using MISA.Web08.BL;
using MySqlConnector;
using NLog;


namespace MISA.Web08.QTKD.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Feild
        private IBaseBL<T> _baseBL;
        #endregion

        #region Contructor

        public BaseController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }
        #endregion

        #region Method

        #region API Get
        /// <summary>
        /// API lấy danh sách 1  bảng
        /// CreatedBy: LTQN(16/9/2022)
        /// </summary>
        /// <returns>Danh sách 1 bảng</returns>
        [HttpGet]
        [Route("")]
        public IActionResult RecordsOfTable()
        {
            try
            {
                var records = _baseBL.GetAll();

                return StatusCode(StatusCodes.Status200OK, records);
            }
            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception);
                handleError.SaveError(ex.Message, er.ToStringMsg());

                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        /// <summary>
        /// APT lấy thông tin một nhân viên theo ID
        /// CreatedBy: LTQN(16/9/2022)
        /// </summary>
        /// <param name="employeeID">ID nhân viên</param>
        /// <returns>thông tin một nhân viên</returns>
        [HttpGet("{employeeID}")]
        public IActionResult Records([FromRoute] Guid employeeID)
        {
            try
            {
                var employee = _baseBL.GetById(employeeID);

                return StatusCode(StatusCodes.Status200OK, employee);
            }
            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception);
                handleError.SaveError(ex.Message, er.ToStringMsg());

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Lọc thông tin/ phân trang/ tìm kiếm
        /// </summary>
        /// <param name="keyword">Từ tìm kiếm</param>
        /// <param name="sort">Sắp xếp</param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Trang muốn lấy</param>
        /// <returns>PagingData: Danh sách nhân viên thỏa mãn điều kiện/ Tổng số bản ghi</returns>
        [HttpGet("filter")]
        public IActionResult Records([FromQuery] string? keyword, [FromQuery] string? sort,
            [FromQuery] int limit, [FromQuery] int pageNumber)
        {
            try
            {
                PagingData<T> emp = _baseBL.Filter(keyword, sort, limit, pageNumber);

                // Xử lý kết quả trả về từ DB
                if (emp != null)
                {
                    return StatusCode(StatusCodes.Status200OK, emp);
                }
                else
                {
                    ErrorResult er = handleError.setErrorCode(TypeOfError.Exception);
                    handleError.SaveError(StatusCodes.Status400BadRequest.ToString(), er.ToStringMsg());
                    return StatusCode(StatusCodes.Status400BadRequest, Resource.UserMsg_FilterFailed);
                }
            }
            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception);
                handleError.SaveError(ex.Message, er.ToStringMsg());

                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }
        #endregion

        #region API Post
        /// <summary>
        /// Thêm 1 bản ghi
        /// CreatedBy: LTQn(16/9/2022)
        /// </summary>
        /// <param name="employee">Nhân viên</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Employee([FromBody] T record)
        {
            try
            {

                var recordID = _baseBL.Insert(record);


                // th1: thành công
                if (recordID != Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status201Created, recordID);
                }
                else  //th2: thất bại
                {
                    return StatusCode(StatusCodes.Status400BadRequest, Resource.UserMsg_InsertFailed);
                }

            }
            catch (MySqlException mySqlException)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception);
                if (mySqlException.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
                {
                    er = handleError.setErrorCode(TypeOfError.DuplicateCode);
                    handleError.SaveError(mySqlException.Message, er.ToStringMsg());
                    return StatusCode(StatusCodes.Status400BadRequest, Resource.UserMsg_DuplicateCode);
                }
                if (mySqlException.ErrorCode == MySqlErrorCode.DataTooLong)
                {
                    er = handleError.setErrorCode(TypeOfError.DataTooLong);
                    handleError.SaveError(mySqlException.Message, er.ToStringMsg());

                    return StatusCode(StatusCodes.Status400BadRequest, Resource.UserMsg_DataTooLong);
                }

                handleError.SaveError(mySqlException.Message, er.ToStringMsg());

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception);
                handleError.SaveError(ex.Message, er.ToStringMsg());

                return StatusCode(StatusCodes.Status500InternalServerError);


            }



        }

        #endregion

        #region API Put
        /// <summary>
        /// Sửa thông tin 1 bản ghi
        /// CreatedBy: LTQN (16/9/2022)
        /// </summary>
        /// <param name="record">thông tin bản ghi</param>
        /// <param name="recordId">ID bản ghi</param>
        /// <returns>ID bản ghi được sửa</returns>
        [HttpPut("{recordId}")]
        public IActionResult Records([FromBody] T record, [FromRoute] Guid recordId)
        {
            try
            {
                var idEmployeeEdit = _baseBL.Edit(record, recordId);

                // th1: thành công
                if (idEmployeeEdit != Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status200OK, recordId);
                }
                else  //th2: thất bại
                {
                    return StatusCode(StatusCodes.Status400BadRequest, Resource.UserMsg_UpdateFailed);
                }
            }
            catch (MySqlException mySqlException)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception);
                if (mySqlException.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
                {
                    er = handleError.setErrorCode(TypeOfError.DuplicateCode);
                    handleError.SaveError(mySqlException.Message, er.ToStringMsg());
                    return StatusCode(StatusCodes.Status400BadRequest, Resource.UserMsg_DuplicateCode);
                }
                if (mySqlException.ErrorCode == MySqlErrorCode.DataTooLong)
                {
                    er = handleError.setErrorCode(TypeOfError.DataTooLong);
                    handleError.SaveError(mySqlException.Message, er.ToStringMsg());

                    return StatusCode(StatusCodes.Status400BadRequest, Resource.UserMsg_DataTooLong);
                }

                handleError.SaveError(mySqlException.Message, er.ToStringMsg());

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception);
                handleError.SaveError(ex.Message, er.ToStringMsg());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        #endregion


        #region API Delete
        /// <summary>
        /// Xóa 1 nhân viên
        /// CratedBy: LTQN (16/9/2022)
        /// </summary>
        /// <param name="employeeID">ID nhân viên muốn xóa</param>
        /// <returns></returns>
        [HttpDelete("{employeeID}")]
        public IActionResult Record([FromRoute] Guid employeeID)
        {
            try
            {

                var idEmployeeDelete = _baseBL.Delete(employeeID);

                //Xử lý kết quả trả về từ DB
                if (idEmployeeDelete != Guid.Empty) //nếu thành công
                {

                    return StatusCode(StatusCodes.Status200OK, employeeID);
                }
                else // nếu thất bại
                {
                    ErrorResult er = handleError.setErrorCode(TypeOfError.DeleteFailed);
                    handleError.SaveError(StatusCodes.Status400BadRequest.ToString(), er.ToStringMsg());

                    return StatusCode(StatusCodes.Status400BadRequest, Resource.UserMsg_DeleteFailed);

                }
            }
            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception);
                handleError.SaveError(ex.Message, er.ToStringMsg());

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion 
        #endregion






    }

    /// <summary>
    /// Xử lý lỗi
    /// CreatedBy: LTQN(29/9/2022)
    /// </summary>
    public class handleError {

        /// <summary>
        /// Hàm Lưu lỗi
        /// CreatedBy: LTQN(29/9/2022)
        /// </summary>
        /// <param name="ex">Thông tin lỗi</param>
        /// <param name="er">ErrorResult</param>
        public static void SaveError(string ex, string er)
        {
            Logger logger = LogManager.GetLogger("fileLogger");
            logger.Error(er, ex);

        }

        /// <summary>
        /// Hàm tạo ErrorCode
        /// CreatedBy: LTQN(29/9/2022)
        /// </summary>
        /// <param name="type">Loại lỗi</param>
        /// <returns>ErrorCode</returns>
        public static ErrorResult setErrorCode(TypeOfError type)
        {
            string devMsg = "";
            string userMsg = "";
            string moreInfo = Resource.MoreInfo;
            AccountErrorCode errorCode = 0;
            switch (type)
            {
                case TypeOfError.Exception:
                    devMsg = Resource.DevMsg_Exception;
                    userMsg = Resource.UserMsg_Exception;
                    errorCode = AccountErrorCode.Exception;
                    break;

                case TypeOfError.GetRecordFailed:
                    devMsg = Resource.DevMsg_GetRecordFailed;
                    userMsg = Resource.UserMsg_GetRecordFailed;
                    errorCode = AccountErrorCode.GetFailed;
                    break;

                case TypeOfError.DeleteFailed:
                    devMsg = Resource.DevMsg_DeleteFailed;
                    userMsg = Resource.UserMsg_DeleteFailed;
                    errorCode = AccountErrorCode.DeleteFailed;
                    break;

                case TypeOfError.InsertFailed:
                    devMsg = Resource.DevMsg_InsertFailed;
                    userMsg = Resource.UserMsg_InsertFailed;
                    errorCode = AccountErrorCode.InsertFailed;
                    break;

                case TypeOfError.DataTooLong:
                    devMsg = Resource.DevMsg_DataTooLong;
                    userMsg = Resource.UserMsg_DataTooLong;
                    errorCode = AccountErrorCode.DataTooLong;
                    break;

                case TypeOfError.DuplicateCode:
                    devMsg = Resource.DevMsg_DuplicateCode;
                    userMsg = Resource.UserMsg_DuplicateCode;
                    errorCode = AccountErrorCode.DuplicateCode;
                    break;
                case TypeOfError.ValidateFailed:
                    devMsg = Resource.DevMsg_ValidateFailed;
                    userMsg = Resource.UserMsg_ValidateFailed;
                    errorCode = AccountErrorCode.InvalidInput;
                    break;
                case TypeOfError.FilterFailed:
                    devMsg = Resource.DevMsg_FilterFailed;
                    userMsg = Resource.UserMsg_FilterFailed;
                    errorCode = AccountErrorCode.FilterFailed;
                    break;
            }

            return new ErrorResult(
                errorCode,
                devMsg,
                userMsg,
                moreInfo);
        }
    }
}
