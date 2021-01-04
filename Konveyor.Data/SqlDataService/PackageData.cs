using System;
using Konveyor.Core.Models;
using Konveyor.Core.ViewModels;
using Konveyor.Data.Contracts;
using Konveyor.Data.SqlDataService.CustomTypes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Konveyor.Data.SqlDataService
{
    public class PackageData : IPackageData
    {

        private readonly KonveyorDbContext dbcontext;
        private readonly OrderDetailViewModel orderInfo;
        private readonly List<SelectListItem> attendantOptions;
        private readonly List<SelectListItem> packageTypeOptions;
        private readonly List<SelectListItem> statusOptions;

        public PackageData(KonveyorDbContext dbContext, OrderDetailViewModel orderDetailVM)
        {
            dbcontext = dbContext;
            orderInfo = orderDetailVM;
            attendantOptions = PopulateEmployees();
            packageTypeOptions = PopulatePackageTypes();
            statusOptions = PopulateStatus();
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


        private List<SelectListItem> PopulatePackageTypes()
        {
            List<SelectListItem> packageTypeList = new List<SelectListItem>
            {
                new SelectListItem("- Please select -", null),
            };

            List<PackageTypes> packageTypes = dbcontext.PackageTypes
                .Where(t => t.IsActive == true)
                .OrderBy(t => t.TypeId)
                .ToList();

            foreach (PackageTypes type in packageTypes)
            {
                packageTypeList.Add(new SelectListItem(type.TypeName, type.TypeId.ToString()));
            }
            return packageTypeList;
        }


        private List<SelectListItem> PopulateStatus()
        {
            List<SelectListItem> statusList = new List<SelectListItem>
            {
                new SelectListItem("- Please select -", null),
            };

            List<PackageStatus> statusValues = dbcontext.PackageStatus
                .Where(s => s.IsActive == true)
                .OrderBy(s => s.PackageStatusId)
                .ToList();

            foreach (PackageStatus status in statusValues)
            {
                statusList.Add(new SelectListItem(status.PackageStatus1, status.PackageStatusId.ToString()));
            }
            return statusList;
        }


        private PackageInformation GetPackageById(long id)
        {
            PackageInformation packageInfo = new PackageInformation
            {
                Package = dbcontext.Packages
                .Where(p => p.Order != null && p.PackageType != null && p.PackageId == id)
                .Include(p => p.Order)
                .Include(p => p.PackageType)
                .SingleOrDefault(),

                InitialUpdate = dbcontext.PackageUpdates
                .Where(u => u.PackageId == id)
                .OrderBy(u => u.EntryId)
                .Include(u => u.NewPackageStatus)
                .Include(u => u.LoggedByNavigation)
                .First(),

                RecentUpdate = dbcontext.PackageUpdates
                .Where(u => u.PackageId == id)
                .OrderBy(u => u.EntryId)
                .Include(u => u.NewPackageStatus)
                .Include(u => u.LoggedByNavigation)
                .Last()
            };
            return packageInfo;
        }


        public List<PackageDetailViewModel> GetAllPackages(long orderId)
        {
            IQueryable<Packages> packages = dbcontext.Packages
                .Where(p => p.Order != null && p.PackageType != null && p.OrderId == orderId)
                .Include(p => p.Order)
                .Include(p => p.PackageType)
                .OrderBy(p => p.PackageId);

            if (!packages.Any())
            {
                return null;
            }

            List<PackageDetailViewModel> packageList = new List<PackageDetailViewModel>();

            foreach (Packages package in packages)
            {
                PackageUpdates initialUpdate = dbcontext.PackageUpdates
                    .Where(u => u.PackageId == package.PackageId)
                    .OrderBy(u => u.EntryId)
                    .Include(u => u.NewPackageStatus)
                    .Include(u => u.LoggedByNavigation)
                    .First();

                PackageUpdates recentUpdate = dbcontext.PackageUpdates
                    .Where(u => u.PackageId == package.PackageId)
                    .OrderBy(u => u.EntryId)
                    .Include(u => u.NewPackageStatus)
                    .Include(u => u.LoggedByNavigation)
                    .Last();

                PackageDetailViewModel packageInfo = new PackageDetailViewModel
                {
                    PackageId = package.PackageId,
                    PackageTypeId = package.PackageTypeId,
                    PackageType = package.PackageType.TypeName,
                    Description = package.Description,
                    Fragile = package.Fragile,
                    Weight = (double) package.Weight,
                    Volume = (double) package.Volume,

                    OrderId = package.OrderId,
                    OrderVM = orderInfo,

                    DateRecorded = initialUpdate.EntryDate,
                    RecorderId = (long)initialUpdate.LoggedBy,
                    RecorderName = $"{initialUpdate.LoggedByNavigation.User.FirstName} {initialUpdate.LoggedByNavigation.User.LastName}",

                    CurrentStatusId = recentUpdate.NewPackageStatusId,
                    CurrentStatus = recentUpdate.NewPackageStatus.PackageStatus1,
                    Remarks = recentUpdate.Remarks,
                };
                packageList.Add(packageInfo);
            }
            return packageList;
        }


        public PackageDetailViewModel GetPackageDetails(long packageId)
        {
            PackageInformation packageInfo = GetPackageById(packageId);
            if (packageInfo.Package == null)
            {
                return null;
            }

            PackageDetailViewModel packageDetails = new PackageDetailViewModel
            {
                PackageId = packageInfo.Package.PackageId,
                PackageTypeId = packageInfo.Package.PackageTypeId,
                PackageType = packageInfo.Package.PackageType.TypeName,
                Description = packageInfo.Package.Description,
                Fragile = packageInfo.Package.Fragile,
                Weight = (double)packageInfo.Package.Weight,
                Volume = (double)packageInfo.Package.Volume,

                OrderId = packageInfo.Package.OrderId,
                OrderVM = orderInfo,

                DateRecorded = packageInfo.InitialUpdate.EntryDate,
                RecorderId = (long)packageInfo.InitialUpdate.LoggedBy,
                RecorderName = $"{packageInfo.InitialUpdate.LoggedByNavigation.User.FirstName} {packageInfo.InitialUpdate.LoggedByNavigation.User.LastName}",

                CurrentStatusId = packageInfo.RecentUpdate.NewPackageStatusId,
                CurrentStatus = packageInfo.RecentUpdate.NewPackageStatus.PackageStatus1,
                Remarks = packageInfo.RecentUpdate.Remarks,
            };
            return packageDetails;
        }


        public PackageEditViewModel CreateNewPackage()
        {
            PackageEditViewModel packageForCreate = new PackageEditViewModel
            {                
                PackageTypeOptions = packageTypeOptions,
                Fragile = false,
                Weight = 0,
                Volume = 0,

                OrderId = orderInfo.OrderId,
                OrderVM = orderInfo,
            };
            packageForCreate.PackageTypeOptions.Find(o => o.Value == null).Selected = true;
            return packageForCreate;
        }


        public PackageEditViewModel GetPackageForEdit(long packageId)
        {
            PackageInformation packageInfo = GetPackageById(packageId);
            if (packageInfo.Package == null)
            {
                return null;
            }

            PackageEditViewModel packageForEdit = new PackageEditViewModel
            {
                PackageId = packageInfo.Package.PackageId,
                Description = packageInfo.Package.Description,
                Fragile = packageInfo.Package.Fragile,
                Weight = (double) packageInfo.Package.Weight,
                Volume = (double) packageInfo.Package.Volume,

                PackageTypeId = packageInfo.Package.PackageTypeId,
                PackageTypeOptions = packageTypeOptions,

                OrderId = orderInfo.OrderId,
                OrderVM = orderInfo,

                RecorderId = (long)packageInfo.InitialUpdate.LoggedBy,
                DateRecorded = packageInfo.InitialUpdate.EntryDate,
                CurrentStatusId = packageInfo.RecentUpdate.NewPackageStatusId,
                Remarks = packageInfo.RecentUpdate.Remarks,

                NewStatusOptions = statusOptions,
                UpdaterOptions = attendantOptions,
                NewRemarks = string.Empty
            };

            //IMPORTANT:  Assign dropdownlist values for display purposes only. Be sure to disable them in the Edit view.
            packageForEdit.PackageTypeOptions.Find(t => t.Value == packageInfo.Package.PackageTypeId.ToString()).Selected = true;
            packageForEdit.NewStatusOptions.Find(s => s.Value == packageInfo.RecentUpdate.NewPackageStatusId.ToString()).Selected = true;
            packageForEdit.UpdaterOptions.Find(u => u.Value == null).Selected = true;
            return packageForEdit;
        }


        public bool TrySavePackageToDb(PackageEditViewModel packageInfo, out string errorMsg)
        {
            Packages packageToSave;
            PackageUpdates packageUpdateToSave = new PackageUpdates();

            if (packageInfo.PackageId > 0)
            {
                packageToSave = GetPackageById(packageInfo.PackageId).Package;

                packageUpdateToSave.NewPackageStatusId = (int)new SelectList(packageInfo.NewStatusOptions).SelectedValue;
                packageUpdateToSave.Remarks = packageInfo.NewRemarks;
                packageUpdateToSave.LoggedBy = (long)new SelectList(packageInfo.UpdaterOptions).SelectedValue;
            }
            else
            {
                packageToSave = new Packages
                {
                    PackageTypeId = (int)new SelectList(packageInfo.PackageTypeOptions).SelectedValue,
                    Description = packageInfo.Description,
                    Fragile = packageInfo.Fragile,
                    Weight = packageInfo.Weight,
                    Volume = packageInfo.Volume,
                    OrderId = packageInfo.OrderVM.OrderId
                };
                packageUpdateToSave.NewPackageStatusId = 0;
                packageUpdateToSave.Remarks = packageInfo.Description;
                packageUpdateToSave.LoggedBy = (long)new SelectList(packageInfo.RecorderOptions).SelectedValue;
            }

            packageUpdateToSave.EntryDate = DateTime.Now;

            try
            {
                packageUpdateToSave.Package = packageToSave;
                dbcontext.PackageUpdates.Add(packageUpdateToSave);
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
