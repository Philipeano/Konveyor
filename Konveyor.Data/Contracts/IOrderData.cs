using Konveyor.Core.ViewModels;

namespace Konveyor.Data.Contracts
{
    public interface IOrderData
    {

        public OrderListViewModel GetAllOrders();

        public OrderDetailViewModel GetOrderDetails(long orderId);

        public OrderEditViewModel GetOrderForEdit(long? orderId);

        public bool RemoveOrder(long? orderId);

        public bool SaveOrderToDb(OrderEditViewModel orderInfo);
    }
}
