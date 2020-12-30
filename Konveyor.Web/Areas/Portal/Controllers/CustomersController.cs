using Konveyor.Core.ViewModels;
using Konveyor.Data.Contracts;
using Konveyor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Konveyor.Web.Areas.Portal.Controllers
{
    [Area("Portal")]
    public class CustomersController : Controller
    {
        private readonly ICustomerData customerData;

        public CustomersController(ICustomerData customerData) 
        {
            this.customerData = customerData;
        }


        // GET: CustomersController
        public ActionResult Index()
        {
            ViewData["Title"] = "Registered Customers";
            ViewData["Description"] = "Below is a list of customers with active profiles.";
            ViewData["ErrorMessage"] = "No registered customers found. Kindly click the 'Register' link to register one.";
            return View(customerData.GetAllCustomers());
        }


        // GET: CustomersController/Details/:id
        public ActionResult Details(long id)
        {
            ViewData["Title"] = "Customer Profile";
            ViewData["Description"] = "Find your detailed information below. To update your profile, click the 'Edit' link";
            ViewData["ErrorMessage"] = "Sorry, we are unable to display the profile information for the selected customer.";
            return View(customerData.GetCustomerDetails(id));
        }


        // GET: CustomersController/Create
        public ActionResult Create()
        {
            ViewData["Title"] = "New Customer Registration";
            ViewData["Description"] = "Fill out this form to create your customer profile.";
            ViewData["ErrorMessage"] = "Sorry, the customer registration form is unavailable at this time. Please try again shortly.";
            return View(customerData.CreateNewCustomer());
        }


        // GET: CustomersController/Edit/:id
        public ActionResult Edit(long id)
        {
            ViewData["Title"] = "Update Customer Profile";
            ViewData["Description"] = "Fill out this form to update your customer profile.";
            ViewData["ErrorMessage"] = "Sorry, the customer profile update form is unavailable at this time. Please try again shortly.";
            return View(customerData.GetCustomerForEdit(id));
        }


        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                CustomerEditViewModel customerVM = new CustomerEditViewModel()
                {
                    CustomerId = 0,
                    PreferredName = collection["PreferredName"],
                    ContactAddress = collection["ContactAddress"],
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"],
                    EmailAddress = collection["EmailAddress"],
                    PhoneNumber = collection["PhoneNumber"],
                    Gender = collection["Gender"],
                    Password = collection["Password"]
                };

                customerData.SaveCustomerToDb(customerVM, out string errorMsg);
                if (errorMsg != string.Empty)
                {
                    ViewData["ErrorMessage"] = $"Unable to create the profile: {errorMsg}";
                    return View();
                    // return View(new ErrorViewModel());
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View(new ErrorViewModel());
            }
        }


        // POST: CustomersController/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, IFormCollection collection)
        {
            try
            {
                CustomerEditViewModel customerVM = new CustomerEditViewModel()
                {
                    CustomerId = id,
                    PreferredName = collection["PreferredName"],
                    ContactAddress = collection["ContactAddress"],
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"],
                    EmailAddress = collection["EmailAddress"],
                    PhoneNumber = collection["PhoneNumber"],
                    Gender = collection["Gender"],
                    Password = collection["Password"]
                };
                customerData.SaveCustomerToDb(customerVM, out string errorMsg);
                if (errorMsg != string.Empty)
                {
                    ViewData["ErrorMessage"] = $"Unable to update the profile: {errorMsg}";
                    return View();
                    // return View(new ErrorViewModel());
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View(new ErrorViewModel());
            }
        }


        // GET: CustomersController/Delete/:id
        public ActionResult Delete(long id)
        {
            customerData.RemoveCustomer(id, out string errorMsg);
            if (errorMsg != string.Empty)
                ViewData["ErrorMessage"] = $"Unable to delete the profile: {errorMsg}";

            return RedirectToAction(nameof(Index));
        }


        // POST: CustomersController/Delete/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id, IFormCollection collection)
        {
            customerData.RemoveCustomer(id, out string errorMsg);
            if (errorMsg != string.Empty)
            {
                ViewData["ErrorMessage"] = $"Unable to delete the profile: {errorMsg}";
                return RedirectToAction(nameof(Index));
                //return View(new ErrorViewModel());
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
