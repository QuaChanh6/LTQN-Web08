using MISA.QTKD.Common.Attributes;

namespace MISA.QTKD.Common.Entities
{   /// <summary>
    ///  Đơn vị
    ///  CreatedBy: LTQN (18/9/2022)
    /// </summary>
    public class Department
    {
        /// <summary>
        /// ID đơn vị
        /// </summary>
        [Primarykey]
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; } = "LTQN";


        /// <summary>
        /// Ngày sửa
        /// </summary>
        [ModifiedDate]
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Người sửa
        /// </summary>
        public string? ModifiedBy { get; set; } = "LTQN";

        /// <summary>
        /// Quản lý
        /// </summary>
        public string Manager { get; set; }
    }
}
