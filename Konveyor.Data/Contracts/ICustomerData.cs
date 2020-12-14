using Konveyor.Core.ViewModels;

namespace Konveyor.Data.Contracts
{
    public interface ICustomerData
    {

        public CustomerListViewModel GetAllCustomers();

        public CustomerDetailViewModel GetCustomerDetails(long customerId);

        public CustomerEditViewModel GetCustomerForEdit(long? customerId);

        public bool RemoveCustomer(long? customerId);

        public bool SaveCustomerToDb(CustomerEditViewModel customerInfo);
    }
}
