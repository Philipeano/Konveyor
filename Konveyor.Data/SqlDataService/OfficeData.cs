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
    public class OfficeData : IOfficeData
    {

        private readonly KonveyorDbContext dbcontext;
        private readonly List<SelectListItem> stateOptions;

        public OfficeData(KonveyorDbContext dbContext)
        {
            dbcontext = dbContext;
            stateOptions = PopulateStates();
        }

        // =================================================================
        private List<SelectListItem> PopulateStates()
        {
            List<SelectListItem> stateOptions = new List<SelectListItem>()
            {
                new SelectListItem("- Please select -", null),
            };

            var nigerianStates = dbcontext.NigerianStates
                .Where(s => s.IsActive == true)
                .OrderBy(s => s.State).ToList();

            foreach (var state in nigerianStates)
            {
                stateOptions.Add(new SelectListItem(state.State, state.StateId.ToString()));
            }
            return stateOptions;
        }


        private Offices GetOfficeById(int id)
        {
            Offices office = dbcontext.Offices
                .Include(o => o.State)
                .Where(o => o.IsActive == true && o.State != null && o.OfficeId == id)
                .SingleOrDefault();
            return office;
        }



        public List<OfficeDetailViewModel> GetAllOffices()
        {
            IQueryable<Offices> offices = dbcontext.Offices
                .Include(o => o.State)
                .Where(o => o.IsActive == true && o.State != null)
                .OrderBy(o => o.OfficeName);

            if (!offices.Any())
                return null;

            List<OfficeDetailViewModel> activeOffices = new List<OfficeDetailViewModel>();
            foreach (var office in offices)
            {
                var activeOffice = new OfficeDetailViewModel()
                {
                    OfficeId = office.OfficeId,
                    OfficeName = office.OfficeName,
                    EmailAddress = office.EmailAddress,
                    PhoneNumber = office.PhoneNumber,
                    Address = office.Address,
                    City = office.City,

                    StateId = office.StateId,
                    State = office.State.State,
                };
                activeOffices.Add(activeOffice);
            }
            return activeOffices;
        }


        public OfficeDetailViewModel GetOfficeDetails(int officeId)
        {
            Offices office = GetOfficeById(officeId);
            if (office == null)
                return null;

            OfficeDetailViewModel officeDetails = new OfficeDetailViewModel
            {
                OfficeId = office.OfficeId,
                OfficeName = office.OfficeName,
                EmailAddress = office.EmailAddress,
                PhoneNumber = office.PhoneNumber,
                Address = office.Address,
                City = office.City,

                StateId = office.StateId,
                State = office.State.State,
            };
            return officeDetails;
        }

        public OfficeEditViewModel CreateNewOffice()
        {
            OfficeEditViewModel officeForCreate = new OfficeEditViewModel
            {
                StateOptions = stateOptions
            };
            officeForCreate.StateOptions.Find(s => s.Value == null).Selected = true;
            return officeForCreate;
        }

        public OfficeEditViewModel GetOfficeForEdit(int officeId)
        {
            Offices office = GetOfficeById(officeId);
            if (office == null)
                return null;

            OfficeEditViewModel officeForEdit = new OfficeEditViewModel
            {
                OfficeId = office.OfficeId,
                OfficeName = office.OfficeName,
                EmailAddress = office.EmailAddress,
                PhoneNumber = office.PhoneNumber,
                Address = office.Address,
                City = office.City,

                StateId = office.StateId,
                StateOptions = stateOptions
            };
            officeForEdit.StateOptions.Find(s => s.Value == office.StateId.ToString()).Selected = true;
            return officeForEdit;
        }

        public void RemoveOffice(int officeId, out string errorMsg)
        {
            Offices office = GetOfficeById(officeId);
            if (office == null)
            {
                errorMsg = "The specified office does not exist.";
                return;
            }

            try
            {
                //dbcontext.Offices.Remove(office);
                dbcontext.Offices.Find(officeId).IsActive = false;
                dbcontext.SaveChanges();
                errorMsg = string.Empty;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
        }

        public void SaveOfficeToDB(OfficeEditViewModel officeInfo, out string errorMsg)
        {
            Offices officeToSave;

            if (officeInfo.OfficeId > 0)
            {
                officeToSave = GetOfficeById(officeInfo.OfficeId);
            }
            else
            {
                officeToSave = new Offices();
            }

            try
            {
                officeToSave.OfficeName = officeInfo.OfficeName;
                officeToSave.EmailAddress = officeInfo.EmailAddress;
                officeToSave.PhoneNumber = officeInfo.PhoneNumber;
                officeToSave.City = officeInfo.City;
                officeToSave.StateId = officeInfo.StateId;

                if (officeInfo.OfficeId == 0)
                {
                    dbcontext.Offices.Add(officeToSave);
                }
                else
                {
                    dbcontext.Offices.Update(officeToSave);
                }
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
