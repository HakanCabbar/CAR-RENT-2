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
    /// CarWebService için özet açıklama
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Bu Web Hizmeti'nin, ASP.NET AJAX kullanılarak komut dosyasından çağrılmasına, aşağıdaki satırı açıklamadan kaldırmasına olanak vermek için.
    // [System.Web.Script.Services.ScriptService]
    public class CarWebService : System.Web.Services.WebService
    {


        [WebMethod]
        public bool InsertCar(Car entity)
        {
            try
            {
                using (var business = new CarBusiness())
                {
                    business.InsertCar(entity);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public bool UpdateCar(Car entity)
        {
            try
            {
                using (var business = new CarBusiness())
                {
                    business.UpdateCar(entity);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public bool DeleteCar(int id)
        {
            try
            {
                using (var business = new CarBusiness())
                {
                    business.DeleteCarById(id);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public Car[] SelectAllCars()
        {
            try
            {
                using (var business = new CarBusiness())
                {
                    return business.SelectAllCars().ToArray();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [WebMethod]
        public Car SelectCarById(int id)
        {
            try
            {
                using (var business = new CarBusiness())
                {
                    return business.SelectCarById(id);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
