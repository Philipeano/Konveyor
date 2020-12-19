using Konveyor.Core.ViewModels;
using System.Collections.Generic;

namespace Konveyor.Data.Contracts
{
    public interface ICustomerData
    {
        public List<CustomerDetailViewModel> GetAllCustomers();

        public CustomerDetailViewModel GetCustomerDetails(long customerId);

        public CustomerEditViewModel CreateNewCustomer();

        public CustomerEditViewModel GetCustomerForEdit(long customerId);

        public void RemoveCustomer(long customerId, out string errorMsg);

        public void SaveCustomerToDb(CustomerEditViewModel customerInfo, out string errorMsg);
    }
}
