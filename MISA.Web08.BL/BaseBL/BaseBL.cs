using MISA.QTKD.Common.Attributes;
using MISA.QTKD.Common.Entities;
using MISA.QTKD.Common.Resources;
using MISA.QTKD.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Web08.BL
{
    /// <summary>
    /// Base business layer
    /// CreatedBy: LTQN(28/9/2022)
    /// </summary>
    /// <typeparam name="T">Generic</typeparam>
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Fields
        private IBaseDL<T> _baseDl;

        #endregion
        #region constructor
        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDl = baseDL;
        }
        #endregion

        #region methods


        /// <summary>
        /// Xóa 1 bản ghi
        /// Createdby: LTQN(29/9/2022)
        /// </summary>
        /// <param name="employeeID">id bản ghi cần xóa</param>
        /// <returns>id bản ghi</returns>
        public Guid Delete(Guid employeeID)
        {
            return _baseDl.Delete(employeeID);
        }

        /// <summary>
        /// Sửa 1 bản ghi
        /// Createdby: LTQN(29/9/2022)
        /// </summary>
        /// <param name="record">Bản ghi sửa</param>
        /// <returns>id bản ghi</returns>
        public Object Edit(T record)
        {
            
            List<string> validation = Validate(record);

            Guid id = _baseDl.checkDuplicateEmployeeCode(record);
            if (id != Guid.Empty)
            {
                return Resource.UserMsg_DuplicateCode;

            }
            if (validation.Count == 0)
            {
                return _baseDl.Edit(record);
            }
            else
            {
                return validation;
            }
            
            //return _baseDl.Edit(record, id);
        }


        /// <summary>
        /// Lọc/ tìm kiếm/ phân trang trong 1 bảng
        /// Createdby: LTQN(29/9/2022)
        /// </summary>
        /// <param name="keyword">Từ cần tìm</param>
        /// <param name="sort">sắp xếp</param>
        /// <param name="limit">Số lượng bản ghi trong 1 trang</param>
        /// <param name="pageNumber">số trang</param>
        /// <returns></returns>
        public PagingData<T> Filter(string? keyword, string? sort, int limit, int pageNumber)
        {
            return _baseDl.Filter(keyword, sort, limit, pageNumber);
        }

        /// <summary>
        /// Lấy danh sách bản ghi trong 1 bảng
        /// Createdby: LTQN(29/9/2022)
        /// </summary>
        /// <returns>danh sách các bản ghi</returns>
        public IEnumerable<T> GetAll()
        {
            return _baseDl.GetAll();
        }

        /// <summary>
        /// Lấy bản ghi thông qua Id
        /// Createdby: LTQN(29/9/2022)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<T> GetById(Guid id)
        {
            return _baseDl.GetById(id);
        }


        /// <summary>
        /// Thêm 1 bản ghi
        /// Createdby: LTQN(29/9/2022)
        /// </summary>
        /// <param name="record">Bản ghi cần thêm</param>
        /// <returns>id bản ghi</returns>
        public Object Insert(T record)
        {

            List<string> validation = Validate(record);
            Guid id = _baseDl.checkDuplicateEmployeeCode(record);
            if(id != Guid.Empty)
            {
                return Resource.UserMsg_DuplicateCode;

            }
            if (validation.Count == 0)
            {
                return _baseDl.Insert(record);
            }
            else
            {
                return validation;
            }
            //return _baseDl.Insert(record);
        }

        public List<string> Validate(T record)
        {

            //validate dữ liệu
            var props = typeof(T).GetProperties(); //lấy các prop của bảng
            var ValidateErrors = new List<string>(); //danh sách lỗi
            foreach (var prop in props)
            {
                var propName = prop.Name; //lấy tên của prop
                var propValue = prop.GetValue(record, null); // lấy giá trị
                 //lấy attribute của prop
                 //nếu prop có attribute IsNotNullOrEmptyAttribute thì trả về đối tượng attribute
                 // nếu không trả về null
                var isNotNullOrEmpty = (IsNotNullOrEmptyAttribute?)Attribute.GetCustomAttribute(prop, typeof(IsNotNullOrEmptyAttribute));
                //nếu có chứa attr và giá trị attr không trống
                if (isNotNullOrEmpty != null && string.IsNullOrEmpty(propValue?.ToString()))
                {
                    ValidateErrors.Add(isNotNullOrEmpty.Msg);
                }
                // định dạng mail
                var isNotEmail = (IsNotEmailAttribute?)Attribute.GetCustomAttribute(prop, typeof(IsNotEmailAttribute));
                string regexEmail = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                Regex email = new Regex(regexEmail);
                if (propValue != null && isNotEmail != null)
                {
                    if(propValue != "")
                    {
                        string emailValue = propValue.ToString();
                        //var a = email.IsMatch(emailValue);
                        if (emailValue != null && !email.IsMatch(emailValue))
                            ValidateErrors.Add(isNotEmail.msg);
                    }

                    
                }
                // validate ngày tháng
                var isDate = (DateAttribute?)Attribute.GetCustomAttribute(prop, typeof(DateAttribute));
                if(isDate != null && propValue != null)
                {
                    if((DateTime)propValue > DateTime.Now)
                    {
                        ValidateErrors.Add(isDate.msg);
                    }

                } 

                //định dạng mã nhân viên
                var isNotCode = (IsNotCodeAttribute?)Attribute.GetCustomAttribute(prop, typeof(IsNotCodeAttribute));
                if (isNotCode != null)
                {
                    var code = propValue?.ToString();
                    if (code != null && !Char.IsDigit(code[code.Length - 1]))
                        ValidateErrors.Add(isNotCode.msg);
                }
            }
            return ValidateErrors;

        }
        #endregion
    }


  
}
