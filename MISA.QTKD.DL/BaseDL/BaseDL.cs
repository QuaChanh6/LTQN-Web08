using Dapper;
using MISA.QTKD.Common;
using MISA.QTKD.Common.Attributes;
using MISA.QTKD.Common.Entities;
using MISA.QTKD.Common.Resources;
using MySqlConnector;
using System.Reflection;

namespace MISA.QTKD.DL
{
    /// <summary>
    /// Base Data layer
    /// CreatedBy: LTQN(28/9/2022)
    /// </summary>
    /// <typeparam name="T">Generic</typeparam>
    public class BaseDL<T> : IBaseDL<T>
    {

        #region Method
        /// <summary>
        /// Xóa thông tin bản ghi
        /// CreatedBy: LTQN(29/9/2022)
        /// </summary>
        /// <param name="ID">Id bản ghi cần xóa</param>
        /// <returns>thành công trả về id, thất bại trả về rỗng</returns>
        public Guid Delete(Guid ID)
        {

            string storedProceduceName = String.Format(Resource.Proc_Delete, typeof(T).Name);

            var parameters = new DynamicParameters();

            string id = $"v_{typeof(T).Name}ID";
            //parameters.Add("v_EmployeeID", ID);
            parameters.Add(id, ID);

            MySqlTransaction transaction = null;
            //khởi tạo kết nối tới db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                connect.Open();
                transaction = connect.BeginTransaction();

                try
                {
                    int numberOfAffectedRows = connect.Execute(storedProceduceName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure); ;

                    transaction.Commit();

                  
                    if (numberOfAffectedRows >= 0) 
                    {
                        return ID;
                    }
                    else 
                    {
                        return Guid.Empty;
                    }

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return Guid.Empty;
                }
                finally
                {
                    connect.Close();
                }
            }

        }

        /// <summary>
        /// Sửa bản ghi
        /// CreatedBy: LTQN(29/9/2022)
        /// </summary>
        /// <param name="record">thông tin sửa</param>
        /// <returns>thành công: id, thất bại: rỗng</returns>
        public Response Edit(T record)
        {
            //khai báo store proceduce
            string storedProceduceName = String.Format(Resource.Proc_Edit, typeof(T).Name);

            //chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            var props = typeof(T).GetProperties();


            //lấy id
            Guid id = Guid.Empty;
            foreach (var prop in props)
            {
                string propName = prop.Name;
                var propValue = prop.GetValue(record, null);

                //lấy attr primarykey
                var isPrimarykey = (PrimarykeyAttribute?)Attribute.GetCustomAttribute(prop, typeof(PrimarykeyAttribute));
                if (isPrimarykey != null && propValue != null)
                {
                    id = (Guid)propValue;
                }

                //tự tạo id nếu là sửa việc làm
                var idWork = (idForRecordAttribute?)Attribute.GetCustomAttribute(prop, typeof(idForRecordAttribute));
                if (idWork != null && propValue != null)
                {
                    id = new Guid();
                }

                // lấy attr ngày sửa
                var ModifiedDate = (ModifiedDateAttribute?)Attribute.GetCustomAttribute(prop, typeof(ModifiedDateAttribute));
                if (ModifiedDate != null)
                {
                    propValue = DateTime.Now;
                }
                string input = $"v_{propName}";
                parameters.Add(input, propValue);

            }

            MySqlTransaction transaction = null;
            //khởi tạo kết nối tới db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                connect.Open();
                transaction = connect.BeginTransaction();

                try
                {
                    int recordsEffected = connect.Execute(storedProceduceName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                    
                    transaction.Commit();
                    //xử lý dữ liệu trả về
                    // th1: thành công
                    if (recordsEffected > 0)
                    {
                        return new Response(id);
                    }
                    else  //th2: thất bại
                    {
                        return new Response(Guid.Empty);
                    }

                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return new Response(ex, Guid.Empty);
                }
                finally
                {
                    connect.Close();
                }
            }

        }

