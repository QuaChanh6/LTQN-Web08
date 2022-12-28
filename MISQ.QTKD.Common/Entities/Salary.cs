using MISA.QTKD.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.Common.Entities
{
    public class Salary
    {
        [PrimarykeyAttribute]
        public Guid SalaryID { get; set; }

        public string SalaryCode { get; set; }

        public int SalaryMonthly { get; set; }

        public int AdvanceMoney { get; set; }

        public int Bonus { get; set; }

        public string EmployeeName { get; set; }

        public int NumberWork { get; set; }
        public int BonusDate { get; set; }


        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        public string? AccountBank { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? NameOfBank { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        public string? BranchOfBank { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        [ModifiedDate]
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Người sửa
        /// </summary>
        public string? ModifiedBy { get; set; } = "LTQN";

        public int? Dayoff { get; set; }

        public int? Allowance { get; set; }

        public string? Month { get; set; }
    }
}
