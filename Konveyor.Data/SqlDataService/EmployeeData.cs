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
    public class EmployeeData : IEmployeeData
    {

        private readonly KonveyorDbContext dbcontext;
        private readonly List<SelectListItem> genderOptions;
        private readonly List<SelectListItem> roleOptions;


        public EmployeeData(KonveyorDbContext dbContext)
        {
            dbcontext = dbContext;
            genderOptions = new List<SelectListItem> {
                new SelectListItem("- Please select -", string.Empty),
                new SelectListItem("Male", "Male"),
                new SelectListItem("Female", "Female")
            };
            roleOptions = PopulateRoles();
        }

        // =================================================================
        // Random number generation for employee code
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

        private List<SelectListItem> PopulateRoles()
        {
            List<SelectListItem> roleOptions = new List<SelectListItem>()
            {
                new SelectListItem("- Please select -", null),
            };
            
            var predefinedRoles = dbcontext.Roles
                .Where(r => r.IsActive == true && r.RoleId > 0 && r.RoleId < 4)
                .OrderBy(r => r.RoleId).ToList();

            foreach (var role in predefinedRoles)
            {
                roleOptions.Add(new SelectListItem(role.RoleName, role.RoleId.ToString()));
            }
            return roleOptions;
        }


        private Employees GetEmployeeById(long id)
        {
            Employees employee = dbcontext.Employees
                .Include(e => e.User)
                .Include(e => e.Role)
                .Where(e => e.IsActive == true && e.User != null && e.Role != null && e.EmployeeId == id)
                .SingleOrDefault();
            return employee;
        }


        public List<EmployeeDetailViewModel> GetAllEmployees()
        {
            IQueryable<Employees> employees = dbcontext.Employees
                .Include(e => e.User)
                .Include(e => e.Role)
                .Where(e => e.IsActive == true && e.User != null && e.Role != null);

            if (!employees.Any())
                return null;

            List<EmployeeDetailViewModel> activeEmployees = new List<EmployeeDetailViewModel>();
            foreach (var employee in employees)
            {
                var activeEmployee = new EmployeeDetailViewModel()
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeCode = employee.EmployeeCode,
                    Designation = employee.Designation,

                    RoleId = employee.RoleId,
                    RoleName = employee.Role.RoleName,

                    UserId = employee.User.UserId,
                    FirstName = employee.User.FirstName,
                    LastName = employee.User.LastName,
                    EmailAddress = employee.User.EmailAddress,
                    PhoneNumber = employee.User.PhoneNumber,
                    Gender = employee.User.Gender
                };
                activeEmployees.Add(activeEmployee);
            }
            return activeEmployees;
        }


        public EmployeeDetailViewModel GetEmployeeDetails(long employeeId)
        {
            Employees employee = GetEmployeeById(employeeId);
            if (employee == null)
                return null;

            EmployeeDetailViewModel employeeDetails = new EmployeeDetailViewModel
            {
                EmployeeId = employee.EmployeeId,
                EmployeeCode = employee.EmployeeCode,
                Designation = employee.Designation,

                RoleId = employee.RoleId,
                RoleName = employee.Role.RoleName,

                UserId = employee.User.UserId,
                FirstName = employee.User.FirstName,
                LastName = employee.User.LastName,
                EmailAddress = employee.User.EmailAddress,
                PhoneNumber = employee.User.PhoneNumber,
                Gender = employee.User.Gender
            };
            return employeeDetails;
        }


        public EmployeeEditViewModel CreateNewEmployee()
        {
            EmployeeEditViewModel employeeForCreate = new EmployeeEditViewModel
            {
                GenderOptions = genderOptions,
                RoleOptions = roleOptions
            };
            employeeForCreate.GenderOptions.Find(g => g.Value == string.Empty || g.Value == null).Selected = true;
            employeeForCreate.RoleOptions.Find(r => r.Value == null).Selected = true;
            return employeeForCreate;
        }


        public EmployeeEditViewModel GetEmployeeForEdit(long employeeId)
        {
            Employees employee = GetEmployeeById(employeeId);
            if (employee == null)
                return null;

            EmployeeEditViewModel employeeForEdit = new EmployeeEditViewModel
            {
                EmployeeId = employee.EmployeeId,
                EmployeeCode = employee.EmployeeCode,
                Designation = employee.Designation,
                RoleId = employee.RoleId,

                UserId = employee.User.UserId,
                FirstName = employee.User.FirstName,
                LastName = employee.User.LastName,
                EmailAddress = employee.User.EmailAddress,
                PhoneNumber = employee.User.PhoneNumber,
                Gender = employee.User.Gender,
                GenderOptions = genderOptions,
                RoleOptions = roleOptions
            };
            employeeForEdit.GenderOptions.Find(g => g.Value == employee.User.Gender).Selected = true;
            employeeForEdit.RoleOptions.Find(r => r.Value == employee.RoleId.ToString()).Selected = true;
            return employeeForEdit;
        }


        public void RemoveEmployee(long employeeId, out string errorMsg)
        {
            Employees employee = GetEmployeeById(employeeId);
            if (employee == null)
            {
                errorMsg = "The specified employee does not exist.";
                return;
            }

            try
            {
                //dbcontext.Employees.Remove(employee);
                dbcontext.Employees.Find(employeeId).IsActive = false;
                dbcontext.SaveChanges();
                errorMsg = string.Empty;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
        }


        public void SaveEmployeeToDb(EmployeeEditViewModel employeeInfo, out string errorMsg)
        {
            Employees employeeToSave;
            Users userToSave;
            // Roles userRole;

            if (employeeInfo.EmployeeId > 0)
            {
                employeeToSave = GetEmployeeById(employeeInfo.EmployeeId);
                userToSave = employeeToSave.User;
                // userRole = employeeToSave.Role;
            }
            else
            {
                employeeToSave = new Employees();
                userToSave = new Users();
                // userRole = new Roles();
                employeeToSave.EmployeeCode = $"EMPLOYEE{GetRandomNumber()}";
            }

            try
            {
                employeeToSave.Designation = employeeInfo.Designation;
                employeeToSave.RoleId = employeeInfo.RoleId;
                employeeToSave.LastUpdated = DateTime.Now;
                userToSave.FirstName = employeeInfo.FirstName;
                userToSave.LastName = employeeInfo.LastName;
                userToSave.EmailAddress = employeeInfo.EmailAddress;
                userToSave.PhoneNumber = employeeInfo.PhoneNumber;
                userToSave.Gender = employeeInfo.Gender;
                userToSave.Password = employeeInfo.Password;
                employeeToSave.User = userToSave;

                if (employeeInfo.EmployeeId == 0)
                {
                    dbcontext.Employees.Add(employeeToSave);
                }
                else
                {
                    dbcontext.Employees.Update(employeeToSave);
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
