using System;
using System.Collections.Generic;
using CarRental.DataAcces.Concretes;
using CarRental.Entity.Concretes;

namespace CarRental.BusinessLogic.Concretes
{
    public class CustomerBusiness : IDisposable
    {
        public bool InsertCustomer(Customer entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CustomerRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:CustomerBusiness::InsertCustomer::Error occured.", ex);
            }
        }


        public bool UpdateCustomer(Customer entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CustomerRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:CustomerBusiness::UpdateCustomer::Error occured.", ex);
            }
        }

        public bool DeleteCustomerById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CustomerRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:CustomerBusiness::DeleteCustomer::Error occured.", ex);
            }
        }

        public Customer SelectCustomerById(int customerId)
        {
            try
            {
                Customer responseEntitiy;
                using (var repo = new CustomerRepository())
                {
                    responseEntitiy = repo.SelectedById(customerId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Customer doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:CustomerBusiness::SelectCustomerById::Error occured.", ex);
            }
        }

        public List<Customer> SelectAllCustomers()
        {
            var responseEntities = new List<Customer>();

            try
            {
                using (var repo = new CustomerRepository())
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
                throw new Exception("BusinessLogic:CustomerBusiness::SelectAllCustomers::Error occured.", ex);
            }
        }

        public CustomerBusiness()
        {

        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

    }
}
