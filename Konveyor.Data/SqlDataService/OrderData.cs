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
    public class OrderData : IOrderData
    {

        private readonly KonveyorDbContext dbcontext;
        private readonly List<SelectListItem> originOptions;
        private readonly List<SelectListItem> destinationOptions;
        private readonly List<SelectListItem> senderOptions;
        private readonly List<SelectListItem> attendantOptions;
        private readonly List<SelectListItem> statusOptions;

        public OrderData(KonveyorDbContext dbContext)
        {
            dbcontext = dbContext;
            originOptions = PopulateLocations();
            destinationOptions = originOptions;
            senderOptions = PopulateCustomers();
            attendantOptions = PopulateEmployees();
            statusOptions = PopulateStatus();
        }

        private List<SelectListItem> PopulateCustomers()
        {
            List<SelectListItem> customerOptions = new List<SelectListItem>()
            {
                new SelectListItem("- Please select -", null),
            };

            var activeCustomers = dbcontext.Customers
                .Where(c => c.IsActive == true && c.User != null)
                .Include(c => c.User)
                .OrderBy(c => c.User.FirstName)
                .ThenBy(c => c.User.LastName).ToList();

            foreach (var customer in activeCustomers)
            {
                customerOptions.Add(new SelectListItem($"{customer.User.FirstName} {customer.User.LastName}", customer.CustomerId.ToString()));
            }
            return customerOptions;
        }

        private List<SelectListItem> PopulateEmployees()
        {
            List<SelectListItem> employeeOptions = new List<SelectListItem>()
            {
                new SelectListItem("- Please select -", null),
            };

            var activeEmployees = dbcontext.Employees
                .Where(e => e.IsActive == true && e.User != null)
                .Include(e => e.User)
                .OrderBy(e => e.User.FirstName)
                .ThenBy(e => e.User.LastName).ToList();

            foreach (var employee in activeEmployees)
            {
                employeeOptions.Add(new SelectListItem($"{employee.User.FirstName} {employee.User.LastName}", employee.EmployeeId.ToString()));
            }
            return employeeOptions;
        }

        public struct OrderInformation
        {
            public Orders order;
            public OrderUpdates initialUpdate;
            public OrderUpdates recentUpdate;
        }


        // =================================================================
        // Random number generation for tracking code
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int min = 1000000000, int max = int.MaxValue)
        {
            lock (syncLock)
            {
                return random.Next(min, max);
            }
        }
        // =================================================================

        private List<SelectListItem> PopulateLocations()
        {
            List<SelectListItem> locationOptions = new List<SelectListItem>()
            {
                new SelectListItem("- Please select -", null),
            };

            var availableOffices = dbcontext.Offices
                .Where(o => o.IsActive == true)
                .OrderBy(o => o.OfficeName).ToList();

            foreach (var office in availableOffices)
            {
                locationOptions.Add(new SelectListItem(office.OfficeName, office.OfficeId.ToString()));
            }
            return locationOptions;
        }

        private List<SelectListItem> PopulateStatus()
        {
            List<SelectListItem> statusOptions = new List<SelectListItem>()
            {
                new SelectListItem("- Please select -", null),
            };

            var statusValues = dbcontext.OrderStatus
                .Where(s => s.IsActive == true)
                .OrderBy(s => s.OrderStatusId).ToList();

            foreach (var status in statusValues)
            {
                statusOptions.Add(new SelectListItem(status.OrderStatus1, status.OrderStatusId.ToString()));
            }
            return statusOptions;
        }

        private OrderInformation GetOrderById(long id)
        {
            OrderInformation orderInfo = new OrderInformation
            {
                order = dbcontext.Orders
                .Where(o => o.Sender != null && o.OriginOffice != null && o.DestinationOffice != null && o.OrderId == id)
                .Include(o => o.OriginOffice)
                .Include(o => o.DestinationOffice)
                .Include(o => o.Sender)
                .SingleOrDefault(),

                initialUpdate = dbcontext.OrderUpdates
                .Where(u => u.OrderId == id)
                .OrderBy(u => u.EntryId)
                .Include(u => u.NewOrderStatus)
                .Include(u => u.ProcessedBy)
                .First(),

                recentUpdate = dbcontext.OrderUpdates
                .Where(u => u.OrderId == id)
                .OrderBy(u => u.EntryId)
                .Include(u => u.NewOrderStatus)
                .Include(u => u.ProcessedBy)
                .Last()
            };
            return orderInfo;
        }


        public List<OrderDetailViewModel> GetAllOrders()
        {
            IQueryable<Orders> orders = dbcontext.Orders
                .Where(o => o.Sender != null && o.OriginOffice != null && o.DestinationOffice != null)
                .Include(o => o.OriginOffice)
                .Include(o => o.DestinationOffice)
                .OrderBy(o => o.OrderId);

            if (!orders.Any())
                return null;

            List<OrderDetailViewModel> orderList = new List<OrderDetailViewModel>();

            foreach (var order in orders)
            {
                var initialUpdate = dbcontext.OrderUpdates
                    .Where(u => u.OrderId == order.OrderId)
                    .OrderBy(u => u.EntryId)
                    .Include(u => u.NewOrderStatus)
                    .Include(u => u.ProcessedBy)
                    .First();

                var recentUpdate = dbcontext.OrderUpdates
                    .Where(u => u.OrderId == order.OrderId)
                    .OrderBy(u => u.EntryId)
                    .Include(u => u.NewOrderStatus)
                    .Include(u => u.ProcessedBy)
                    .Last();

                var orderInfo = new OrderDetailViewModel()
                {
                    OrderId = order.OrderId,
                    TrackingCode = order.TrackingCode,
                    RecipientName = order.RecipientName,
                    RecipientPhone = order.RecipientPhone,
                    RecipientAddress = order.RecipientAddress,
                    TotalCost = order.TotalCost,
                    ExpressService = order.ExpressService,
                    ExpectedNumOfDays = order.ExpectedNumOfDays,
                    SenderId = order.SenderId,
                    SenderName = $"{order.Sender.User.FirstName} {order.Sender.User.LastName}",
                    OriginOfficeId = order.OriginOfficeId,
                    OriginOfficeName = order.OriginOffice.OfficeName,
                    DestinationOfficeId = order.DestinationOfficeId,
                    DestinationOfficeName = order.DestinationOffice.OfficeName,

                    DateInitiated = initialUpdate.EntryDate,
                    InitiatorId = (long)initialUpdate.ProcessedBy,
                    InitiatorName = $"{initialUpdate.ProcessedByNavigation.User.FirstName} {initialUpdate.ProcessedByNavigation.User.LastName}",

                    CurrentStatusId = recentUpdate.NewOrderStatusId,
                    CurrentStatus = recentUpdate.NewOrderStatus.OrderStatus1,
                    Remarks = recentUpdate.Remarks,
                };
                orderList.Add(orderInfo);
            }
            return orderList;
        }

        public OrderDetailViewModel GetOrderDetails(long orderId)
        {
            var orderInfo = GetOrderById(orderId);
            if (orderInfo.order == null)
                return null;

            OrderDetailViewModel orderDetails = new OrderDetailViewModel
            {

                OrderId = orderInfo.order.OrderId,
                TrackingCode = orderInfo.order.TrackingCode,
                RecipientName = orderInfo.order.RecipientName,
                RecipientPhone = orderInfo.order.RecipientPhone,
                RecipientAddress = orderInfo.order.RecipientAddress,
                TotalCost = orderInfo.order.TotalCost,
                ExpressService = orderInfo.order.ExpressService,
                ExpectedNumOfDays = orderInfo.order.ExpectedNumOfDays,
                SenderId = orderInfo.order.SenderId,
                SenderName = $"{orderInfo.order.Sender.User.FirstName} {orderInfo.order.Sender.User.LastName}",
                OriginOfficeId = orderInfo.order.OriginOfficeId,
                OriginOfficeName = orderInfo.order.OriginOffice.OfficeName,
                DestinationOfficeId = orderInfo.order.DestinationOfficeId,
                DestinationOfficeName = orderInfo.order.DestinationOffice.OfficeName,

                DateInitiated = orderInfo.initialUpdate.EntryDate,
                InitiatorId = (long)orderInfo.initialUpdate.ProcessedBy,
                InitiatorName = $"{orderInfo.initialUpdate.ProcessedByNavigation.User.FirstName} {orderInfo.initialUpdate.ProcessedByNavigation.User.LastName}",

                CurrentStatusId = orderInfo.recentUpdate.NewOrderStatusId,
                CurrentStatus = orderInfo.recentUpdate.NewOrderStatus.OrderStatus1,
                Remarks = orderInfo.recentUpdate.Remarks,
            };
            return orderDetails;
        }


        public OrderEditViewModel CreateNewOrder()
        {
            OrderEditViewModel orderForCreate = new OrderEditViewModel
            {
                OriginOptions = originOptions,
                DestinationOptions = destinationOptions,
                SenderOptions = senderOptions,
                InitiatorOptions = attendantOptions,
                ExpressService = false,
                ExpectedNumOfDays = 3,
                CurrentStatusId = 0,
            };
            orderForCreate.OriginOptions.Find(o => o.Value == null).Selected = true;
            orderForCreate.DestinationOptions.Find(d => d.Value == null).Selected = true;
            orderForCreate.SenderOptions.Find(s => s.Value == null).Selected = true;
            orderForCreate.InitiatorOptions.Find(i => i.Value == null).Selected = true;
            return orderForCreate;
        }


        public OrderEditViewModel GetOrderForEdit(long orderId)
        {
            var orderInfo = GetOrderById(orderId);
            if (orderInfo.order == null)
                return null;

            OrderEditViewModel orderForEdit = new OrderEditViewModel
            {
                OrderId = orderInfo.order.OrderId,
                TrackingCode = orderInfo.order.TrackingCode,
                RecipientName = orderInfo.order.RecipientName,
                RecipientPhone = orderInfo.order.RecipientPhone,
                RecipientAddress = orderInfo.order.RecipientAddress,
                TotalCost = orderInfo.order.TotalCost,
                ExpressService = orderInfo.order.ExpressService,
                ExpectedNumOfDays = orderInfo.order.ExpectedNumOfDays,

                OriginOptions = originOptions,
                DestinationOptions = destinationOptions,
                InitiatorOptions = attendantOptions,
                SenderOptions = senderOptions,

                InitiatorId = (long)orderInfo.initialUpdate.ProcessedBy,
                DateInitiated = orderInfo.initialUpdate.EntryDate,
                CurrentStatusId = orderInfo.recentUpdate.NewOrderStatusId,
                CurrentStatus = orderInfo.recentUpdate.NewOrderStatus.OrderStatus1,
                Remarks = orderInfo.recentUpdate.Remarks,

                NewStatusOptions = statusOptions,
                UpdaterOptions = attendantOptions,
                NewRemarks = string.Empty
            };


            //IMPORTANT:  Assign dropdownlist values for display purposes only. Be sure to disable them in the Edit view.
            orderForEdit.OriginOptions.Find(o => o.Value == orderInfo.order.OriginOfficeId.ToString()).Selected = true;
            orderForEdit.DestinationOptions.Find(d => d.Value == orderInfo.order.DestinationOfficeId.ToString()).Selected = true;
            orderForEdit.InitiatorOptions.Find(i => i.Value == orderInfo.initialUpdate.ProcessedBy.ToString()).Selected = true;
            orderForEdit.SenderOptions.Find(s => s.Value == orderInfo.order.SenderId.ToString()).Selected = true;

            orderForEdit.NewStatusOptions.Find(s => s.Value == orderInfo.recentUpdate.NewOrderStatusId.ToString()).Selected = true;
            orderForEdit.UpdaterOptions.Find(u => u.Value == null).Selected = true;
            return orderForEdit;
        }


        public void SaveOrderToDb(OrderEditViewModel orderInfo, out string errorMsg)
        {
            Orders orderToSave;
            OrderUpdates orderUpdateToSave = new OrderUpdates();

            if (orderInfo.OrderId > 0)
            {
                orderToSave = GetOrderById(orderInfo.OrderId).order;

                orderUpdateToSave.NewOrderStatusId = 0;
                orderUpdateToSave.Remarks = orderInfo.Remarks;
                orderUpdateToSave.ProcessedBy = (long)new SelectList(orderInfo.InitiatorOptions).SelectedValue;
                orderUpdateToSave.EntryDate = DateTime.Now;

            }
            else
            {
                orderToSave = new Orders
                {
                    TrackingCode = $"ORD{DateTime.Today.Year}{GetRandomNumber()}",
                    OriginOfficeId = (int)new SelectList(orderInfo.OriginOptions).SelectedValue,
                    DestinationOfficeId = (int)new SelectList(orderInfo.DestinationOptions).SelectedValue,
                    SenderId = (long)new SelectList(orderInfo.SenderOptions).SelectedValue,
                    RecipientName = orderInfo.RecipientName,
                    RecipientPhone = orderInfo.RecipientPhone,
                    RecipientAddress = orderInfo.RecipientAddress,
                    Remarks = orderInfo.Remarks,
                    TotalCost = orderInfo.TotalCost,
                    ExpressService = orderInfo.ExpressService,
                    ExpectedNumOfDays = orderInfo.ExpectedNumOfDays

                };
                
                orderUpdateToSave.NewOrderStatusId = (int)new SelectList(orderInfo.NewStatusOptions).SelectedValue;
                orderUpdateToSave.Remarks = orderInfo.NewRemarks;
                orderUpdateToSave.ProcessedBy = (long)new SelectList(orderInfo.UpdaterOptions).SelectedValue;
                orderUpdateToSave.EntryDate = DateTime.Now;
            }

            try
            {
                orderUpdateToSave.Order = orderToSave;

                //if (orderInfo.OrderId == 0)
                //{
                //    dbcontext.OrderUpdates.Add(orderUpdateToSave);
                //}
                //else
                //{
                    dbcontext.OrderUpdates.Add(orderUpdateToSave);
                //}
                dbcontext.SaveChanges();
                errorMsg = string.Empty;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
        }
    }
}
