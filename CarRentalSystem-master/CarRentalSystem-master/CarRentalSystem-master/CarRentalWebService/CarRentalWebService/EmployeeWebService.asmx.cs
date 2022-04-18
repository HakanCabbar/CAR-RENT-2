using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CarRental.BusinessLogic.Concretes;
using CarRental.Commons.Concretes;
using CarRental.Entity.Concretes;

namespace CarRentalWebService
{
    /// <summary>
    /// EmployeeWebService için özet açıklama
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Bu Web Hizmeti'nin, ASP.NET AJAX kullanılarak komut dosyasından çağrılmasına, aşağıdaki satırı açıklamadan kaldırmasına olanak vermek için.
    // [System.Web.Script.Services.ScriptService]
    public class EmployeeWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public bool InsertEmployee(Employee entity)
        {
            try
            {
                using (var business = new EmployeeBusiness())
                {
                    business.InsertEmployee(entity);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public bool UpdateEmployee(Employee entity)
        {
            try
            {
                using (var business = new EmployeeBusiness())
                {
                    business.UpdateEmployee(entity);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public bool DeleteEmployee(int id)
        {
            try
            {
                using (var business = new EmployeeBusiness())
                {
                    business.DeleteEmployeeById(id);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public Employee[] SelectAllEmployees()
        {
            try
            {
                using (var business = new EmployeeBusiness())
                {
                    return business.SelectAllEmployees().ToArray();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [WebMethod]
        public Employee SelectEmployeeById(int id)
        {
            try
            {
                using (var business = new EmployeeBusiness())
                {
                    return business.SelectEmployeeById(id);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}
