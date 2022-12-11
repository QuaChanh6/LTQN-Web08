using MISA.QTKD.Common.Attributes;
using MISA.QTKD.Common.Enum;
using MISA.QTKD.Common.Enums;

namespace MISA.QTKD.Common.Entities
{   /// <summary>
    /// Nhân viên
    /// createBy: LTQN(16/9/2022)
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Id nhân viên
        /// </summary>
        [Primarykey]
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [IsNotNullOrEmpty("Mã nhân viên không được để trống."), IsNotCode("Mã nhân viên phải kết thúc là một số."), Export("Mã nhân viên"), Duplicate]
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [IsNotNullOrEmpty("Tên nhân viên không được để trống."), Export("Tên nhân viên")]
        public string? EmployeeName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [Date("Ngày sinh không được lớn hơn hiện tại."), Export("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        [Export("Giới tính")]
        public string? GenderName { get; set; }

        /// <summary>
        /// Số chứng minh nhân dân/ căn cước công dân
        /// </summary>
        [Export("Chứng minh nhân dân")]
        public string? IdentityNumber { get; set; }


        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        [IsNotNullOrEmpty("Đơn vị không được để trống."), Export("Tên đơn vị")]
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Chức vụ
        /// </summary>
        [Export("Chức vụ")]
        public string? PositionName { get; set; }

        /// <summary>
        /// ID chức vụ
        /// </summary>
        public Guid? PositionID { get; set; }


        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }
        
        /// <summary>
        /// Email
        /// </summary>
        [Export("Email")]
        [IsNotEmail("Không đúng định dạng email"),]
        public string? Email { get; set; }

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
        /// Trạng thái
        /// </summary>
        public Status Status { get; set; }

        [Export("Tình trạng công việc")]
        public string StatusName   { get; set; }

        [Salary]
        public Guid SalaryID { get; set; }
    }
}
