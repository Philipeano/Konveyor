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

        public bool TrySaveOrderToDb(OrderEditViewModel orderInfo, out string errorMsg);
    }
}
