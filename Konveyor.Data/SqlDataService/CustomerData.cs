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
                new SelectListItem("- Please select -", string.Empty),
                new SelectListItem("Male", "Male"),
                new SelectListItem("Female", "Female")
            };
        }

        // =================================================================
        // Random number generation for customer code
        private static readonly System.Random random = new System.Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int min = 1000000000, int max = int.MaxValue)
        {
            lock (syncLock)
            { 
                return random.Next(min, max);
            }
        }
        // =================================================================


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
            customerForCreate.GenderOptions.Find(g => g.Value == string.Empty || g.Value == null).Selected = true;
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
            Customers customerToSave;
            Users userToSave;

            if (customerInfo.CustomerId > 0)
            {
                customerToSave = GetCustomerById(customerInfo.CustomerId);
                userToSave = customerToSave.User;
            }
            else
            {
                customerToSave = new Customers();
                userToSave = new Users();
                customerToSave.CustomerCode = $"CUSTOMER{GetRandomNumber()}";
            }

            // Assign fields from VM to 'customer' entity
            // customerToSave.CustomerId =
            // customerToSave.CustomerCode =
            // customerToSave.IsActive =
            // customerToSave.UserId =
            // customerToSave.Status =
            // userToSave.UserId =

            customerToSave.PreferredName = customerInfo.PreferredName;
            customerToSave.ContactAddress = customerInfo.ContactAddress;
            customerToSave.LastUpdated = System.DateTime.Now;
            userToSave.FirstName = customerInfo.FirstName;
            userToSave.LastName = customerInfo.LastName;
            userToSave.EmailAddress = customerInfo.EmailAddress;
            userToSave.PhoneNumber = customerInfo.PhoneNumber;
            userToSave.Gender = customerInfo.Gender; 
            userToSave.Password = customerInfo.Password;
            customerToSave.User = userToSave;

            if (customerInfo.CustomerId == 0)
            {
                dbcontext.Customers.Add(customerToSave);
            }
            else 
            {
                dbcontext.Customers.Update(customerToSave);
            }
            dbcontext.SaveChanges();
            return true;
        }


    }
}
