using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Office.Interop.Excel;
using MISA.QTKD.Common;
using MISA.QTKD.Common.Entities;
using MISA.QTKD.Common.Enum;
using MISA.QTKD.Common.Enums;
using MISA.QTKD.Common.Resources;
using MISA.Web08.BL;
using MySqlConnector;
using NLog;
using System.Runtime.InteropServices;

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
        public IActionResult Records([FromQuery] string? keyword)
        {
            try
            {
                var records = _baseBL.GetAll(keyword);

                return StatusCode(StatusCodes.Status200OK, records);
            }
            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception, Resource.MoreInfo);
                handleError.SaveError(ex, er.ToStringMsg(HttpContext.TraceIdentifier));

                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_Exception);
            }


        }

        /// <summary>
        /// APT lấy thông tin một nhân viên theo ID
        /// CreatedBy: LTQN(16/9/2022)
        /// </summary>
        /// <param name="employeeID">ID nhân viên</param>
        /// <returns>thông tin một nhân viên</returns>
        [HttpGet("{id}")]
        public IActionResult Records([FromRoute] Guid id)
        {
            try
            {
                var employee = _baseBL.GetById(id);

                return StatusCode(StatusCodes.Status200OK, employee);
            }
            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception, Resource.MoreInfo);
                handleError.SaveError(ex, er.ToStringMsg(HttpContext.TraceIdentifier));

                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_Exception);
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
            [FromQuery] int limit, [FromQuery] int pageNumber, [FromQuery] string? department, [FromQuery] string? position)
        {
            try
            {
                PagingData<T> emp = _baseBL.Filter(keyword, sort, limit, pageNumber, department, position);

                // Xử lý kết quả trả về từ DB
                if (emp.Data.Count > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, emp);
                }
                else
                {
                    ErrorResult er = handleError.setErrorCode(TypeOfError.Exception, Resource.MoreInfo);
                    handleError.SaveError(null, er.ToStringMsg(HttpContext.TraceIdentifier));
                    return StatusCode(StatusCodes.Status400BadRequest, Resource.UserMsg_FilterFailed);
                }
            }
            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception, Resource.MoreInfo);
                handleError.SaveError(ex, er.ToStringMsg(HttpContext.TraceIdentifier));

                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_Exception);
            }


        }
        #endregion

        #region API Post
        /// <summary>
        /// Thêm 1 bản ghi
        /// CreatedBy: LTQn(16/9/2022)
        /// </summary>
        /// <param name="T">Bản ghi được thêm</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Records([FromBody] T record)
        {
            try
            {
                var recordID = _baseBL.Insert(record);

                // dữ liệu hợp lệ
                if (Object.ReferenceEquals(recordID.GetType(), typeof(Response)))
                {   

                    Response res = (Response)recordID;
                    //sửa thành công
                    if (res.Id != Guid.Empty)
                    {
                        return StatusCode(StatusCodes.Status201Created, res.Id);
                    }
                    //sửa thất bại
                    else
                    {
                        //bắt exception
                        if (res.Ex != null)
                        {
                            MySqlException mySqlException = (MySqlException)res.Ex;
                            ErrorResult er = handleError.setErrorCode(TypeOfError.Exception, Resource.MoreInfo);

                            if (mySqlException.ErrorCode == MySqlErrorCode.DataTooLong)
                            {
                                er = handleError.setErrorCode(TypeOfError.DataTooLong, Resource.MoreInfo);
                                handleError.SaveError(mySqlException, er.ToStringMsg(HttpContext.TraceIdentifier));

                                return StatusCode(StatusCodes.Status400BadRequest, Resource.UserMsg_DataTooLong);
                            }

                        }

                        return StatusCode(StatusCodes.Status400BadRequest, Resource.UserMsg_InsertFailed);
                    }
                }
                else //dữ liệu không hợp lệ 
                {
                    if (Object.ReferenceEquals(recordID.GetType(), typeof(string))) // check mã trùng
                    {
                        ErrorResult er = handleError.setErrorCode(TypeOfError.DuplicateCode, Resource.MoreInfo);
                        handleError.SaveError(null, er.ToStringMsg(HttpContext.TraceIdentifier));
                        return StatusCode(StatusCodes.Status400BadRequest, recordID);
                    }
                    else
                    {
                        ErrorResult er = handleError.setErrorCode(TypeOfError.ValidateFailed, string.Join(", ", (List<string>)recordID));
                        handleError.SaveError(null, er.ToStringMsg(HttpContext.TraceIdentifier));
                        return StatusCode(StatusCodes.Status400BadRequest, Resource.UserMsg_ValidateFailed);
                    }

                }

            }

            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception, Resource.MoreInfo);
                handleError.SaveError(ex, er.ToStringMsg(HttpContext.TraceIdentifier));

                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_Exception);


            }



        }

        #endregion

        #region API Put
        /// <summary>
        /// Sửa thông tin 1 bản ghi
        /// CreatedBy: LTQN (16/9/2022)
        /// </summary>
        /// <param name="record">thông tin bản ghi</param>
        /// <returns>ID bản ghi được sửa</returns>
        [HttpPut("{recordId}")]
        public IActionResult Record([FromBody] T record)
        {
            try
            {
                var recordID = _baseBL.Edit(record);

                // dữ liệu hợp lệ
                if (Object.ReferenceEquals(recordID.GetType(), typeof(Response)))
                {
                    Response res = (Response)recordID;
                    //sửa thành công
                    if (res.Id != Guid.Empty)
                    {

                        return StatusCode(StatusCodes.Status200OK, res.Id);
                    }
                    //sửa thất bại
                    else
                    {
                        //bắt exception
                        if(res.Ex != null)
                        {
                            MySqlException mySqlException = (MySqlException)res.Ex;
                            ErrorResult er = handleError.setErrorCode(TypeOfError.Exception, Resource.MoreInfo);
                            if (mySqlException.ErrorCode == MySqlErrorCode.DataTooLong)
                            {
                                er = handleError.setErrorCode(TypeOfError.DataTooLong, Resource.MoreInfo);
                                handleError.SaveError(mySqlException, er.ToStringMsg(HttpContext.TraceIdentifier));

                                return StatusCode(StatusCodes.Status400BadRequest, Resource.UserMsg_DataTooLong);
                            }

                        }

                        return StatusCode(StatusCodes.Status400BadRequest, Resource.UserMsg_UpdateFailed);
                    }
                }
                else //dữ liệu không hợp lệ 
                {
                    if (Object.ReferenceEquals(recordID.GetType(), typeof(string))) // check mã trùng
                    {
                        ErrorResult er = handleError.setErrorCode(TypeOfError.DuplicateCode, Resource.MoreInfo);
                        handleError.SaveError(null, er.ToStringMsg(HttpContext.TraceIdentifier));
                        return StatusCode(StatusCodes.Status400BadRequest, recordID);
                    }
                    else
                    {
                        ErrorResult er = handleError.setErrorCode(TypeOfError.ValidateFailed, string.Join(", ", (List<string>)recordID));
                        handleError.SaveError(null, er.ToStringMsg(HttpContext.TraceIdentifier));
                        return StatusCode(StatusCodes.Status400BadRequest, Resource.UserMsg_ValidateFailed);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception, Resource.MoreInfo);
                handleError.SaveError(ex, er.ToStringMsg(HttpContext.TraceIdentifier));
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_Exception);
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
                    ErrorResult er = handleError.setErrorCode(TypeOfError.DeleteFailed, Resource.MoreInfo);
                    handleError.SaveError(null, er.ToStringMsg(HttpContext.TraceIdentifier));

                    return StatusCode(StatusCodes.Status400BadRequest, Resource.UserMsg_DeleteFailed);

                }
            }
            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception, Resource.MoreInfo);
                handleError.SaveError(ex,er.ToStringMsg(HttpContext.TraceIdentifier));

                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_Exception);
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
        /// <param name="er">ErrorResult</param>
        public static void SaveError(Exception? ex, string er)
        {
            Logger logger = LogManager.GetLogger("fileLogger");
            logger.Error(ex, er);
 

        }

        /// <summary>
        /// Hàm tạo ErrorCode
        /// CreatedBy: LTQN(29/9/2022)
        /// </summary>
        /// <param name="type">Loại lỗi</param>
        /// <returns>ErrorCode</returns>
        public static ErrorResult setErrorCode(TypeOfError type, string moreInfo)
        {
            string devMsg = "";
            string userMsg = "";
            //string moreInfo = Resource.MoreInfo;
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
