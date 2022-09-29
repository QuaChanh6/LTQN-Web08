using MISA.QTKD.Common.Attributes;
using MISA.QTKD.Common.Enum;

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
        [IsNotNullOrEmpty("Mã nhân viên không được để trống"), IsNotCode("Mã nhân viên phải kết thúc là một số")]
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [IsNotNullOrEmpty("Tên nhân viên không được để trống")]
        public string? EmployeeName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public string? GenderName { get; set; }

        /// <summary>
        /// Số chứng minh nhân dân/ căn cước công dân
        /// </summary>
        public string? IdentityNumber { get; set; }


        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public Guid DepartmentID   { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Nơi cấp chứng minh nhân dân
        /// </summary>
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Ngày cấp chúng minh nhân dân
        /// </summary>
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
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Người sửa
        /// </summary>
        public string? ModifiedBy { get; set; } = "LTQN";

        /// <summary>
        /// Chức vụ
        /// </summary>
        public string? Position { get; set; }


    }
}
