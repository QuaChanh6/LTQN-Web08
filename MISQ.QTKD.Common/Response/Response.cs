using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.Common
{
    /// <summary>
    /// Trả về kq từ db
    /// </summary>
    public class Response
    {
        #region Fields
        private Guid _id;
        private Exception? _exception;
        #endregion


        #region Props
        /// <summary>
        /// Exception
        /// </summary>
        public Exception? Ex
        {
            get { return _exception; }
        }

        /// <summary>
        /// Thông tin trả về
        /// </summary>
        public Guid Id { get { return _id; } } 
        #endregion


        #region Contructor

        public Response(Guid id)
        {
            _id = id;
        }
        public Response(Exception ex, Guid id)
        {
            _exception = ex;
            _id = id;
        } 
        #endregion



    }
}
