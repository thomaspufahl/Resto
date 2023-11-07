using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoService.Database
{
    internal class DataAccess
    {
        private readonly string _ConnectionString = Properties.Settings.Default.Conex;
        private SqlConnection _Conex;
        private SqlCommand _Cmd;

        public SqlDataReader Reader { get; private set; }

        public DataAccess()
        {
            _Conex = new SqlConnection();
            _Cmd = new SqlCommand();

            _Conex.ConnectionString = _ConnectionString;
        }

        public void SetQuery(string query)
        {
            _Cmd.CommandType = System.Data.CommandType.Text;
            _Cmd.CommandText = query;
        }

        public void SetProc(string procName)
        {
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.CommandText = procName;
        }

        public void SetParam(string paramName, object value)
        {
            _Cmd.Parameters.AddWithValue(paramName, value);
        }

        public void CloseConnection()
        {
            if (Reader != null && !Reader.IsClosed) Reader.Close();

            _Conex.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// A SqlDataReader object.
        /// </returns>
        /// <exception cref="Exception"></exception>
        public void ExecuteReader()
        {
            _Cmd.Connection = _Conex;

            try
            {
                _Conex.Open();
                Reader = _Cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw new Exception("Error ocurring at database conex", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// The number of rows affected.
        /// </returns>
        /// <exception cref="Exception"></exception>
        public int ExecuteNonQuery()
        {
            _Cmd.Connection = _Conex;

            try
            {
                _Conex.Open();
                return _Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw new Exception("Error ocurring at database conex", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// The first column of the first row in the result set, or a null reference if the result set is empty.
        /// </returns>
        /// <exception cref="Exception"></exception>
        public object ExecuteScalar()
        {
            _Cmd.Connection = _Conex;

            try
            {
                _Conex.Open();
                return _Cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw new Exception("Error ocurring at database conex", ex);
            }
        }
    }
}
