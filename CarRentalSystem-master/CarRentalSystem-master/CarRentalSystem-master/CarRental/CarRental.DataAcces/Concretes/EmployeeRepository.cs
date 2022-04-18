using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using CarRental.Commons.Concretes.Data;
using CarRental.Commons.Concretes.Helpers;
using CarRental.DataAcces.Abstractions;
using CarRental.Entity.Concretes;

namespace CarRental.DataAcces.Concretes
{
    public class EmployeeRepository : IRepository<Employee>, IDisposable
    {

        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;


        public EmployeeRepository()
        {
            _connectionString = DBHelper.GetConnectionString();
            _dbProviderName = DBHelper.GetConnectionProvider();
            _dbProviderFactory = DbProviderFactories.GetFactory(_dbProviderName);

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            // Check the Dispose method called before.
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    // Clean the resources used.
                    _dbProviderFactory = null;
                }

                _bDisposed = true;
            }
        }


        public bool DeletedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("DELETE ");
                query.Append("FROM [dbo].[tbl_Employees] ");
                query.Append("WHERE ");
                query.Append("[EmployeeID] = @id ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException(
                                "dbCommand" + " The db SelectById command for entity [tbl_Employees] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        DBHelper.AddParameter(dbCommand, "@id", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();
                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception(
                                "Deleting Error for entity [tbl_Employees] reported the Database ErrorCode: " +
                                _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("EmployeeRepository::Delete:Error occured.", ex);
            }

        }


        public bool Insert(Employee entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Employees] ");
                query.Append("( [EmployeeName], [EmployeeSurname], [Email], [CompanyID] ) ");
                query.Append("VALUES ");
                query.Append(
                    "( @EmployeeName, @EmployeeSurname, @Email, @CompanyID ) ");
                query.Append("SELECT @intErrorCode=@@ERROR;");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Employees] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@EmployeeName", CsType.String, ParameterDirection.Input, entity.EmployeeName);
                        DBHelper.AddParameter(dbCommand, "@EmployeeSurname", CsType.String, ParameterDirection.Input, entity.EmployeeSurname);
                        DBHelper.AddParameter(dbCommand, "@Email", CsType.String, ParameterDirection.Input, entity.Email);
                        DBHelper.AddParameter(dbCommand, "@CompanyID", CsType.Int, ParameterDirection.Input, entity.CompanyID);
                        
                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_Employees] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("EmployeeRepository::Insert:Error occured.", ex);
            }

        }





        public IList<Employee> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Employee> employee = new List<Employee>();

            try
            {

                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[EmployeeID], [EmployeeName], [EmployeeSurname], [Email], [CompanyID] ");
                query.Append("FROM [dbo].[tbl_Employees] ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");


                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException(
                                "dbCommand" + " The db SelectById command for entity [tbl_Employees] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters - None

                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int,
                            ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new Employee();
                                    entity.EmployeeID = reader.GetInt32(0);
                                    entity.EmployeeName = reader.GetString(1);
                                    entity.EmployeeSurname = reader.GetString(2);
                                    entity.Email = reader.GetString(3);
                                    entity.CompanyID = reader.GetInt32(4);
                                    employee.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_Employees] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception("EmployeeRepository::SelectAll:Error occured.", ex);
            }
        }



        public Employee SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            Employee employee = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[EmployeeID], [EmployeeName], [EmployeeSurname], [Email], [CompanyID] ");
                query.Append("FROM [dbo].[tbl_Employees] ");
                query.Append("WHERE ");
                query.Append("[EmployeeID] = @id ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException(
                                "dbCommand" + " The db SelectById command for entity [tbl_Employees] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        DBHelper.AddParameter(dbCommand, "@id", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new Employee();
                                    entity.EmployeeID = reader.GetInt32(0);
                                    entity.EmployeeName = reader.GetString(1);
                                    entity.EmployeeSurname = reader.GetString(2);
                                    entity.Email = reader.GetString(3);
                                    entity.CompanyID = reader.GetInt32(4);
                                    employee = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_Employees] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                //
                //
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception("EmployeeRepository::SelectById:Error occured.", ex);
            }
        }



        public bool Update(Employee entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tbl_Employees] ");
                query.Append(" SET [EmployeeName] = @EmployeeName, [EmployeeSurname] = @EmployeeSurname, [Email] =  @Email, [CompanyID] = @CompanyID ");
                query.Append(" WHERE ");
                query.Append(" [EmployeeID] = @EmployeeID ");
                query.Append(" SELECT @intErrorCode = @@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Employees] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@EmployeeID", CsType.Int, ParameterDirection.Input, entity.EmployeeID);
                        DBHelper.AddParameter(dbCommand, "@EmployeeName", CsType.String, ParameterDirection.Input, entity.EmployeeName);
                        DBHelper.AddParameter(dbCommand, "@EmployeeSurname", CsType.String, ParameterDirection.Input, entity.EmployeeSurname);
                        DBHelper.AddParameter(dbCommand, "@Email", CsType.String, ParameterDirection.Input, entity.Email);
                        DBHelper.AddParameter(dbCommand, "@CompanyID", CsType.Int, ParameterDirection.Input, entity.CompanyID);
                        
                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_Employees] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("EmployeeRepository::Update:Error occured.", ex);
            }
        }





    }
}
