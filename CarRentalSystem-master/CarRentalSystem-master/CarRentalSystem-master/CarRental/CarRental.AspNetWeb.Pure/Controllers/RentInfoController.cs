using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RentInfo = CarRental.Entity.Concretes.RentInfo;
using CarRental.BusinessLogic.Concretes;

namespace CarRental.AspNetWeb.Pure.Controllers
{
    public class RentInfoController : Controller
    {

        // GET: RentInfo
        public ActionResult Index()
        {
            return View();
        }


        // GET: RentInfo/Details/1
        public ActionResult Details(int id)
        {
            return View(SelectRentInfoByID(id));
        }


        // GET: RentInfo/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: RentInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                //if (InsertRentInfo(DateTime.Parse(collection["RentDateStart"]), DateTime.Parse(collection["RentDateEnd"]), int.Parse(collection["FirstKM"]), int.Parse(collection["LastKM"]), int.Parse(collection["TotalRentalFee"]), int.Parse(collection["CarID"]) ))
                 //   return RedirectToAction("ListAll");

                return View();
            }
            catch (Exception ex)
            {
                // LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("AspNetWeb.Pure::RentInfoController::CreateRentInfo::Error occured.", ex);
                //return View();
            }
        }



        // GET: RentInfo/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                return View(SelectRentInfoByID(id));
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("RentInfo doesn't exists.", ex);
            }
        }

        // POST: RentInfo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                //if (UpdateRentInfo(id, DateTime.Parse(collection["RentDateStart"]), DateTime.Parse(collection["RentDateEnd"]), int.Parse(collection["FirstKM"]), int.Parse(collection["LastKM"]), int.Parse(collection["TotalRentalFee"]), int.Parse(collection["CarID"])))
                  //  return RedirectToAction("Index");

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: RentInfo/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (DeleteRentInfo(id))
                    return RedirectToAction("ListAll");
                return RedirectToAction("ListAll");
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Operation failed!", ex);
            }
        }

        public ActionResult ListAll()
        {
            try
            {
                IList<RentInfo> rentInfos = ListAllRentInfos().ToList();
                return View(rentInfos);
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("RentInfo doesn't exists.", ex);
            }
        }

        #region PRIVATE METHODS

        private bool InsertRentInfo(int carID, string carName, string carModel, int rentFeeDaily, int totalSeat, int totalKM, int companyID)
        {
            try
            {
                using (var carBussines = new RentInfoBusiness())
                {
                    return carBussines.InsertRentInfo(new RentInfo()
                    {
                        CarModel = carModel,
                        CarName = carName,
                        CarTotalKM = totalKM,
                        CarTotalSeat = totalSeat,
                        RentFeeDaily = rentFeeDaily,
                        CarID = carID,
                        CompanyID = companyID
                    });
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("RentInfo doesn't exists.", ex);
            }
        }

        private bool UpdateRentInfo(int carID, string carName, string carModel, int rentFeeDaily, int totalSeat, int totalKM, int companyID)
        {
            try
            {
                using (var carBussines = new RentInfoBusiness())
                {
                    return carBussines.UpdateRentInfo(new RentInfo()
                    {
                        CarModel = carModel,
                        CarName = carName,
                        CarTotalKM = totalKM,
                        CarTotalSeat = totalSeat,
                        RentFeeDaily = rentFeeDaily,
                        CarID = carID,
                        CompanyID = companyID
                    });
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("RentInfo doesn't exists.", ex);
            }
        }

        private bool DeleteRentInfo(int ID)
        {
            try
            {
                using (var carBussines = new RentInfoBusiness())
                {
                    return carBussines.DeleteRentInfoById(ID);
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("RentInfo doesn't exists.", ex);
            }
        }

        private List<RentInfo> ListAllRentInfos()
        {
            try
            {
                using (var carBussines = new RentInfoBusiness())
                {
                    List<RentInfo> cars = carBussines.SelectAllRentInfos();
                    return cars;
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("RentInfo doesn't exists.", ex);
            }
        }

        private RentInfo SelectRentInfoByID(int ID)
        {
            try
            {
                using (var carBussines = new RentInfoBusiness())
                {
                    return carBussines.SelectRentInfoById(ID);
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("RentInfo doesn't exists.", ex);
            }
        }

        #endregion

    }
}