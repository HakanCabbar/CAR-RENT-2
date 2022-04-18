using System;
using System.Collections.Generic;
using CarRental.DataAcces.Concretes;
using CarRental.Entity.Concretes;

namespace CarRental.BusinessLogic.Concretes
{
    public class CompanyBusiness : IDisposable
    {
        public bool InsertCompany(Company entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CompanyRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:CompanyBusiness::InsertCompany::Error occured.", ex);
            }
        }


        public bool UpdateCompany(Company entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CompanyRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:CompanyBusiness::UpdateCompany::Error occured.", ex);
            }
        }

        public bool DeleteCompanyById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CompanyRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:CompanyBusiness::DeleteCompany::Error occured.", ex);
            }
        }

        public Company SelectCompanyById(int companyId)
        {
            try
            {
                Company responseEntitiy;
                using (var repo = new CompanyRepository())
                {
                    responseEntitiy = repo.SelectedById(companyId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Company doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:CompanyBusiness::SelectCompanyById::Error occured.", ex);
            }
        }

        public List<Company> SelectAllCompanies()
        {
            var responseEntities = new List<Company>();

            try
            {
                using (var repo = new CompanyRepository())
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
                throw new Exception("BusinessLogic:CompanyBusiness::SelectAllCompan'es::Error occured.", ex);
            }
        }

        public CompanyBusiness()
        {

        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

    }
}
