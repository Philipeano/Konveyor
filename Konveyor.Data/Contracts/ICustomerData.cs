using Konveyor.Core.ViewModels;
using System.Collections.Generic;

namespace Konveyor.Data.Contracts
{
    public interface ICustomerData
    {

        //public CustomerListViewModel GetAllCustomers();

        public List<CustomerDetailViewModel> GetAllCustomers();

        public CustomerDetailViewModel GetCustomerDetails(long customerId);

        public CustomerEditViewModel CreateNewCustomer();

        public CustomerEditViewModel GetCustomerForEdit(long customerId);

        public bool RemoveCustomer(long customerId);

        public bool SaveCustomerToDb(CustomerEditViewModel customerInfo);
    }
}
