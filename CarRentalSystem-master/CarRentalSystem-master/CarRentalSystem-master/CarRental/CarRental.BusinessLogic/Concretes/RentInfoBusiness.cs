using System;
using System.Collections.Generic;
using CarRental.DataAcces.Concretes;
using CarRental.Entity.Concretes;

namespace CarRental.BusinessLogic.Concretes
{
    public class RentInfoBusiness : IDisposable
    {
        public bool InsertRentInfo(RentInfo entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new RentInfoRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:RentInfoBusiness::InsertRentInfo::Error occured.", ex);
            }
        }


        public bool UpdateRentInfo(RentInfo entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new RentInfoRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:RentInfoBusiness::UpdateRentInfo::Error occured.", ex);
            }
        }

        public bool DeleteRentInfoById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new RentInfoRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:RentInfoBusiness::DeleteRentInfo::Error occured.", ex);
            }
        }

        public RentInfo SelectRentInfoById(int rentInfoId)
        {
            try
            {
                RentInfo responseEntitiy;
                using (var repo = new RentInfoRepository())
                {
                    responseEntitiy = repo.SelectedById(rentInfoId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("RentInfo doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:RentInfoBusiness::SelectRentInfoById::Error occured.", ex);
            }
        }

        public List<RentInfo> SelectAllRentInfos()
        {
            var responseEntities = new List<RentInfo>();

            try
            {
                using (var repo = new RentInfoRepository())
                {
                    foreach (var entity in repo.SelectAll())
                    {
                        responseEntities.Add(entity);
                    }
                }
                return responseEntities;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:RentInfoBusiness::SelectAllRentInfos::Error occured.", ex);
            }
        }

        public RentInfoBusiness()
        {

        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

    }
}
