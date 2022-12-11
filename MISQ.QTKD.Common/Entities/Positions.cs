using MISA.QTKD.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.Common.Entities
{
    public class Position
    {
        /// <summary>
        /// Mã vị trí
        /// </summary>
        [Primarykey]
        public Guid PositionID { get; set; }

        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Mã vị trí
        /// </summary>
        public string PositionCode { get; set; }

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
