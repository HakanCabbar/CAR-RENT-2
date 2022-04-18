using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Customer = CarRental.Entity.Concretes.Customer;
using CarRental.BusinessLogic.Concretes;

namespace CarRental.AspNetWeb.Pure.Controllers
{
    public class CustomerController : Controller
    {

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }


        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View(SelectCustomerByID(id));
        }


        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Customer/Create
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
                if (InsertCustomer(collection["CustomerName"], collection["CustomerSurname"], collection["PhoneNumber"], collection["Email"]))
                    return RedirectToAction("ListAll");

                return View();
            }
            catch (Exception ex)
            {
                // LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("AspNetWeb.Pure::CustomerController::CreateCustomer::Error occured.", ex);
                //return View();
            }
        }



        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                return View(SelectCustomerByID(id));
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Customer doesn't exists.", ex);
            }
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                if (UpdateCustomer(id, collection["CustomerName"], collection["CustomerSurname"], collection["PhoneNumber"], collection["Email"]))
                    return RedirectToAction("Index");

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (DeleteCustomer(id))
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
                IList<Customer> customers = ListAllCustomers().ToList();
                return View(customers);
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Customer doesn't exists.", ex);
            }
        }

        #region PRIVATE METHODS

        private bool InsertCustomer(string name, string surname, string number, string email)
        {
            try
            {
                using (var customerBussines = new CustomerBusiness())
                {
                    return customerBussines.InsertCustomer(new Customer()
                    {
                        CustomerName = name,
                        CustomerSurname = surname,
                        PhoneNumber = number,
                        Email = email
                    });
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Customer doesn't exists.", ex);
            }
        }

        private bool UpdateCustomer(int id, string name, string surname, string number, string email)
        {
            try
            {
                using (var customerBussines = new CustomerBusiness())
                {
                    return customerBussines.UpdateCustomer(new Customer()
                    {
                        CustomerID = id,
                        CustomerName = name,
                        CustomerSurname = surname,
                        PhoneNumber = number,
                        Email = email
                    });
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Customer doesn't exists.", ex);
            }
        }

        private bool DeleteCustomer(int ID)
        {
            try
            {
                using (var customerBussines = new CustomerBusiness())
                {
                    return customerBussines.DeleteCustomerById(ID);
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Customer doesn't exists.", ex);
            }
        }

        private List<Customer> ListAllCustomers()
        {
            try
            {
                using (var customerBussines = new CustomerBusiness())
                {
                    List<Customer> customers = customerBussines.SelectAllCustomers();
                    return customers;
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Customer doesn't exists.", ex);
            }
        }

        private Customer SelectCustomerByID(int ID)
        {
            try
            {
                using (var customerBussines = new CustomerBusiness())
                {
                    return customerBussines.SelectCustomerById(ID);
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Customer doesn't exists.", ex);
            }
        }

        #endregion














    }
}