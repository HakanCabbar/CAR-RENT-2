using System;
using System.Collections.Generic;
using CarRental.DataAcces.Concretes;
using CarRental.Entity.Concretes;

namespace CarRental.BusinessLogic.Concretes
{
    public class CarBusiness : IDisposable
    {
        public bool InsertCar(Car entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CarRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:CarBusiness::InsertCar::Error occured.", ex);
            }
        }

        public bool Rent(Car entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CarRepository())
                {
                    isSuccess = repo.Rent(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:CarBusiness::InsertCar::Error occured.", ex);
            }
        }


        public bool UpdateCar(Car entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CarRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:CarBusiness::UpdateCar::Error occured.", ex);
            }
        }

        public bool DeleteCarById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CarRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:CarBusiness::DeleteCar::Error occured.", ex);
            }
        }

        public Car SelectCarById(int carId)
        {
            try
            {
                Car responseEntitiy;
                using (var repo = new CarRepository())
                {
                    responseEntitiy = repo.SelectedById(carId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Car doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:CarBusiness::SelectCarById::Error occured.", ex);
            }
        }

        public List<Car> SelectAllCars()
        {
            var responseEntities = new List<Car>();

            try
            {
                using (var repo = new CarRepository())
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
                throw new Exception("BusinessLogic:CarBusiness::SelectAllCars::Error occured.", ex);
            }
        }

        public CarBusiness()
        {

        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

    }
}
