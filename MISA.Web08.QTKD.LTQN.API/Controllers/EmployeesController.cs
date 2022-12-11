using ClosedXML.Excel;
using Dapper;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Mvc;
using MISA.QTKD.Common.Attributes;
using MISA.QTKD.Common.Entities;
using MISA.QTKD.Common.Enum;
using MISA.QTKD.Common.Enums;
using MISA.QTKD.Common.Resources;
using MISA.Web08.BL;
using MySqlConnector;
using NLog;
using NLog.Web;
using System.Collections.Generic;
using System.Globalization;

namespace MISA.Web08.QTKD.API.Controllers
{

    /// <summary>
    /// API employee
    /// CreatedBy: LTQN(16/9/2022)
    /// </summary>

    [ApiController]
    public class EmployeesController : BaseController<Employee>
    {
        private IEmployeeBL _employeeBL;


        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }

        /// <summary>
        /// Láy mã nhân viên lớn nhất
        /// CreatedBy: LTQN (16/9/2022)
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        [HttpGet("new-code")]
        public IActionResult NewCodeEmployee()
        {
            try
            {
                var newCode = _employeeBL.GetNewCodeEmployee();
                // Trả về dữ liệu cho client
                return StatusCode(StatusCodes.Status200OK, newCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                                   new ErrorResult(
                                            AccountErrorCode.Exception,
                                            Resource.DevMsg_Exception,
                                            Resource.UserMsg_Exception,
                                            Resource.MoreInfo
                                        )
                                 );
            }

        }

