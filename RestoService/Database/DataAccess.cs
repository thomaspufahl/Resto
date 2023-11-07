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

        public void Execute()
        {
            ExecuteReader();
        }
        public void Execute(bool isNonQuery)
        {
            if (isNonQuery)
            {
                ExecuteNonQuery();
            }
            else
            {
                ExecuteReader();
            }
        }

        public void CloseConnection()
        {
            if (Reader != null && !Reader.IsClosed) Reader.Close();

            _Conex.Close();
        }

        private void ExecuteReader()
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

        private void ExecuteNonQuery()
        {
            _Cmd.Connection = _Conex;

            try
            {
                _Conex.Open();
                _Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw new Exception("Error ocurring at database conex", ex);
            }
        }
    }
}
