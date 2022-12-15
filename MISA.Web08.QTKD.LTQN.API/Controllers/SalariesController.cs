﻿using DocumentFormat.OpenXml.Wordprocessing;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QTKD.Common.Entities;
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
                for (int i = 0; i < 4; i++)
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

                    if (currentRow == null || currentRow.Cells == null || currentRow.Cells.Count() != 4)
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

    }
}