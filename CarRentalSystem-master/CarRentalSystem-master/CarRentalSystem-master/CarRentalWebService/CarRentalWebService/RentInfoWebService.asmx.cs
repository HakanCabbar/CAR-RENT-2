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
    /// RentInfoWebService için özet açıklama
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Bu Web Hizmeti'nin, ASP.NET AJAX kullanılarak komut dosyasından çağrılmasına, aşağıdaki satırı açıklamadan kaldırmasına olanak vermek için.
    // [System.Web.Script.Services.ScriptService]
    public class RentInfoWebService : System.Web.Services.WebService
    {


        [WebMethod]
        public bool InsertRentInfo(RentInfo entity)
        {
            try
            {
                using (var business = new RentInfoBusiness())
                {
                    business.InsertRentInfo(entity);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public bool UpdateRentInfo(RentInfo entity)
        {
            try
            {
                using (var business = new RentInfoBusiness())
                {
                    business.UpdateRentInfo(entity);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public bool DeleteRentInfo(int id)
        {
            try
            {
                using (var business = new RentInfoBusiness())
                {
                    business.DeleteRentInfoById(id);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public RentInfo[] SelectAllRentInfos()
        {
            try
            {
                using (var business = new RentInfoBusiness())
                {
                    return business.SelectAllRentInfos().ToArray();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [WebMethod]
        public RentInfo SelectRentInfoById(int id)
        {
            try
            {
                using (var business = new RentInfoBusiness())
                {
                    return business.SelectRentInfoById(id);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
