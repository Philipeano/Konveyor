using Konveyor.Common.Utilities;
using Konveyor.Core.Models;
using Konveyor.Core.ViewModels;
using Konveyor.Data.Contracts;
using Konveyor.Data.SqlDataService.CustomTypes;
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
            List<SelectListItem> customerList = new List<SelectListItem>
            {
                new SelectListItem("- Please select -", null),
            };

            List<Customers> customers = dbcontext.Customers
                .Where(c => c.IsActive == true && c.User != null)
                .Include(c => c.User)
                .OrderBy(c => c.User.FirstName)
                .ThenBy(c => c.User.LastName)
                .ToList();

            foreach (Customers customer in customers)
            {
                customerList.Add(new SelectListItem($"{customer.User.FirstName} {customer.User.LastName}", customer.CustomerId.ToString()));
            }
            return customerList;
        }

        
        private List<SelectListItem> PopulateEmployees()
        {
            List<SelectListItem> employeeList = new List<SelectListItem>
            {
                new SelectListItem("- Please select -", null),
            };

            List<Employees> employees = dbcontext.Employees
                .Where(e => e.IsActive == true && e.User != null)
                .Include(e => e.User)
                .OrderBy(e => e.User.FirstName)
                .ThenBy(e => e.User.LastName)
                .ToList();

            foreach (Employees employee in employees)
            {
                employeeList.Add(new SelectListItem($"{employee.User.FirstName} {employee.User.LastName}", employee.EmployeeId.ToString()));
            }
            return employeeList;
        }


        private List<SelectListItem> PopulateLocations()
        {
            List<SelectListItem> officeList = new List<SelectListItem>
            {
                new SelectListItem("- Please select -", null),
            };

            List<Offices> offices = dbcontext.Offices
                .Where(o => o.IsActive == true)
                .OrderBy(o => o.OfficeName)
                .ToList();

            foreach (Offices office in offices)
            {
                officeList.Add(new SelectListItem(office.OfficeName, office.OfficeId.ToString()));
            }
            return officeList;
        }


        private List<SelectListItem> PopulateStatus()
        {
            List<SelectListItem> statusList = new List<SelectListItem>
            {
                new SelectListItem("- Please select -", null),
            };

            List<OrderStatus> statusValues = dbcontext.OrderStatus
                .Where(s => s.IsActive == true)
                .OrderBy(s => s.OrderStatusId)
                .ToList();

            foreach (OrderStatus status in statusValues)
            {
                statusList.Add(new SelectListItem(status.OrderStatus1, status.OrderStatusId.ToString()));
            }
            return statusList;
        }


        private OrderInformation GetOrderById(long id)
        {
            OrderInformation orderInfo = new OrderInformation
            {
                Order = dbcontext.Orders
                .Where(o => o.Sender != null && o.OriginOffice != null && o.DestinationOffice != null && o.OrderId == id)
                .Include(o => o.OriginOffice)
                .Include(o => o.DestinationOffice)
                .Include(o => o.Sender)
                .SingleOrDefault(),

                InitialUpdate = dbcontext.OrderUpdates
                .Where(u => u.OrderId == id)
                .OrderBy(u => u.EntryId)
                .Include(u => u.NewOrderStatus)
                .Include(u => u.ProcessedByNavigation)
                .First(),

                RecentUpdate = dbcontext.OrderUpdates
                .Where(u => u.OrderId == id)
                .OrderBy(u => u.EntryId)
                .Include(u => u.NewOrderStatus)
                .Include(u => u.ProcessedByNavigation)
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
            {
                return null;
            }

            List<OrderDetailViewModel> orderList = new List<OrderDetailViewModel>();

            foreach (Orders order in orders)
            {
                OrderUpdates initialUpdate = dbcontext.OrderUpdates
                    .Where(u => u.OrderId == order.OrderId)
                    .OrderBy(u => u.EntryId)
                    .Include(u => u.NewOrderStatus)
                    .Include(u => u.ProcessedBy)
                    .First();

                OrderUpdates recentUpdate = dbcontext.OrderUpdates
                    .Where(u => u.OrderId == order.OrderId)
                    .OrderBy(u => u.EntryId)
                    .Include(u => u.NewOrderStatus)
                    .Include(u => u.ProcessedBy)
                    .Last();

                OrderDetailViewModel orderInfo = new OrderDetailViewModel
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
            OrderInformation orderInfo = GetOrderById(orderId);
            if (orderInfo.Order == null)
            {
                return null;
            }

            OrderDetailViewModel orderDetails = new OrderDetailViewModel
            {
                OrderId = orderInfo.Order.OrderId,
                TrackingCode = orderInfo.Order.TrackingCode,
                RecipientName = orderInfo.Order.RecipientName,
                RecipientPhone = orderInfo.Order.RecipientPhone,
                RecipientAddress = orderInfo.Order.RecipientAddress,
                TotalCost = orderInfo.Order.TotalCost,
                ExpressService = orderInfo.Order.ExpressService,
                ExpectedNumOfDays = orderInfo.Order.ExpectedNumOfDays,
                SenderId = orderInfo.Order.SenderId,
                SenderName = $"{orderInfo.Order.Sender.User.FirstName} {orderInfo.Order.Sender.User.LastName}",
                OriginOfficeId = orderInfo.Order.OriginOfficeId,
                OriginOfficeName = orderInfo.Order.OriginOffice.OfficeName,
                DestinationOfficeId = orderInfo.Order.DestinationOfficeId,
                DestinationOfficeName = orderInfo.Order.DestinationOffice.OfficeName,

                DateInitiated = orderInfo.InitialUpdate.EntryDate,
                InitiatorId = (long)orderInfo.InitialUpdate.ProcessedBy,
                InitiatorName = $"{orderInfo.InitialUpdate.ProcessedByNavigation.User.FirstName} {orderInfo.InitialUpdate.ProcessedByNavigation.User.LastName}",

                CurrentStatusId = orderInfo.RecentUpdate.NewOrderStatusId,
                CurrentStatus = orderInfo.RecentUpdate.NewOrderStatus.OrderStatus1,
                Remarks = orderInfo.RecentUpdate.Remarks,
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
            OrderInformation orderInfo = GetOrderById(orderId);
            if (orderInfo.Order == null)
            {
                return null;
            }

            OrderEditViewModel orderForEdit = new OrderEditViewModel
            {
                OrderId = orderInfo.Order.OrderId,
                TrackingCode = orderInfo.Order.TrackingCode,
                RecipientName = orderInfo.Order.RecipientName,
                RecipientPhone = orderInfo.Order.RecipientPhone,
                RecipientAddress = orderInfo.Order.RecipientAddress,
                TotalCost = orderInfo.Order.TotalCost,
                ExpressService = orderInfo.Order.ExpressService,
                ExpectedNumOfDays = orderInfo.Order.ExpectedNumOfDays,

                OriginOptions = originOptions,
                DestinationOptions = destinationOptions,
                InitiatorOptions = attendantOptions,
                SenderOptions = senderOptions,

                InitiatorId = (long)orderInfo.InitialUpdate.ProcessedBy,
                DateInitiated = orderInfo.InitialUpdate.EntryDate,
                CurrentStatusId = orderInfo.RecentUpdate.NewOrderStatusId,
                Remarks = orderInfo.RecentUpdate.Remarks,

                NewStatusOptions = statusOptions,
                UpdaterOptions = attendantOptions,
                NewRemarks = string.Empty
            };


            //IMPORTANT:  Assign dropdownlist values for display purposes only. Be sure to disable them in the Edit view.
            orderForEdit.OriginOptions.Find(o => o.Value == orderInfo.Order.OriginOfficeId.ToString()).Selected = true;
            orderForEdit.DestinationOptions.Find(d => d.Value == orderInfo.Order.DestinationOfficeId.ToString()).Selected = true;
            orderForEdit.InitiatorOptions.Find(i => i.Value == orderInfo.InitialUpdate.ProcessedBy.ToString()).Selected = true;
            orderForEdit.SenderOptions.Find(s => s.Value == orderInfo.Order.SenderId.ToString()).Selected = true;

            orderForEdit.NewStatusOptions.Find(s => s.Value == orderInfo.RecentUpdate.NewOrderStatusId.ToString()).Selected = true;
            orderForEdit.UpdaterOptions.Find(u => u.Value == null).Selected = true;
            return orderForEdit;
        }


        public bool TrySaveOrderToDb(OrderEditViewModel orderInfo, out string errorMsg)
        {
            Orders orderToSave;
            OrderUpdates orderUpdateToSave = new OrderUpdates();

            if (orderInfo.OrderId > 0)
            {
                orderToSave = GetOrderById(orderInfo.OrderId).Order;

                orderUpdateToSave.NewOrderStatusId = (int)new SelectList(orderInfo.NewStatusOptions).SelectedValue;
                orderUpdateToSave.Remarks = orderInfo.NewRemarks;
                orderUpdateToSave.ProcessedBy = (long)new SelectList(orderInfo.UpdaterOptions).SelectedValue;
                orderUpdateToSave.EntryDate = DateTime.Now;
            }
            else
            {
                orderToSave = new Orders
                {
                    TrackingCode = CodeGenerator.GenerateCode("Order"),
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

                orderUpdateToSave.NewOrderStatusId = 0;
                orderUpdateToSave.Remarks = orderInfo.Remarks;
                orderUpdateToSave.ProcessedBy = (long)new SelectList(orderInfo.InitiatorOptions).SelectedValue;
                orderUpdateToSave.EntryDate = DateTime.Now;
            }

            try
            {
                orderUpdateToSave.Order = orderToSave;
                dbcontext.OrderUpdates.Add(orderUpdateToSave);
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
