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
        [IsNotNullOrEmpty("Mã nhân viên không được để trống."), IsNotCode("Mã nhân viên phải kết thúc là một số."), Export("Mã nhân viên")]
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
        public Guid DepartmentID   { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        [Export("Tên đơn vị")]
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Chức vụ
        /// </summary>
        [Export("Chức vụ")]
        public string? Position { get; set; }

        /// <summary>
        /// Nơi cấp chứng minh nhân dân
        /// </summary>
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Ngày cấp chúng minh nhân dân
        /// </summary>
        [Date("Ngày cấp không được lớn hơn hiện tại.")]
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }
        
        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public string? LandlinePhone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [IsNotEmail("Không đúng định dạng email")]
        public string? Email { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        [Export("Tài khoản ngân hàng")]
        public string? AccountBank { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [Export("Tên ngân hàng")]
        public string? NameOfBank { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        [Export("Chi nhánh ngân hàng")]
        public string? BranchOfBank { get; set; }

        /// <summary>
        /// Bổn phận
        /// </summary>
        public Duty? Duty { get; set; }

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
    }
}
