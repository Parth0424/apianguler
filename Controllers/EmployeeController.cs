using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Manage.Data;
using Manage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Manage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            var employees = await _employeeDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddEmployee(Employee e)
        {
            if (string.IsNullOrEmpty(e.FirstName))
            {
                ModelState.AddModelError("FirstName", "First Name is required.");
            }

            if (string.IsNullOrEmpty(e.LastName))
            {
                ModelState.AddModelError("LastName", "Last Name is required.");
            }
            if (e.Age == 0)
            {
                ModelState.AddModelError("Age", "Age is required.");
            }
            else if (!int.TryParse(e.Age.ToString(), out int age))
            {
                ModelState.AddModelError("Age", "Age must be a numeric value.");
            }
            else if (age < 18 || age > 100)
            {
                ModelState.AddModelError("Age", "Age must be between 18 and 100.");
            }

            if (string.IsNullOrEmpty(e.Contact.ToString()))
            {
                ModelState.AddModelError("Contact", "Contact Number is required.");
            }
            else if (!long.TryParse(e.Contact.ToString(), out long contact))
            {
                ModelState.AddModelError("Contact", "Contact Number must be a numeric value.");
            }
            else if (e.Contact.ToString().Length != 10)
            {
                ModelState.AddModelError("Contact", "Contact Number must be a 10-digit number.");
            }

            if (string.IsNullOrEmpty(e.Email))
            {
                ModelState.AddModelError("Email", "Email is required.");
            }
            else if (!IsValidEmail(e.Email))
            {
                ModelState.AddModelError("Email", "Invalid Email.");
            }
            else if (_employeeDbContext.Employees.Any(emp => emp.Email == e.Email))
            {
                ModelState.AddModelError("Email", "Email Name must be unique.");
            }

            if (string.IsNullOrEmpty(e.UserName))
            {
                ModelState.AddModelError("UserName", "UserName is required.");
            }
            else if (_employeeDbContext.Employees.Any(emp => emp.UserName == e.UserName))
            {
                ModelState.AddModelError("UserName", "User Name must be unique.");
            }

            if (string.IsNullOrEmpty(e.Password) || e.Password.Length < 6)
            {
                ModelState.AddModelError("Password", "Password must be at least 6 characters.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _employeeDbContext.Employees.AddAsync(e);
            await _employeeDbContext.SaveChangesAsync();
            return Ok(e);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _employeeDbContext.Employees.FirstOrDefaultAsync(x => x.id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> EditEmployee(Employee employeeChange)
        {
            if (employeeChange.id == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(employeeChange.FirstName))
            {
                ModelState.AddModelError("FirstName", "First Name is required.");
            }

            if (string.IsNullOrEmpty(employeeChange.LastName))
            {
                ModelState.AddModelError("LastName", "Last Name is required.");
            }



            if (employeeChange.Age == 0)
            {
                ModelState.AddModelError("Age", "Age is required.");
            }
            else if (!int.TryParse(employeeChange.Age.ToString(), out int age))
            {
                ModelState.AddModelError("Age", "Age must be a numeric value.");
            }
            else if (age < 18 || age > 100)
            {
                ModelState.AddModelError("Age", "Age must be between 18 and 100.");
            }

            if (string.IsNullOrEmpty(employeeChange.Contact.ToString()))
            {
                ModelState.AddModelError("Contact", "Contact Number is required.");
            }
            else if (!long.TryParse(employeeChange.Contact.ToString(), out long contact))
            {
                ModelState.AddModelError("Contact", "Contact Number must be a numeric value.");
            }
            else if (employeeChange.Contact.ToString().Length != 10)
            {
                ModelState.AddModelError("Contact", "Contact Number must be a 10-digit number.");
            }

            if (string.IsNullOrEmpty(employeeChange.Email))
            {
                ModelState.AddModelError("Email", "Email is required.");
            }
            else if (!IsValidEmail(employeeChange.Email))
            {
                ModelState.AddModelError("Email", "Invalid Email.");
            }
            //else if (_employeeDbContext.Employees.Any(emp => emp.Email == employeeChange.Email))
            //{
            //    ModelState.AddModelError("Email", "Email Name must be unique.");
            //}

            if (string.IsNullOrEmpty(employeeChange.UserName))
            {
                ModelState.AddModelError("UserName", "UserName is required.");
            }
            else if (_employeeDbContext.Employees.Any(emp => emp.UserName == employeeChange.UserName && emp.id != employeeChange.id))
            {
                ModelState.AddModelError("UserName", "User Name must be unique.");
            }
            if (string.IsNullOrEmpty(employeeChange.Password) || employeeChange.Password.Length < 6)
            {
                ModelState.AddModelError("Password", "Password must be at least 6 characters.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _employeeDbContext.Employees.Update(employeeChange);
            _employeeDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _employeeDbContext.Employees.Remove(employee);
            await _employeeDbContext.SaveChangesAsync();

            return Ok(employee);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
