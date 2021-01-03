using Konveyor.Common.Utilities;
using Konveyor.Core.Models;
using Konveyor.Core.ViewModels;
using Konveyor.Data.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
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
            {
                return null;
            }

            List<CustomerDetailViewModel> customerList = new List<CustomerDetailViewModel>();
            foreach (Customers customer in customers)
            {
                CustomerDetailViewModel customerInfo = new CustomerDetailViewModel
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
                customerList.Add(customerInfo);
            }
            return customerList;
        }


        public CustomerDetailViewModel GetCustomerDetails(long customerId)
        {
            Customers customer = GetCustomerById(customerId);
            if (customer == null)
            {
                return null;
            }

            CustomerDetailViewModel customerInfo = new CustomerDetailViewModel
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
            return customerInfo;
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


        public bool TryRemoveCustomer(long customerId, out string errorMsg)
        {
            Customers customer = GetCustomerById(customerId);
            if (customer == null)
            {
                errorMsg = "The specified customer does not exist.";
                return false;
            }
            
            try 
            {
                dbcontext.Customers.Find(customerId).IsActive = false;
                dbcontext.SaveChanges();
                errorMsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }


        public bool TrySaveCustomerToDb(CustomerEditViewModel customerInfo, out string errorMsg)
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
                customerToSave.CustomerCode = CodeGenerator.GenerateCode("Customer"); 
            }

            try
            {
                customerToSave.PreferredName = customerInfo.PreferredName;
                customerToSave.ContactAddress = customerInfo.ContactAddress;
                customerToSave.LastUpdated = DateTime.Now;
                userToSave.FirstName = customerInfo.FirstName;
                userToSave.LastName = customerInfo.LastName;
                userToSave.EmailAddress = customerInfo.EmailAddress;
                userToSave.PhoneNumber = customerInfo.PhoneNumber;
                userToSave.Gender = new SelectList(customerInfo.GenderOptions).SelectedValue.ToString();
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
                errorMsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }
    }
}
