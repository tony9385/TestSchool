using Microsoft.IdentityModel.Protocols;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace DOD.MachineClass
{
    /// <summary>
    /// DBHelper
    /// </summary>
    public class DBCom : IDisposable
    {

        string connectionString;
        #region 垃圾回收
        private bool isDispose = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!isDispose)
            {
                if (disposing)
                {
                    //Cleanup managed objects by calling their Dispose() methods
                }
                //Cleanup unmanaged objects
            }
            isDispose = true;
        }
        ~DBCom()
        {
            Dispose(false);
        }
        #endregion 垃圾回收
        /// <summary>
        /// 连接字符串
        /// </summary>
        private string ConnectionString
        {
            get
            {
                if (connectionString == null)
                {
                    try
                    {
                        connectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                    }
                    catch
                    {
                        throw new Exception("数据库配置错误！");
                    }
                }
                return connectionString;
            }
            set { connectionString = value; }
        }

        MySqlConnection connection;
        private MySqlConnection Connection
        {
            get
            {
                if (connection == null)
                    connection = new MySqlConnection(ConnectionString);


                return connection;
            }
            set { connection = value; }
        }


        MySqlCommand command;
        MySqlCommand Command
        {
            get
            {
                if (command == null)
                {
                    command = new MySqlCommand();
                    command.Connection = Connection;
                    command.CommandTimeout = CommandTimeout;
                }
                return command;
            }
            set { command = value; }
        }


        int commandTimeout = 300;
        public int CommandTimeout
        {
            get { return commandTimeout; }
            set { commandTimeout = value; }
        }


        public int ExecuteNonQuery(List<string> _commandTextList, List<DbParameter> dbPar = null, bool beginTran = false)
        {
            try
            {
                if (Connection != null && Connection.State == ConnectionState.Closed)
                    Connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("数据库未能正确连接！");
            }

            int row = -1;
            int i = 0;
            foreach (string _commandText in _commandTextList)
            {
                Command.CommandText = _commandText;

                if (dbParameters != null)
                    foreach (DbParameter p in dbParameters)
                    {
                        if (!Command.Parameters.Contains(p))
                            Command.Parameters.Add(p);
                    }

                if (dbPar != null)
                    foreach (DbParameter p in dbPar)
                    {
                        if (!Command.Parameters.Contains(p))
                            Command.Parameters.Add(p);
                    }

                Command.CommandTimeout = CommandTimeout;

                foreach (MySqlParameter p in Command.Parameters)
                {
                    if (p.ParameterName.StartsWith("@"))
                    {
                        string pName = "?" + p.ParameterName.Substring(1, p.ParameterName.Length - 1);
                        Command.CommandText = Command.CommandText.Replace(p.ParameterName, pName);
                        p.ParameterName = pName;
                    }
                }
                MySqlTransaction tran = null;
                if (beginTran)
                    tran = Connection.BeginTransaction();

                try
                {
                    row = Command.ExecuteNonQuery();
                    i++;
                    if (tran != null)
                        tran.Commit();

                }
                catch (Exception ex)
                {
                    if (tran != null)
                        tran.Rollback();
                    throw new Exception("执行语句出现问题！" + ex.Message);
                }

                if (tran != null)
                    tran = null;
            }

            if (Connection != null && Connection.State != ConnectionState.Closed)
                Connection.Close();

            Command.Parameters.Clear();
            if (dbParameters != null)
                dbParameters.Clear();

            return row;

        }

        public int ExecuteNonQuery(string _commandText, List<DbParameter> dbPar = null, bool beginTran = false)
        {
            try
            {
                if (Connection != null && Connection.State == ConnectionState.Closed)
                    Connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("数据库未能正确连接！");
            }

            Command.CommandText = _commandText;
            Command.CommandTimeout = CommandTimeout;


            if (dbParameters != null)
                foreach (DbParameter p in dbParameters)
                {
                    if (!Command.Parameters.Contains(p))
                        Command.Parameters.Add(p);
                }

            if (dbPar != null)
                foreach (DbParameter p in dbPar)
                {
                    if (!Command.Parameters.Contains(p))
                        Command.Parameters.Add(p);
                }


            foreach (MySqlParameter p in Command.Parameters)
            {
                if (p.ParameterName.StartsWith("@"))
                {
                    string pName = "?" + p.ParameterName.Substring(1, p.ParameterName.Length - 1);
                    Command.CommandText = Command.CommandText.Replace(p.ParameterName, pName);
                    p.ParameterName = pName;
                }
            }


            MySqlTransaction tran = null;
            if (beginTran)
                tran = Connection.BeginTransaction();

            int row = -1;
            try
            {
                row = Command.ExecuteNonQuery();
                if (tran != null)
                    tran.Commit();

            }
            catch (Exception ex)
            {
                if (tran != null)
                    tran.Rollback();
                throw new Exception("执行语句出现问题！" + ex.Message);
            }
            finally
            {
                if (tran != null)
                    tran = null;

                if (Connection != null && Connection.State != ConnectionState.Closed)
                    Connection.Close();

                Command.Parameters.Clear();
                if (dbParameters != null)
                    dbParameters.Clear();
            }

            return row;
        }


        public object ExecuteScalar(string _commandText, List<DbParameter> dbPar = null, bool beginTran = false)
        {
            try
            {
                if (Connection != null && Connection.State == ConnectionState.Closed)
                    Connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("数据库未能正确连接！");
            }

            Command.CommandText = _commandText;
            Command.CommandTimeout = CommandTimeout;


            if (dbParameters != null)
                foreach (DbParameter p in dbParameters)
                {
                    if (!Command.Parameters.Contains(p))
                        Command.Parameters.Add(p);
                }

            if (dbPar != null)
                foreach (DbParameter p in dbPar)
                {
                    if (!Command.Parameters.Contains(p))
                        Command.Parameters.Add(p);
                }


            foreach (MySqlParameter p in Command.Parameters)
            {
                if (p.ParameterName.StartsWith("@"))
                {
                    string pName = "?" + p.ParameterName.Substring(1, p.ParameterName.Length - 1);
                    Command.CommandText = Command.CommandText.Replace(p.ParameterName, pName);
                    p.ParameterName = pName;
                }
            }


            MySqlTransaction tran = null;
            if (beginTran)
                tran = Connection.BeginTransaction();

            object obj = null;
            try
            {
                obj = Command.ExecuteScalar();
                if (tran != null)
                    tran.Commit();

            }
            catch (Exception ex)
            {
                if (tran != null)
                    tran.Rollback();
                throw new Exception("执行语句出现问题！" + ex.Message);
            }
            finally
            {
                if (tran != null)
                    tran = null;

                if (Connection != null && Connection.State != ConnectionState.Closed)
                    Connection.Close();

                Command.Parameters.Clear();
                if (dbParameters != null)
                    dbParameters.Clear();
            }


            return obj;
        }

        public DataSet ExecuteQuery(string _commandText, List<DbParameter> dbPar = null)
        {
            try
            {
                Connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("数据库未能正确连接！");
            }
            Command.CommandText = _commandText;
            Command.CommandTimeout = CommandTimeout;

            DataSet ds = new DataSet();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(Command);

            if (Command.Parameters.Count > 0)
                Command.Parameters.Clear();

            if (dbParameters != null)
                foreach (DbParameter p in dbParameters)
                {
                    if (!Command.Parameters.Contains(p))
                        Command.Parameters.Add(p);
                }

            if (dbPar != null)
                foreach (DbParameter p in dbPar)
                {
                    if (!Command.Parameters.Contains(p))
                        Command.Parameters.Add(p);
                }


            foreach (MySqlParameter p in Command.Parameters)
            {
                if (p.ParameterName.StartsWith("@"))
                {
                    string pName = "?" + p.ParameterName.Substring(1, p.ParameterName.Length - 1);
                    Command.CommandText = Command.CommandText.Replace(p.ParameterName, pName);
                    p.ParameterName = pName;
                }
            }


            try
            {
                dataAdapter.Fill(ds);
            }
            catch (Exception ex)
            {
               // LogHelper.WriteLog4NetErr("DBCom", "ExecuteQuery", ex);
                throw new Exception("执行语句出现问题！");
            }
            finally
            {
                Command.Parameters.Clear();
                if (dbParameters != null)
                    dbParameters.Clear();

                if (Connection != null && Connection.State != ConnectionState.Closed)
                    Connection.Close();
            }

            return ds;
        }


        public DbParameter AddParameter(string pName, object pValue)
        {
            if (dbParameters == null)
                dbParameters = new List<DbParameter>();

            foreach (DbParameter p in dbParameters)
            {
                if (p.ParameterName == pName)
                {
                    p.Value = pValue;
                }
                else
                {
                    dbParameter = new MySqlParameter(pName, pValue);
                    dbParameters.Add(dbParameter);
                }
                return null; ;
            }
            dbParameter = new MySqlParameter(pName, pValue);
            dbParameters.Add(dbParameter);

            return dbParameter;
        }
        List<DbParameter> dbParameters;

        MySqlParameter dbParameter;
    }
}
