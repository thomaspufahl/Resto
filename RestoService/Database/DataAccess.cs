using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoService.Database
{
    internal class DataAccess
    {
        private readonly string _ConnectionStringP = "server=localhost, 1433; database=Resto; user=sa; password=ThomasPufahl1525";
        private readonly string _ConnectionStringN = "server=localhost, 1433; database=Resto; user=sa; password=Verdeverde8";
        private SqlConnection _Conex;
        private SqlCommand _Cmd;

        public SqlDataReader Reader { get; private set; }

        public DataAccess()
        {
            _Conex = new SqlConnection();
            _Cmd = new SqlCommand();

            _Conex.ConnectionString = _ConnectionStringN;
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
