using ClosedXML.Excel;
using Dapper;
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
                var records = _employeeBL.GetAll();

                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = "emp.xlsx";

                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Employee");

                    worksheet.Cell("A1").Value = "DANH SÁCH NHÂN VIÊN";
                    var range = worksheet.Range("A1:I1");
                    worksheet.Range("A1:I1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    range.Merge().Style.Font.SetBold().Font.FontSize = 24;

                   // range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    var props = typeof(Employee).GetProperties();

                    int column = 0;
                    //ghi header
                    for (int i = 0; i < props.Length; i++)
                    {
                        var isExport = (ExportAttribute?)Attribute.GetCustomAttribute(props[i], typeof(ExportAttribute));
                        if (isExport != null)
                        {
                            column++;
                            worksheet.Cell(3, column).Value = props[i].Name;
                            //set đường viền
                           // worksheet.Cell(3, column).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            //worksheet.Cell(3, column).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        }

                    }
                    //set background
                   
                    worksheet.Range("A3:J3").Style.Fill.BackgroundColor = XLColor.Gainsboro;
                    List<Employee> listRecords = records.ToList();
                    for (int i = 0; i < listRecords.Count; i++) //duyệt bản ghi
                    {
                        column = 0;
                        for (int j = 0; j < props.Length; j++) //duyệt từn prop
                        {
                            var isExport = (ExportAttribute?)Attribute.GetCustomAttribute(props[j], typeof(ExportAttribute));
                            if (isExport != null)
                            {
                                column++;
                                var propValue = props[j].GetValue(listRecords[i], null);

                                worksheet.Cell(i + 4, column).Value = propValue?.ToString();

                                //set đường viền
                               // worksheet.Cell(i + 4, column).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                
                            }

                        }
                    }
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

        [HttpPost("deletion-requests")]
        public IActionResult Records([FromBody] string ids)
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
    }
   
}
