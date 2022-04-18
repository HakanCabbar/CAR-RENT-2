using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Car = CarRental.Entity.Concretes.Car;
using CarRental.BusinessLogic.Concretes;

namespace CarRental.AspNetWeb.Pure.Controllers
{
    public class CarController : Controller
    {

        // GET: Car
        public ActionResult Index()
        {
            return View();
        }


        // GET: Car/Details/1
        public ActionResult Details(int id)
        {
            return View(SelectCarByID(id));
        }


        // GET: Car/Create
        public ActionResult Create()
        {
            return View();
        }


        public ActionResult Rent()
        {
            return View();
        }


        // POST: Car/Create
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
                if (InsertCar(collection["CarName"], int.Parse(collection["CarModel"]), int.Parse(collection["RentFeeDaily"]), int.Parse(collection["CarTotalKM"]), int.Parse(collection["CarTotalSeats"]), int.Parse(collection["CompanyID"]) ))
                    return RedirectToAction("ListAll");

                return View();
            }
            catch (Exception ex)
            {
                // LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("AspNetWeb.Pure::CarController::CreateCar::Error occured.", ex);
                //return View();
            }
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        /*public ActionResult Rent(FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                if (Rent(collection["CarName"], int.Parse(collection["CarModel"]), int.Parse(collection["RentFeeDaily"]), int.Parse(collection["CarTotalKM"]), int.Parse(collection["CarTotalSeats"]), int.Parse(collection["CompanyID"])))
                    return RedirectToAction("ListAll");

                return View();
            }
            catch (Exception ex)
            {
                // LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("AspNetWeb.Pure::CarController::CreateCar::Error occured.", ex);
                //return View();
            }
        }*/
        public ActionResult RentCar()
        {
            try
            {
              Car car = SelectCarByID(int.Parse(Request.Form["aracID"]));
              Rent(car.CarID, car.CarName, car.CarModel, car.RentFeeDaily, car.CarTotalKM, car.CarTotalSeats, car.CompanyID);
                IList<Car> cars = ListAllCars().ToList();
                return View("ListAll", cars);
            }
            catch (Exception ex)
            {

                throw new Exception("hata oldu la", ex);
            }
        }

        // GET: Car/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                return View(SelectCarByID(id));
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Car doesn't exists.", ex);
            }
        }

        // POST: Car/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                if (UpdateCar(id, collection["CarName"], int.Parse(collection["CarModel"]), int.Parse(collection["RentFeeDaily"]), int.Parse(collection["CarTotalKM"]), int.Parse(collection["CarTotalSeats"]), int.Parse(collection["CompanyID"])))
                    return RedirectToAction("Index");

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Car/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (DeleteCar(id))
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
                IList<Car> cars = ListAllCars().ToList();
                return View(cars);
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Car doesn't exists.", ex);
            }
        }

        #region PRIVATE METHODS

        private bool InsertCar(string name, int model, int rentFee, int totalKM, int totalSeats, int companyID)
        {
            try
            {
                using (var carBussines = new CarBusiness())
                {
                    return carBussines.InsertCar(new Car()
                    {
                        CarName = name,
                        CarModel = model,
                        RentFeeDaily = rentFee,
                        CarTotalKM = totalKM,
                        CarTotalSeats = totalSeats,
                        CompanyID= companyID
                    });
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Car doesn't exists.", ex);
            }
        }

        private bool Rent(int ID ,string name, int model, int rentFee, int totalKM, int totalSeats, int companyID)
        {
            try
            {
                using (var carBussines = new CarBusiness())
                {
                    return carBussines.Rent(new Car()
                    {
                        CarID = ID,
                        CarName = name,
                        CarModel = model,
                        RentFeeDaily = rentFee,
                        CarTotalKM = totalKM,
                        CarTotalSeats = totalSeats,
                        CompanyID = companyID
                    });
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Car doesn't exists.", ex);
            }
        }

        private bool UpdateCar(int id, string name, int model, int rentFee, int totalKM, int totalSeats, int companyID)
        {
            try
            {
                using (var carBussines = new CarBusiness())
                {
                    return carBussines.UpdateCar(new Car()
                    {
                        CarID = id,
                        CarName = name,
                        CarModel = model,
                        RentFeeDaily = rentFee,
                        CarTotalKM = totalKM,
                        CarTotalSeats = totalSeats,
                        CompanyID = companyID
                    });
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Car doesn't exists.", ex);
            }
        }

        private bool DeleteCar(int ID)
        {
            try
            {
                using (var carBussines = new CarBusiness())
                {
                    return carBussines.DeleteCarById(ID);
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Car doesn't exists.", ex);
            }
        }

        public List<Car> ListAllCars()
        {
            try
            {
                using (var carBussines = new CarBusiness())
                {
                    List<Car> cars = carBussines.SelectAllCars();
                    return cars;
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Car doesn't exists.", ex);
            }
        }

        private Car SelectCarByID(int ID)
        {
            try
            {
                using (var carBussines = new CarBusiness())
                {
                    return carBussines.SelectCarById(ID);
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Car doesn't exists.", ex);
            }
        }

        #endregion

    }
}