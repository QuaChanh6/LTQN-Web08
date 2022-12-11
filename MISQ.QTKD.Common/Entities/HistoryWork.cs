using MISA.QTKD.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.Common.Entities
{
    public class HistoryWork
    {
        [idForRecord]
        public string? EmployeeCode { get; set; }

        public string? PositionW { get; set; }

        public string? DepartmentW { get; set; }

        [ModifiedDate]
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}
