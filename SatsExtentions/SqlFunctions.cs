using Microsoft.Win32.SafeHandles;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Sats.Ado.Net
{
    public abstract partial class AdoNet : IDisposable
    {
        string constring = string.Empty;
        DataSet dataSet = new DataSet();
        DataTable datatable = new DataTable();
        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);


        public AdoNet(string connString)
        {
            constring = connString; 
        }

        #region Private Methods

        private SqlCommand SqlCommand(string ProcedureName, SqlParameter[] parameters, SqlConnection sqlConn)
        {
            using (var sqCommand = new SqlCommand(ProcedureName, sqlConn))
            {
                sqCommand.CommandType = CommandType.StoredProcedure;
                sqCommand.Parameters.AddRange(parameters);
                return sqCommand;
            }

        }


        private SqlCommand SqlCommand(string ProcedureName, object o, SqlConnection sqlConn)
        {
            using (var sqCommand = new SqlCommand(ProcedureName, sqlConn))
            {
                sqCommand.CommandType = CommandType.StoredProcedure;

                PropertyInfo[] properties = o.GetType().GetProperties().ToArray();
                foreach (var property in properties)
                {
                    sqCommand.Parameters.AddWithValue(property.Name, property.GetValue(o));
                }

                return sqCommand;
            }

        }
        #endregion Private Methods


        public void ExecuteProcedure(string ProcedureName, SqlParameter[] parameters)
        {
            try
            {
                using (var sqlConn = new SqlConnection(constring))
                {
                    sqlConn.Open();
                    var sqlCommand = SqlCommand(ProcedureName, parameters, sqlConn);
                    using (sqlCommand)
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void ExecuteProcedure(string ProcedureName, object o)
        {
            try
            {
                using (var sqlConn = new SqlConnection(constring))
                {
                    sqlConn.Open();
                    var sqlCommand = SqlCommand(ProcedureName, o, sqlConn);
                    using (sqlCommand)
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }


        public DataTable GetDataTable(string ProcedureName, SqlParameter[] parameters)
        {

            try
            {
                using (var sqlConn = new SqlConnection(constring))
                {
                    sqlConn.Open();

                    var sqlCommand = SqlCommand(ProcedureName, parameters, sqlConn);

                    using (sqlCommand)
                    {
                        using (var sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                        {
                            sqlDataAdapter.Fill(datatable);
                        }
                    }
                }

                return datatable;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetDataTable(string ProcedureName, object o)
        {

            try
            {
                using (var sqlConn = new SqlConnection(constring))
                {
                    sqlConn.Open();

                    var sqlCommand = SqlCommand(ProcedureName, o, sqlConn);

                    using (sqlCommand)
                    {
                        using (var sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                        {
                            sqlDataAdapter.Fill(datatable);
                        }
                    }

                }

                return datatable;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetDataSet(string ProcedureName, SqlParameter[] parameters)
        {

            try
            {
                using (var sqlConn = new SqlConnection(constring))
                {
                    sqlConn.Open();

                    var sqlCommand = SqlCommand(ProcedureName, parameters, sqlConn);

                    using (sqlCommand)
                    {
                        using (var sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                        {
                            sqlDataAdapter.Fill(dataSet);
                        }
                    }

                }

                return dataSet;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetDataSet(string ProcedureName, object o)
        {

            try
            {
                using (var sqlConn = new SqlConnection(constring))
                {
                    sqlConn.Open();

                    var sqlCommand = SqlCommand(ProcedureName, o, sqlConn);

                    using (sqlCommand)
                    {
                        using (var sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                        {
                            sqlDataAdapter.Fill(dataSet);
                        }
                    }

                }

                return dataSet;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                datatable.Dispose();
                dataSet.Dispose();
            }


            disposed = true;
        }
    }
}
