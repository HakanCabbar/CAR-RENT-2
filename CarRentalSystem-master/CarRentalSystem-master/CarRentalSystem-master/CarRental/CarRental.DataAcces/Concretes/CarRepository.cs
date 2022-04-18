using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using CarRental.Commons.Concretes.Data;
using CarRental.Commons.Concretes.Helpers;
using CarRental.DataAcces.Abstractions;
using CarRental.Entity.Concretes;

namespace CarRental.DataAcces.Concretes
{
    public class CarRepository : IRepository<Car>, IDisposable
    {

        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public CarRepository()
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
                query.Append("FROM [dbo].[tbl_Cars] ");
                query.Append("WHERE ");
                query.Append("[CarID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Cars] can't be null. ");

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
                                "Deleting Error for entity [tbl_Cars] reported the Database ErrorCode: " +
                                _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CarRepository::Insert:Error occured.", ex);
            }

        }



        public bool Insert(Car entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Cars] ");
                query.Append("([CarName], [CarModel], [RentFeeDaily], [CarTotalKM], [CarTotalSeats], [isRent], [CompanyID] ) ");
                query.Append("VALUES ");
                query.Append(
                    "( @CarName, @CarModel, @RentFeeDaily, @CarTotalKM, @CarTotalSeats, @isRent, @CompanyID ) ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Cars] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@CarName", CsType.String, ParameterDirection.Input, entity.CarName);
                        DBHelper.AddParameter(dbCommand, "@CarModel", CsType.Int, ParameterDirection.Input, entity.CarModel);
                        DBHelper.AddParameter(dbCommand, "@RentFeeDaily", CsType.Int, ParameterDirection.Input, entity.RentFeeDaily);
                        DBHelper.AddParameter(dbCommand, "@CarTotalKM", CsType.Int, ParameterDirection.Input, entity.CarTotalKM);
                        DBHelper.AddParameter(dbCommand, "@CarTotalSeats", CsType.Int, ParameterDirection.Input, entity.CarTotalSeats);
                        DBHelper.AddParameter(dbCommand, "@isRent", CsType.Byte, ParameterDirection.Input, entity.isRent);
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
                            throw new Exception("Inserting Error for entity [tbl_Cars] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("CarRepository::Insert:Error occured.", ex);
            }

        }

        public bool Rent(Car entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Rent] ");
                query.Append("([CarID], [CarName], [CarModel], [RentFeeDaily], [CarTotalKM], [CarTotalSeats], [CompanyID] ) ");
                query.Append("VALUES ");
                query.Append(
                    "(@CarID, @CarName, @CarModel, @RentFeeDaily, @CarTotalKM, @CarTotalSeats, @CompanyID ) ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Rent] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@CarID", CsType.Int, ParameterDirection.Input, entity.CarID);
                        DBHelper.AddParameter(dbCommand, "@CarName", CsType.String, ParameterDirection.Input, entity.CarName);
                        DBHelper.AddParameter(dbCommand, "@CarModel", CsType.Int, ParameterDirection.Input, entity.CarModel);
                        DBHelper.AddParameter(dbCommand, "@RentFeeDaily", CsType.Int, ParameterDirection.Input, entity.RentFeeDaily);
                        DBHelper.AddParameter(dbCommand, "@CarTotalKM", CsType.Int, ParameterDirection.Input, entity.CarTotalKM);
                        DBHelper.AddParameter(dbCommand, "@CarTotalSeats", CsType.Int, ParameterDirection.Input, entity.CarTotalSeats);
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
                            throw new Exception("Inserting Error for entity [tbl_Rent] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("CarRepository::Insert:Error occured.", ex);
            }

        }



        public IList<Car> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Car> car = new List<Car>();

            try
            {

                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[CarID], [CarName], [CarModel], [RentFeeDaily], [CarTotalKM], [CarTotalSeats], [isRent], [CompanyID] ");
                query.Append("FROM [dbo].[tbl_Cars] ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Cars] can't be null. ");

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
                                    var entity = new Car();
                                    entity.CarID = reader.GetInt32(0);
                                    entity.CarName = reader.GetString(1);
                                    entity.CarModel = reader.GetInt32(2);
                                    entity.RentFeeDaily = reader.GetInt32(3);
                                    entity.CarTotalKM = reader.GetInt32(4);
                                    entity.CarTotalSeats = reader.GetInt32(5);
                                    entity.isRent = reader.GetBoolean(6);
                                    entity.CompanyID = reader.GetInt32(7);
                                    car.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_Cars] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return car;
            }
            catch (Exception ex)
            {
                throw new Exception("CarRepository::SelectAll:Error occured.", ex);
            }
        }



        public Car SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            Car car = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[CarID], [CarName], [CarModel], [RentFeeDaily], [CarTotalKM], [CarTotalSeats], [isRent], [CompanyID] ");
                query.Append("FROM [dbo].[tbl_Cars] ");
                query.Append("WHERE ");
                query.Append("[CarID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Cars] can't be null. ");

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
                                    var entity = new Car();
                                    entity.CarID = reader.GetInt32(0);
                                    entity.CarName = reader.GetString(1);
                                    entity.CarModel = reader.GetInt32(2);
                                    entity.RentFeeDaily = reader.GetInt32(3);
                                    entity.CarTotalKM = reader.GetInt32(4);
                                    entity.CarTotalSeats = reader.GetInt32(5);
                                    entity.isRent = reader.GetBoolean(6);
                                    entity.CompanyID = reader.GetInt32(7);
                                    car = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_Cars] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                //
                //
                return car;
            }
            catch (Exception ex)
            {
                throw new Exception("CarRepository::SelectById:Error occured.", ex);
            }
        }



        public bool Update(Car entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tbl_Cars] ");
                query.Append(" SET [CarName] = @CarName, [CarModel] = @CarModel, [RentFeeDaily] =  @RentFeeDaily, [CarTotalKM] = @CarTotalKM, [CarTotalSeats] = @CarTotalSeats, [isRent] = @isRent, [CompanyID] = @CompanyID ");
                query.Append(" WHERE ");
                query.Append(" [CarID] = @CarID ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Cars] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@CarID", CsType.Int, ParameterDirection.Input, entity.CarID);
                        DBHelper.AddParameter(dbCommand, "@CarName", CsType.String, ParameterDirection.Input, entity.CarName);
                        DBHelper.AddParameter(dbCommand, "@CarModel", CsType.Int, ParameterDirection.Input, entity.CarModel);
                        DBHelper.AddParameter(dbCommand, "@RentFeeDaily", CsType.Int, ParameterDirection.Input, entity.RentFeeDaily);
                        DBHelper.AddParameter(dbCommand, "@CarTotalKM", CsType.Int, ParameterDirection.Input, entity.CarTotalKM);
                        DBHelper.AddParameter(dbCommand, "@CarTotalSeats", CsType.Int, ParameterDirection.Input, entity.CarTotalSeats);
                        DBHelper.AddParameter(dbCommand, "@isRent", CsType.Boolean, ParameterDirection.Input, entity.isRent);
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
                            throw new Exception("Updating Error for entity [tbl_Cars] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("CarRepository::Update:Error occured.", ex);
            }
        }


    }
}
