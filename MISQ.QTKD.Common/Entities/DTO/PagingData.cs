namespace MISA.QTKD.Common.Entities
{
    /// <summary>
    /// kết quả phân trang/ lọc/ tìm kiếm
    /// CreatedBy: LTQN
    /// (19/9/2022)
    /// </summary>
    /// <typeparam name="T">Đối tượng employee</typeparam>
    public class PagingData<T>
    {
        /// <summary>
        /// //bản ghi thỏa mãn điều kiện
        /// </summary>
        public List<T> Data { get; set; } = new List<T>();

        /// <summary>
        /// // tổng số bản ghi thỏa mãi điều kiện
        /// </summary>
        public int TotalCount { get; set; } 

    }
}