        /// <summary>
        /// Phân trang/tìm kiếm
        /// CreatedBy: LTQN(29/9/2022)
        /// </summary>
        /// <param name="keyword">Từ cần tìm</param>
        /// <param name="sort">Sắp xếp</param>
        /// <param name="limit">Số lượng bản ghi trong 1 trang</param>
        /// <param name="pageNumber">Số trang</param>
        /// <returns></returns>
        public PagingData<T> Filter(string? keyword, string? sort, int limit, int pageNumber, string? department, string? position)
        {
            //khai báo store proceduce
            string storedProceduceName = String.Format(Resource.Proc_Filter, typeof(T).Name);

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("v_Offset", (pageNumber - 1) * limit);
            parameters.Add("v_Limit", limit);
            parameters.Add("v_Sort", sort);

            var whereConditions = new List<string>();
            var code = $"{typeof(T).Name}Code";
            var name = $"{typeof(T).Name}Name";

            if (keyword != null)
            {
                whereConditions.Add($"{code} LIKE '%{keyword}%' OR {name} LIKE '%{keyword}%'");
            }
            if(department != null)
            {
                whereConditions.Add($"departmentName ='{department}'");
            }
            if (position != null)
            {
                whereConditions.Add($"positionName ='{position}'");
            }
                string whereClause = string.Join(" AND ", whereConditions);
            parameters.Add("v_Where", whereClause);


            //kết nối đến db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {

                // Thực hiện gọi vào DB để chạy stored procedure với tham số đầu vào
                var multipleResults = connect.QueryMultiple(storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về từ DB
                if (multipleResults != null)
                {
                    var records = multipleResults.Read<T>();
                    var totalCount = multipleResults.Read<int>().Single();

                    return new PagingData<T>()
                    {
                        Data = records.ToList(),
                        TotalCount = totalCount
                    };
                }
                else
                {
                    return new PagingData<T>();
                }
            }

          
        }


