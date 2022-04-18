using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CarRental.BusinessLogic.Concretes;
using CarRental.Entity.Concretes;

namespace CarRentalWebService
{
    /// <summary>
    /// CompanyWebService için özet açıklama
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Bu Web Hizmeti'nin, ASP.NET AJAX kullanılarak komut dosyasından çağrılmasına, aşağıdaki satırı açıklamadan kaldırmasına olanak vermek için.
    // [System.Web.Script.Services.ScriptService]

    public class CompanyWebService : System.Web.Services.WebService
    {


        [WebMethod]
        public bool InsertCompany(Company entity)
        {
            try
            {
                using (var business = new CompanyBusiness())
                {
                    business.InsertCompany(entity);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public bool UpdateCompany(Company entity)
        {
            try
            {
                using (var business = new CompanyBusiness())
                {
                    business.UpdateCompany(entity);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public bool DeleteCompany(int id)
        {
            try
            {
                using (var business = new CompanyBusiness())
                {
                    business.DeleteCompanyById(id);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public Company[] SelectAllCompanies()
        {
            try
            {
                using (var business = new CompanyBusiness())
                {
                    return business.SelectAllCompanies().ToArray();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [WebMethod]
        public Company SelectCompanyById(int id)
        {
            try
            {
                using (var business = new CompanyBusiness())
                {
                    return business.SelectCompanyById(id);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }

}

