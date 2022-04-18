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
    public class CustomerRepository : IRepository<Customer>, IDisposable
    {
        
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public CustomerRepository()
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
                query.Append("FROM [dbo].[tbl_Customers] ");
                query.Append("WHERE ");
                query.Append("[CustomerID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Customers] can't be null. ");

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
                                "Deleting Error for entity [tbl_Customers] reported the Database ErrorCode: " +
                                _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomerRepository::Insert:Error occured.", ex);
            }

        }



        public bool Insert(Customer entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Customers] ");
                query.Append("( [CustomerName], [CustomerSurname], [PhoneNumber], [Email] ) ");
                query.Append("VALUES ");
                query.Append(
                    "( @CustomerName, @CustomerSurname, @PhoneNumber, @Email ) ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Customers] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@CustomerName", CsType.String, ParameterDirection.Input, entity.CustomerName);
                        DBHelper.AddParameter(dbCommand, "@CustomerSurname", CsType.String, ParameterDirection.Input, entity.CustomerSurname);
                        DBHelper.AddParameter(dbCommand, "@PhoneNumber", CsType.String, ParameterDirection.Input, entity.PhoneNumber);
                        DBHelper.AddParameter(dbCommand, "@Email", CsType.String, ParameterDirection.Input, entity.Email);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_Customers] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("CustomerRepository::Insert:Error occured.", ex);
            }

        }



        public IList<Customer> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Customer> customer = new List<Customer>();

            try
            {

                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[CustomerID], [CustomerName], [CustomerSurname], [PhoneNumber], [Email] ");
                query.Append("FROM [dbo].[tbl_Customers] ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Customers] can't be null. ");

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
                                    var entity = new Customer();
                                    entity.CustomerID = reader.GetInt32(0);
                                    entity.CustomerName = reader.GetString(1);
                                    entity.CustomerSurname = reader.GetString(2);
                                    entity.PhoneNumber = reader.GetString(3);
                                    entity.Email = reader.GetString(4);
                                    customer.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_Customer] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception("CustomerRepository::SelectAll:Error occured.", ex);
            }
        }



        public Customer SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            Customer customer = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[CustomerID], [CustomerName], [CustomerSurname], [PhoneNumber], [Email] ");
                query.Append("FROM [dbo].[tbl_Customers] ");
                query.Append("WHERE ");
                query.Append("[CustomerID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Customers] can't be null. ");

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
                                    var entity = new Customer();
                                    entity.CustomerID = reader.GetInt32(0);
                                    entity.CustomerName = reader.GetString(1);
                                    entity.CustomerSurname = reader.GetString(2);
                                    entity.PhoneNumber = reader.GetString(3);
                                    entity.Email = reader.GetString(4);
                                    customer = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_Customer] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                //
                //Rentlere göre düzenle
                //customer.Transactions = new TranscationsRepository().SelectAll().Where(x => x.TransactorAccountNumber.Equals(customer.CustomerID) || x.ReceiverAccountNumber.Equals(customer.CustomerID)).ToList();
                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception("CustomerRepository::SelectById:Error occured.", ex);
            }
        }



        public bool Update(Customer entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tbl_Customers] ");
                query.Append(" SET [CustomerName] = @CustomerName, [CustomerSurname] = @CustomerSurname, [PhoneNumber] =  @PhoneNumber, [Email] = @Email ");
                query.Append(" WHERE ");
                query.Append(" [CustomerID] = @CustomerID ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Customers] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@CustomerID", CsType.String, ParameterDirection.Input, entity.CustomerID);
                        DBHelper.AddParameter(dbCommand, "@CustomerName", CsType.String, ParameterDirection.Input, entity.CustomerName);
                        DBHelper.AddParameter(dbCommand, "@CustomerSurname", CsType.String, ParameterDirection.Input, entity.CustomerSurname);
                        DBHelper.AddParameter(dbCommand, "@PhoneNumber", CsType.Int, ParameterDirection.Input, entity.PhoneNumber);
                        DBHelper.AddParameter(dbCommand, "@Email", CsType.String, ParameterDirection.Input, entity.Email);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_Customer] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("CustomerRepository::Update:Error occured.", ex);
            }
        }


    }
}
