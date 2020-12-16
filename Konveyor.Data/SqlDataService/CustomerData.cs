using Konveyor.Core.Models;
using Konveyor.Core.ViewModels;
using Konveyor.Data.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Konveyor.Data.SqlDataService
{
    public class CustomerData : ICustomerData
    {

        private readonly KonveyorDbContext dbcontext;
        private readonly List<SelectListItem> genderOptions;


        public CustomerData(KonveyorDbContext dbContext)
        {
            dbcontext = dbContext;
            genderOptions = new List<SelectListItem> {
                new SelectListItem("- Please select -", null),
                new SelectListItem("Male", "Male"),
                new SelectListItem("Female", "Female")
            };
        }


        private Customers GetCustomerById(long id)
        {
            Customers customer = dbcontext.Customers
                .Include(c => c.User)
                .Where(c => c.IsActive == true && c.User != null && c.CustomerId == id)
                .SingleOrDefault();
            return customer;
        }

        public List<CustomerDetailViewModel> GetAllCustomers()
        {
            IQueryable<Customers> customers = dbcontext.Customers
                .Include(c => c.User)
                .Where(c => c.IsActive == true && c.User != null);

            if (!customers.Any())
                return null;

            List<CustomerDetailViewModel> activeCustomers = new List<CustomerDetailViewModel>();
            foreach (var customer in customers)
            {
                var activeCustomer = new CustomerDetailViewModel()
                {
                    CustomerId = customer.CustomerId,
                    CustomerCode = customer.CustomerCode,
                    PreferredName = customer.PreferredName,
                    ContactAddress = customer.ContactAddress,

                    UserId = customer.User.UserId,
                    FirstName = customer.User.FirstName,
                    LastName = customer.User.LastName,
                    EmailAddress = customer.User.EmailAddress,
                    PhoneNumber = customer.User.PhoneNumber,
                    Gender = customer.User.Gender
                };
                activeCustomers.Add(activeCustomer);
            }
            return activeCustomers;
        }


        public CustomerDetailViewModel GetCustomerDetails(long customerId)
        {
            Customers customer = GetCustomerById(customerId);
            if (customer == null)
                return null;

            CustomerDetailViewModel customerDetails = new CustomerDetailViewModel
            {
                CustomerId = customer.CustomerId,
                CustomerCode = customer.CustomerCode,
                PreferredName = customer.PreferredName,
                ContactAddress = customer.ContactAddress,

                UserId = customer.User.UserId,
                FirstName = customer.User.FirstName,
                LastName = customer.User.LastName,
                EmailAddress = customer.User.EmailAddress,
                PhoneNumber = customer.User.PhoneNumber,
                Gender = customer.User.Gender
            };
            return customerDetails;
        }


        public CustomerEditViewModel CreateNewCustomer() 
        {
            CustomerEditViewModel customerForCreate = new CustomerEditViewModel
            {
                GenderOptions = genderOptions
            };
            customerForCreate.GenderOptions.Find(g => g.Value == null).Selected = true;
            return customerForCreate;
        }


        public CustomerEditViewModel GetCustomerForEdit(long customerId)
        {
            Customers customer = GetCustomerById(customerId);
            if (customer == null)
                return null;

            CustomerEditViewModel customerForEdit = new CustomerEditViewModel
            {
                CustomerId = customer.CustomerId,
                CustomerCode = customer.CustomerCode,
                PreferredName = customer.PreferredName,
                ContactAddress = customer.ContactAddress,

                UserId = customer.User.UserId,
                FirstName = customer.User.FirstName,
                LastName = customer.User.LastName,
                EmailAddress = customer.User.EmailAddress,
                PhoneNumber = customer.User.PhoneNumber,
                Gender = customer.User.Gender,
                GenderOptions = genderOptions,
            };
            customerForEdit.GenderOptions.Find(g => g.Value == customer.User.Gender).Selected = true;
            return customerForEdit;
        }


        public bool RemoveCustomer(long customerId)
        {
            Customers customer = GetCustomerById(customerId);
            if (customer == null)
                return false;

            //dbcontext.Customers.Remove(customer);
            dbcontext.Customers.Find(customerId).IsActive = false;
            dbcontext.SaveChanges();
            return true;
        }


        public bool SaveCustomerToDb(CustomerEditViewModel customerInfo)
        {
            bool result = false;
            Customers customerToUpdate;
            Users userToUpdate;

            if (customerInfo.CustomerId > 0)
            {
                customerToUpdate = new Customers();
                userToUpdate = new Users();
                customerToUpdate.CustomerCode = "CUSTOMER-ABC-00001";
            }
            else
            {
                customerToUpdate = GetCustomerById(customerInfo.CustomerId);
                userToUpdate = customerToUpdate.User;
            }

            // Assign fields from VM to 'customer' entity
            // customerToUpdate.CustomerId =
            // customerToUpdate.CustomerCode =
            // customerToUpdate.IsActive =
            // customerToUpdate.UserId =
            // customerToUpdate.Status =
            // customerToUpdate.LastUpdated =
            customerToUpdate.PreferredName = customerInfo.PreferredName;
            customerToUpdate.ContactAddress = customerInfo.ContactAddress;

            // userToUpdate.UserId =
            userToUpdate.FirstName = customerInfo.FirstName;
            userToUpdate.LastName = customerInfo.LastName;
            userToUpdate.EmailAddress = customerInfo.EmailAddress;
            userToUpdate.PhoneNumber = customerInfo.PhoneNumber;
            userToUpdate.Gender = customerInfo.GenderOptions.Where(g => g.Selected == true).FirstOrDefault().Value;
            userToUpdate.Password = customerInfo.Password;

            customerToUpdate.User = userToUpdate;
            dbcontext.Customers.Add(customerToUpdate);
            dbcontext.SaveChanges();
            return result;
        }       


    }
}
