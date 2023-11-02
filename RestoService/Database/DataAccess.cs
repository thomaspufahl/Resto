﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoService.Database
{
    internal class DataAccess
    {
        private readonly string _ConnectionString = "server=localhost, 1433; database=Resto; user=sa; password=ThomasPufahl1525";
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

        public void Execute()
        {
            ExecuteSelectionQuery();
        }
        public void Execute(bool isAction)
        {
            if (isAction)
            {
                ExecuteActionQuery();
            }
            else
            {
                ExecuteSelectionQuery();
            }
        }

        public void CloseConnection()
        {
            if (Reader != null && !Reader.IsClosed) Reader.Close();

            _Conex.Close();
        }

        private void ExecuteSelectionQuery()
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

        private void ExecuteActionQuery()
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
