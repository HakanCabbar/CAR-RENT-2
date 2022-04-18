using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Company = CarRental.Entity.Concretes.Company;
using CarRental.BusinessLogic.Concretes;

namespace CarRental.AspNetWeb.Pure.Controllers
{
    public class CompanyController : Controller
    {

        // GET: Company
        public ActionResult Index()
        {
            return View();
        }


        // GET: Company/Details/1
        public ActionResult Details(int id)
        {
            return View(SelectCompanyByID(id));
        }


        // GET: Company/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Company/Create
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
                if (InsertCompany(collection["CompanyName"], collection["City"], collection["Address"], int.Parse(collection["TotalCars"])))
                    return RedirectToAction("ListAll");

                return View();
            }
            catch (Exception ex)
            {
                // LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("AspNetWeb.Pure::CompanyController::CreateCompany::Error occured.", ex);
                //return View();
            }
        }



        // GET: Company/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                return View(SelectCompanyByID(id));
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Company doesn't exists.", ex);
            }
        }

        // POST: Company/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                if (UpdateCompany(id, collection["CompanyName"], collection["City"], collection["Address"], int.Parse(collection["TotalCars"])))
                    return RedirectToAction("Index");

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (DeleteCompany(id))
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
                IList<Company> companies = ListAllCompanies().ToList();
                return View(companies);
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Company doesn't exists.", ex);
            }
        }

        #region PRIVATE METHODS

        private bool InsertCompany(string name, string city, string address, int totalCars)
        {
            try
            {
                using (var companyBussines = new CompanyBusiness())
                {
                    return companyBussines.InsertCompany(new Company()
                    {
                        CompanyName = name,
                        City = city,
                        Address = address,
                        TotalCars = totalCars
                    });
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Company doesn't exists.", ex);
            }
        }

        private bool UpdateCompany(int id, string name, string city, string address, int totalCars)
        {
            try
            {
                using (var companyBussines = new CompanyBusiness())
                {
                    return companyBussines.UpdateCompany(new Company()
                    {
                        CompanyID = id,
                        CompanyName = name,
                        City = city,
                        Address = address,
                        TotalCars = totalCars
                    });
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Company doesn't exists.", ex);
            }
        }

        private bool DeleteCompany(int ID)
        {
            try
            {
                using (var companyBussines = new CompanyBusiness())
                {
                    return companyBussines.DeleteCompanyById(ID);
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Company doesn't exists.", ex);
            }
        }

        private List<Company> ListAllCompanies()
        {
            try
            {
                using (var companyBussines = new CompanyBusiness())
                {
                    List<Company> companies = companyBussines.SelectAllCompanies();
                    return companies;
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Company doesn't exists.", ex);
            }
        }

        private Company SelectCompanyByID(int ID)
        {
            try
            {
                using (var companyBussines = new CompanyBusiness())
                {
                    return companyBussines.SelectCompanyById(ID);
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Company doesn't exists.", ex);
            }
        }

        #endregion


    }
}