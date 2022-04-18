using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Employee = CarRental.Entity.Concretes.Employee;
using CarRental.BusinessLogic.Concretes;
using CarRental.Entity.Concretes;
using CarRental.AspNetWeb.Pure.Controllers;

namespace CarRental.AspNetWeb.Pure.Controllers
{
    public class EmployeeController : Controller
    {

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LoginForm()
        {
            IList<RentInfo> rents = RentInfos();
            
            return View("RentRequests", rents);
        }

        

        // GET: Employee/Details/1
        public ActionResult Details(int id)
        {
            return View(SelectEmployeeByID(id));
        }


        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Employee/Create
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
                if (InsertEmployee(collection["EmployeeName"], collection["EmployeeSurname"], collection["Email"], int.Parse(collection["CompanyID"]) ))
                    return RedirectToAction("ListAll");

                return View();
            }
            catch (Exception ex)
            {
                // LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("AspNetWeb.Pure::EmployeeController::CreateEmployee::Error occured.", ex);
                //return View();
            }
        }



        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                return View(SelectEmployeeByID(id));
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Employee doesn't exists.", ex);
            }
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                if (UpdateEmployee(id, collection["EmployeeName"], collection["EmployeeSurname"], collection["Email"], int.Parse(collection["CompanyID"])))
                    return RedirectToAction("Index");

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (DeleteEmployee(id))
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
                IList<Employee> employee = ListAllEmployees().ToList();
                return View(employee);
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Employee doesn't exists.", ex);
            }
        }

        #region PRIVATE METHODS

        private bool InsertEmployee(string name, string surname, string email, int companyID)
        {
            try
            {
                using (var employeeBussines = new EmployeeBusiness())
                {
                    return employeeBussines.InsertEmployee(new Employee()
                    {
                        EmployeeName = name,
                        EmployeeSurname = surname,
                        Email = email,
                        CompanyID = companyID
                    });
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Employee doesn't exists.", ex);
            }
        }

        private bool UpdateEmployee(int id, string name, string surname, string email, int companyID)
        {
            try
            {
                using (var employeeBussines = new EmployeeBusiness())
                {
                    return employeeBussines.UpdateEmployee(new Employee()
                    {
                        EmployeeID = id,
                        EmployeeName = name,
                        EmployeeSurname = surname,
                        Email = email,
                        CompanyID = companyID
                    });
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Employee doesn't exists.", ex);
            }
        }

        private bool DeleteEmployee(int ID)
        {
            try
            {
                using (var employeeBussines = new EmployeeBusiness())
                {
                    return employeeBussines.DeleteEmployeeById(ID);
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Employee doesn't exists.", ex);
            }
        }

        private List<Employee> ListAllEmployees()
        {
            try
            {
                using (var employeeBussines = new EmployeeBusiness())
                {
                    List<Employee> employee = employeeBussines.SelectAllEmployees();
                    return employee;
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Employee doesn't exists.", ex);
            }
        }
        private IList<RentInfo> RentInfos()
        {
            IList<RentInfo> rents = new List<RentInfo>();
            try
            {
                using (var rentCtx = new RentInfoBusiness())
                {
                    rents = rentCtx.SelectAllRentInfos();
                    return rents;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private Employee SelectEmployeeByID(int ID)
        {
            try
            {
                using (var employeeBussines = new EmployeeBusiness())
                {
                    return employeeBussines.SelectEmployeeById(ID);
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Employee doesn't exists.", ex);
            }
        }

        #endregion

    }
}