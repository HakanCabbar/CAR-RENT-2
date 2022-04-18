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
    /// CustomerWebService için özet açıklama
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Bu Web Hizmeti'nin, ASP.NET AJAX kullanılarak komut dosyasından çağrılmasına, aşağıdaki satırı açıklamadan kaldırmasına olanak vermek için.
    // [System.Web.Script.Services.ScriptService]
    public class CustomerWebService : System.Web.Services.WebService
    {


        [WebMethod]
        public bool InsertCustomer(Customer entity)
        {
            try
            {
                using (var business = new CustomerBusiness())
                {
                    business.InsertCustomer(entity);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public bool UpdateCustomer(Customer entity)
        {
            try
            {
                using (var business = new CustomerBusiness())
                {
                    business.UpdateCustomer(entity);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public bool DeleteCustomer(int id)
        {
            try
            {
                using (var business = new CustomerBusiness())
                {
                    business.DeleteCustomerById(id);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public Customer[] SelectAllCustomers()
        {
            try
            {
                using (var business = new CustomerBusiness())
                {
                    return business.SelectAllCustomers().ToArray();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [WebMethod]
        public Customer SelectCustomerById(int id)
        {
            try
            {
                using (var business = new CustomerBusiness())
                {
                    return business.SelectCustomerById(id);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