        /// <summary>
        /// Xuất khẩu 
        /// Createdby: LTQN(1/10/2022)
        /// </summary>
        /// <returns></returns>
        [HttpGet("data-export")]
        public IActionResult FileExport()
        {
            try
            {
                var records = _employeeBL.GetAll("");

                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = "emp.xlsx";

                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Nhân viên");

                    worksheet.Cell("A1").Value = "DANH SÁCH NHÂN VIÊN";
                    var range = worksheet.Range("A1:K1");
                    worksheet.Range("A1:K1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    range.Merge().Style.Font.SetBold().Font.FontSize = 24;

                   // range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    var props = typeof(Employee).GetProperties();

                    int column = 1;
                    int row = 0;
                    //cột stt
                    worksheet.Cell(3, column).Value = "STT";
                    worksheet.Cell(3, column).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    //ghi header
                    for (int i = 0; i < props.Length; i++)
                    {
                        var isExport = (ExportAttribute?)Attribute.GetCustomAttribute(props[i], typeof(ExportAttribute));
                        if (isExport != null)
                        {
                            column++;
                            worksheet.Cell(3, column).Value = isExport.msg;
                            worksheet.Cell(3, column).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        }

                    }
                    //set background
                   
                    worksheet.Range("A3:J3").Style.Fill.BackgroundColor = XLColor.Gainsboro;
                    List<Employee> listRecords = records.ToList();
                    for (int i = 0; i < listRecords.Count; i++) //duyệt bản ghi
                    {
                        column = 1;
                        row++;
                        worksheet.Cell(i + 4, column).Value = row;
                        worksheet.Cell(i + 4, column).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        for (int j = 0; j < props.Length; j++) //duyệt từn prop
                        {
                            var isExport = (ExportAttribute?)Attribute.GetCustomAttribute(props[j], typeof(ExportAttribute));
                            if (isExport != null)
                            {
                                column++;
                                var propValue = props[j].GetValue(listRecords[i], null);
                                
                                var date = (DateAttribute?)Attribute.GetCustomAttribute(props[j], typeof(DateAttribute));
                                if(date != null && propValue != null)
                                {
                                    string? dateOfBirth = propValue.ToString();
                                    if(dateOfBirth != null)
                                    {
                                        string[] dateString = dateOfBirth.Split('/'); 
                                        string day = dateString[1];
                                        string month = dateString[0];
                                        string year = dateString[2].Remove(4);

                                        propValue = day + "/" + month + "/" + year;

                                    }

                                }

                                worksheet.Cell(i + 4, column).Value = propValue?.ToString();

                                //set đường viền
                               worksheet.Cell(i + 4, column).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                
                            }

                        }
                    }
                    worksheet.Column("D").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    //set chiều rộng ứng với độ dài giá trị
                    worksheet.Columns().AdjustToContents();
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception, Resource.MoreInfo);
                handleError.SaveError(ex, er.ToStringMsg(HttpContext.TraceIdentifier));

                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        //[HttpPost("deletion-requests")]
        //public IActionResult Records([FromBody] string ids)
        //{

        //    try
        //    {
        //        var rowEffected = _employeeBL.DeleteMultiple(ids);
        //        if (rowEffected > 0)
        //        {
        //            // Trả về dữ liệu cho client
        //            return StatusCode(StatusCodes.Status200OK, ids);
        //        }
        //        else
        //        {
        //            return StatusCode(StatusCodes.Status400BadRequest, Resource.UserMsg_DeleteFailed);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw;
        //    } 
        //}

        [HttpPost("deletion-requests")]
        public IActionResult Records([FromBody] List<string> ids)
        {

            try
            {
                var rowEffected = _employeeBL.DeleteMultiple(ids);
                if (rowEffected > 0)
                {
                    // Trả về dữ liệu cho client
                    return StatusCode(StatusCodes.Status200OK, ids);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, Resource.UserMsg_DeleteFailed);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        [HttpGet("countGender")]
        public IActionResult Department()
        {
            try
            {
                var result = _employeeBL.getCountGender();

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_Exception);
            }
        }


        [HttpGet("countStatus")]
        public IActionResult Status()
        {
            try
            {
                var result = _employeeBL.getCountStatus();

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_Exception);
            }
        }

        [HttpPost("File/{employeeCode}")]
        [Obsolete]
        public IActionResult FileAttach([FromForm] IFormFile file, [FromRoute] string employeeCode)
        {

            string[] Scopes = { Google.Apis.Drive.v3.DriveService.Scope.Drive };
            string ApplicationName = "ManageEmployee";

            UserCredential credential;
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {

                // Thông tin về quyền truy xuất dữ liệu của người dùng được lưu ở thư mục token.json
                string credPath = "token.json";

                // Yêu cầu người dùng xác thực lần đầu và thông tin sẽ được lưu vào thư mục token.json
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,  // Quyền truy xuất dữ liệu của người dùng
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;

                Console.WriteLine("Credential file saved to: " + credPath);

                // Tạo ra 1 dịch vụ Drive API - Create Drive API service với thông tin xác thực và ApplicationName
                var driveService = new Google.Apis.Drive.v3.DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });


                // ID thư mục file, các bạn thay bằng ID của các bạn khi chạy
                var folderId = "1hnkn1I9SgMX2Vn2lf0M3GkGTbpe5luM1";
                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = "hoctoantap.jpg",

                    // Thư mục chưa file
                    Parents = new List<string>{folderId}
 

                };

                // Đường dẫn file trong thiết bị của bạn, dùng để upload lên Goolge Drive
                string pathFile = "hoctoantap.com.jpg";


                Google.Apis.Drive.v3.FilesResource.CreateMediaUpload request;
                using (var streamFile = file.OpenReadStream())
                {
                    request = driveService.Files.Create(fileMetadata, streamFile, null);

                    // Cấu hình thông tin lấy về là ID
                    request.Fields = "id";

                    // thực hiện Upload
                    request.Upload();
                }

                // Trả về thông tin file đã được upload lên Google Drive
                var fileOutPut = request.ResponseBody;

                Console.WriteLine("File ID: " + fileOutPut.Id);
                //Name a;
                //a.call();
                return StatusCode(StatusCodes.Status200OK);

            }
        }

    }
    
}
