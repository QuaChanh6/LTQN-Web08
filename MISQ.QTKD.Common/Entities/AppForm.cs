using MISA.QTKD.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.Common.Entities
{
    public class AppForm
    {
        [Primarykey]
        public Guid id { get; set; }
        public int? type { get; set; }

        public string? employeeCode { get; set; }

        public string? reason { get; set; }

        public DateTime? dateStart { get; set; }

        public DateTime? dateEnd { get; set; }

        public string? manager { get; set; }

        public int? status { get; set; }  

        public DateTime? createdDate { get; set; } = DateTime.Now;

        public string? employeeName { get; set; }

        public int? money { get; set; }

    }
}
