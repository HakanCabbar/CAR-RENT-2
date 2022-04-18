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
    public class RentInfoRepository : IRepository<RentInfo>, IDisposable
    {

        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public RentInfoRepository()
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
                query.Append("FROM [dbo].[tbl_Rent] ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_RentInfos] can't be null. ");

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
                                "Deleting Error for entity [tbl_RentInfos] reported the Database ErrorCode: " +
                                _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                 throw new Exception("RentInfoRepository::Insert:Error occured.", ex);
            }

        }



        public bool Insert(RentInfo entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Rent] ");
                query.Append("( [CarName], [CarModel], [RentFeeDaily], [CarTotalKM], [CarTotalSeats], [CompanyID] ) ");
                query.Append("VALUES ");
                query.Append(
                    "( @CarName, @CarModel, @RentFeeDaily, @CarTotalKM, @CarTotalSeats, @CompanyID ) ");
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
                        DBHelper.AddParameter(dbCommand, "@CarName", CsType.String, ParameterDirection.Input, entity.CarName);
                        DBHelper.AddParameter(dbCommand, "@CarModel", CsType.Int, ParameterDirection.Input, entity.CarModel);
                        DBHelper.AddParameter(dbCommand, "@RentFeeDaily", CsType.Int, ParameterDirection.Input, entity.RentFeeDaily);
                        DBHelper.AddParameter(dbCommand, "@CarTotalKM", CsType.Int, ParameterDirection.Input, entity.CarTotalKM);
                        DBHelper.AddParameter(dbCommand, "@CarTotalSeats", CsType.Int, ParameterDirection.Input, entity.CarTotalSeat);
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



        public IList<RentInfo> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<RentInfo> rentInfo = new List<RentInfo>();

            try
            {

                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[CarID], [CarName], [CarModel], [RentFeeDaily], [CarTotalKM], [CarTotalSeats], [CompanyID] ");
                query.Append("FROM [dbo].[tbl_Rent] ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_RentInfos] can't be null. ");

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
                                    var entity = new RentInfo();
                                    entity.CarID = reader.GetInt32(0);
                                    entity.CarName = reader.GetString(1);
                                    entity.CarModel = reader.GetString(2);
                                    entity.RentFeeDaily = reader.GetInt32(3);
                                    entity.CarTotalKM = reader.GetInt32(4);
                                    entity.CarTotalSeat = reader.GetInt32(5);
                                    entity.CompanyID = reader.GetInt32(6);
                                    rentInfo.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_RentInfos] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return rentInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("RentInfoRepository::SelectAll:Error occured.", ex);
            }
        }



        public RentInfo SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            RentInfo rentInfo = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[CarID], [CarName ], [CarModel], , [RentFeeDaily], [CarTotalKM], [CarTotalSeats], [CompanyID] ");
                query.Append("FROM [dbo].[tbl_Rent] ");
                query.Append("WHERE ");
                query.Append("[RentID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_RentInfos] can't be null. ");

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
                                    var entity = new RentInfo();
                                    entity.CarID = reader.GetInt32(0);
                                    entity.CarName = reader.GetString(1);
                                    entity.CarModel = reader.GetString(2);
                                    entity.RentFeeDaily = reader.GetInt32(3);
                                    entity.CarTotalKM = reader.GetInt32(4);
                                    entity.CarTotalSeat = reader.GetInt32(5);
                                    entity.CompanyID = reader.GetInt32(6);
                                    rentInfo = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_RentInfos] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                //
                //
                return rentInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("RentInfoRepository::SelectById:Error occured.", ex);
            }
        }



        public bool Update(RentInfo entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tbl_Rent] ");
                query.Append(" SET [CarName] = @CarName, [CarModel] = @CarModel, [RentFeeDaily] =  @RentFeeDaily, [CarTotalKM] = @CarTotalKM, [CarTotalSeats] = @CarTotalSeats, [CompanyId] = @CompanyID ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_RentInfos] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@CarID", CsType.Int, ParameterDirection.Input, entity.CarID);
                        DBHelper.AddParameter(dbCommand, "@CarName", CsType.String, ParameterDirection.Input, entity.CarName);
                        DBHelper.AddParameter(dbCommand, "@CarModel", CsType.String, ParameterDirection.Input, entity.CarModel);
                        DBHelper.AddParameter(dbCommand, "@RentFeeDaily", CsType.Int, ParameterDirection.Input, entity.RentFeeDaily);
                        DBHelper.AddParameter(dbCommand, "@CarTotalKM", CsType.Int, ParameterDirection.Input, entity.CarTotalKM);
                        DBHelper.AddParameter(dbCommand, "@CarTotalSeat", CsType.Int, ParameterDirection.Input, entity.CarTotalSeat);
                        DBHelper.AddParameter(dbCommand, "@CompanyId", CsType.Int, ParameterDirection.Input, entity.CompanyID);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_RentInfos] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("RentInfoRepository::Update:Error occured.", ex);
            }
        }


    }
}
