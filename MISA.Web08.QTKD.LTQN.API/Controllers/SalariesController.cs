using DocumentFormat.OpenXml.Wordprocessing;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QTKD.Common.Entities;
using MISA.QTKD.Common.Enums;
using MISA.QTKD.Common.Resources;
using MISA.Web08.BL;
using MISA.Web08.BL.SalaryBL;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;



namespace MISA.Web08.QTKD.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SalariesController : BaseController<Salary>
    {
        private ISalaryBL _salBL;
        public SalariesController(ISalaryBL salBL) : base(salBL)
        {
            _salBL = salBL;
        }

        [HttpPost("salary-data")]
        public async Task<ActionResult> Upload([FromForm] IFormFile file)
        {
            try
            {
                //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                //var data="";
                //using (var stream = new MemoryStream())
                //{
                //    file.CopyTo(stream);
                //    stream.Position = 0;
                //    using (var reader = ExcelReaderFactory.CreateReader(stream))
                //    {

                //        while (reader.Read()) //Each row of the file
                //        {
                //            data = reader.GetValue(0).ToString();

                //        }
                //    }
                //}

                List<List<String>> data = new List<List<String>>();
                var inputstream = file.OpenReadStream();

                XSSFWorkbook workbook = new XSSFWorkbook(inputstream);

                ISheet sheet = workbook.GetSheetAt(0);
                // Example: var firstCellRow = (int)sheet.GetRow(0).GetCell(0).NumericCellValue;
                for (int i = 0; i < 3; i++)
                {
                    List<string> list1 = new List<string>();
                    data.Add(list1);
                }

                if (sheet.GetRow(0).GetCell(1) == null | sheet.GetRow(0).GetCell(1).NumericCellValue < 0 | sheet.GetRow(0).GetCell(1).NumericCellValue > 12)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Format file không đúng!");
                }
                for (int rowIdx = 2; rowIdx <= sheet.LastRowNum; rowIdx++)
                {
                    IRow currentRow = sheet.GetRow(rowIdx);

                    if (currentRow == null || currentRow.Cells == null || currentRow.Cells.Count() != 3)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "Format file không đúng!");
                    }

                    var df = new DataFormatter();

                    for (int cellNumber = 0; cellNumber < currentRow.Cells.Count(); cellNumber++)
                    {
                        var cell = currentRow.Cells[cellNumber];

                        data[rowIdx - 2].Add(df.FormatCellValue(cell));
                    }
                    //business logic & saving data to DB                        
                }

                int result = _salBL.setDataImport(data);
                if(result > 0)
                {
                return StatusCode(StatusCodes.Status201Created, result);
                }
                return StatusCode(StatusCodes.Status400BadRequest, "Dữ liệu không đúng định dạng!");

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Format file không đúng!");
            }
        }

        [HttpGet]
        [Route("SalaryManager/{deparment}")]
        public IActionResult RecordsManager ([FromRoute] Guid deparment, [FromQuery] string? keyword)
        {
            try
            {
                var records = _salBL.GetAllSalary(deparment, keyword);

                return StatusCode(StatusCodes.Status200OK, records);
            }
            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception, Resource.MoreInfo);
                handleError.SaveError(ex, er.ToStringMsg(HttpContext.TraceIdentifier));

                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_Exception);
            }


        }



        [HttpGet("code/{code}/{month}")]
        public IActionResult RecordCode([FromRoute] string code, [FromRoute] string? month)
        {
            try
            {
                var employee = _salBL.GetByCode(code, month);

                return StatusCode(StatusCodes.Status200OK, employee);
            }
            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception, Resource.MoreInfo);
                handleError.SaveError(ex, er.ToStringMsg(HttpContext.TraceIdentifier));

                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_Exception);
            }
        }


        [HttpPut("salary-reset")]
        public IActionResult Reset()
        {
            try
            {
                var employee = _salBL.reset();

                return StatusCode(StatusCodes.Status200OK, employee);
            }
            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception, Resource.MoreInfo);
                handleError.SaveError(ex, er.ToStringMsg(HttpContext.TraceIdentifier));

                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_Exception);
            }
        }



        [HttpGet("file-example")]
        public IActionResult GetFileById()
        {
            string path = "ImportSalary.xlsx";


         if (System.IO.File.Exists(path))
            {
                return File(System.IO.File.OpenRead(path),  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Path.GetFileName(path));
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Có lỗi xảy ra!");
        }


        [HttpGet]
        [Route("dayoff/{code}")]
        public IActionResult DayOff([FromRoute] string code)
        {
            try
            {
                var records = _salBL.GetDayOff(code);

                return StatusCode(StatusCodes.Status200OK, records);
            }
            catch (Exception ex)
            {
                ErrorResult er = handleError.setErrorCode(TypeOfError.Exception, Resource.MoreInfo);
                handleError.SaveError(ex, er.ToStringMsg(HttpContext.TraceIdentifier));

                return StatusCode(StatusCodes.Status500InternalServerError, Resource.UserMsg_Exception);
            }


        }

    }
}
