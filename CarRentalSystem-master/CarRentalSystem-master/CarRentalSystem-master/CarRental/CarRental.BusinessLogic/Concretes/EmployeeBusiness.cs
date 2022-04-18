using System;
using System.Collections.Generic;
using CarRental.DataAcces.Concretes;
using CarRental.Entity.Concretes;

namespace CarRental.BusinessLogic.Concretes
{
    public class EmployeeBusiness : IDisposable
    {
        public bool InsertEmployee(Employee entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new EmployeeRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:EmployeeBusiness::InsertEmployee::Error occured.", ex);
            }
        }


        public bool UpdateEmployee(Employee entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new EmployeeRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:EmployeeBusiness::UpdateEmployee::Error occured.", ex);
            }
        }

        public bool DeleteEmployeeById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new EmployeeRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:EmployeeBusiness::DeleteEmployee::Error occured.", ex);
            }
        }

        public Employee SelectEmployeeById(int employeeId)
        {
            try
            {
                Employee responseEntitiy;
                using (var repo = new EmployeeRepository())
                {
                    responseEntitiy = repo.SelectedById(employeeId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Employee doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:EmployeeBusiness::SelectEmployeeById::Error occured.", ex);
            }
        }

        public List<Employee> SelectAllEmployees()
        {
            var responseEntities = new List<Employee>();

            try
            {
                using (var repo = new EmployeeRepository())
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
                throw new Exception("BusinessLogic:EmployeeBusiness::SelectAllEmployees::Error occured.", ex);
            }
        }

        public EmployeeBusiness()
        {

        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

    }
}