        /// <summary>
        /// Lấy danh sách bản ghi trong 1 bảng
        /// Createdby: LTQN(29/9/2022)
        /// </summary>
        /// <returns>danh sách các bản ghi</returns>
        public IEnumerable<T> GetAll(string? keyword)
        {

            //khai báo store proceduce
            string storedProceduceName = String.Format(Resource.Proc_GetAll, typeof(T).Name);
            var parameters = new DynamicParameters();
            if (keyword != null)
            {
                string whereClause = $"EmployeeName LIKE '%{keyword}%'";
                parameters.Add("v_Where", whereClause);
            }
            else
            {
                parameters.Add("v_Where", "");
            }
             

            //MySqlTransaction transaction = null;
            //khởi tạo kết nối tới db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                    var records = connect.Query<T>(storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                   
                    return records;
            }
 
        }

        /// <summary>
        /// Lấy thông tin bản ghi qua id
        /// CreatedBy: LTQN(29/9/2022)
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <returns>Bản ghi cần lấy</returns>
        public IEnumerable<T> GetById(Guid id)
        {

            //khai báo store proceduce
            string storedProceduceName = String.Format(Resource.Proc_GetRecord, typeof(T).Name);

            //chuẩn bị tham số đầu vào theo câu lênh
            var parameters = new DynamicParameters();
            string IdInput = $"v_{typeof(T).Name}ID";
            parameters.Add(IdInput, id);


            //khởi tạo kết nối tới db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
     
                    //thực hiện gọi db
                    var record = connect.Query<T>(storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure);


                //if(Object.ReferenceEquals(typeof(T), typeof(Employee)))
                //{
                //    IEnumerable<Employee> emps = (IEnumerable<Employee>)this.GetAll("");
                //    string query = "";
                //    foreach (Employee emp in emps)
                //    {
                //        query = query + "INSERT INTO historyWork ( EmployeeCode, PositionW, DepartmentW, DateStart)" +
                //                $"VALUES('{emp.EmployeeCode}', '{emp.PositionName}', '{emp.DepartmentName}', now()); ";
                //    }
                //    //thực hiện câu lệnh 
                //    var result1 = connect.Execute(query);

                //}

                    return record;
            }


        }

        public IEnumerable<T> GetByCode(string code)
        {

            //khai báo store proceduce
            string storedProceduceName = String.Format(Resource.Proc_GetCode, typeof(T).Name);

            //chuẩn bị tham số đầu vào theo câu lênh
            var parameters = new DynamicParameters();
            string IdInput = $"v_{typeof(T).Name}Code";
            if (Object.ReferenceEquals(typeof(T), typeof(AppForm)))
            {
                IdInput = "v_employeeCode";

            }
            parameters.Add(IdInput, code);


            //khởi tạo kết nối tới db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {

                //thực hiện gọi db
                var record = connect.Query<T>(storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return record;
            }


        }

        /// <summary>
        /// Thêm bản ghi
        /// CreatedBy: LTQN(29/9/2022)
        /// </summary>
        /// <param name="record">Bản ghi cần thêm</param>
        /// <returns>Thành công: id, thất bại: rỗng</returns>
        public Response Insert(T record)
        {
            //khai báo store proceduce
            string storedProceduceName = String.Format(Resource.Proc_Insert, typeof(T).Name);
            
            //chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            var props = typeof(T).GetProperties();
            var newID = Guid.NewGuid();
            foreach (var prop in props)
            {
                string propName = prop.Name;
                var propValue = prop.GetValue(record, null);

                //lấy attr primarykey
                var isPrimarykey = (PrimarykeyAttribute?)Attribute.GetCustomAttribute(prop, typeof(PrimarykeyAttribute));
                if (isPrimarykey != null)
                {
                    propValue = newID;
                }
                // lấy attr ngày sửa
                var modifiedDate = (ModifiedDateAttribute?)Attribute.GetCustomAttribute(prop, typeof(ModifiedDateAttribute));
                if (modifiedDate != null)
                {
                    propValue = DateTime.Now;
                }

                // lấy attr ngày sửa
                var salary = (SalaryAttribute?)Attribute.GetCustomAttribute(prop, typeof(SalaryAttribute));
                if (salary != null)
                {
                    propValue = Guid.NewGuid();
                }
                string input = $"v_{propName}";
                parameters.Add(input, propValue);

            }

            MySqlTransaction transaction = null;
            //khởi tạo kết nối tới db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                connect.Open();
                transaction = connect.BeginTransaction();

                try
                {
                    var numberOfAffectedRows = connect.Execute(storedProceduceName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                    transaction.Commit();
                    //xử lý dữ liệu trả về
                    // th1: thành công
                    if (numberOfAffectedRows > 0)
                    {
                        return new Response(newID);
                    }
                    else  //th2: thất bại
                    {
                        return new Response(Guid.Empty);
                    }

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Response(ex, Guid.Empty);
                }
                finally
                {
                    connect.Close();
                }
            }

        }

        /// <summary>
        /// Kiểm tra mã trùng
        /// CreatedBy: LTQN(9/10/2022)
        /// </summary>
        /// <param name="record">bản ghi nhân viên</param>
        /// <returns>Mã nhân viên nếu bị trùng</returns>
        public Guid checkDuplicateEmployeeCode(T record)
        {

            //khai báo store proceduce
            string storedProceduceName = "Proc_employee_FindCode";

            var parameters = new DynamicParameters();

            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {

                //lấy attr primarykey
                var isDuplicate = (DuplicateAttribute?)Attribute.GetCustomAttribute(prop, typeof(DuplicateAttribute));
                if (isDuplicate != null)
                {
                    var propValue = prop.GetValue(record, null);
                    parameters.Add("v_EmployeeCode", propValue);
                    break;
                }
                var isPrimarykey = (PrimarykeyAttribute?)Attribute.GetCustomAttribute(prop, typeof(PrimarykeyAttribute));
                if (isPrimarykey != null)
                {
                    var propValue = prop.GetValue(record, null);
                    parameters.Add("v_EmployeeID", propValue);
                }
                else parameters.Add("v_EmployeeID", Guid.Empty);



            }
           

            //kết nối đến db
            using (MySqlConnection connect = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                //thực hiện câu lệnh 
                Guid idEmp = connect.QueryFirstOrDefault<Guid>(storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Trả về dữ liệu cho client
                return idEmp;

            }
        }
        #endregion
    }


}
