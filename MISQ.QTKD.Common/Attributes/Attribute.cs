namespace MISA.QTKD.Common.Attributes
{
    /// <summary>
    /// Atribute dùng dể xác định 1 prop là khóa chính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimarykeyAttribute : Attribute
    {
 
    }


    /// <summary>
    /// Atribute xác định prop không được để trống
    /// createdby: LTQN(24/9/2022)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IsNotNullOrEmptyAttribute : Attribute
    {
        public string Msg;

        public IsNotNullOrEmptyAttribute(string msg)
        {
            this.Msg = msg;
        }
    }
    /// <summary>
    /// Atribute kiểm tra định dạng mail
    /// createdby: LTQN(25/9/2022)
    /// </summary>
    public class IsNotEmailAttribute : Attribute
    {
        public string msg;

        public IsNotEmailAttribute(string msg)
        {
            this.msg = msg;
        }
    }

    /// <summary>
    /// Atribute kiểm tra mã nhân viên phải kết thúc là một số
    /// createdby: LTQN(25/9/2022)
    /// </summary>
    public class IsNotCodeAttribute : Attribute
    {
        public string msg;

        public IsNotCodeAttribute(string msg)
        {
            this.msg = msg;
        }
    }

    /// <summary>
    /// Attribute xác định ngày sửa
    /// </summary>
    public class ModifiedDateAttribute : Attribute
    {
    }

    /// <summary>
    /// Attribute validate ngày tháng
    /// </summary>

    public class DateAttribute : Attribute
    {

        public string msg;

        public DateAttribute(string msg)
        {
            this.msg = msg;
        }
    }

    /// <summary>
    /// Atribute xuất khẩu
    /// </summary>
    public class ExportAttribute : Attribute
    {
        public string msg;

        public ExportAttribute(string msg)
        {
            this.msg = msg;
        }
    }
}
