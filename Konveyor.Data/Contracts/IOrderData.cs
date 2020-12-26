using Konveyor.Core.ViewModels;
using System.Collections.Generic;

namespace Konveyor.Data.Contracts
{
    public interface IOrderData
    {

        public List<OrderDetailViewModel> GetAllOrders();

        public OrderDetailViewModel GetOrderDetails(long orderId);

        public OrderEditViewModel CreateNewOrder();

        public OrderEditViewModel GetOrderForEdit(long orderId);

        // public void RemoveOrder(long orderId, out string errorMsg);

        public void SaveOrderToDb(OrderEditViewModel orderInfo, out string errorMsg);
    }
}
