using Manage.Data;
using Manage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public EmployeeController(EmployeeDbContext employeeDbContext )
        {
            _employeeDbContext = employeeDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
          var employee = await _employeeDbContext.Employees.ToListAsync();
          return Ok(employee);
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddEmployee(Employee e)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_employeeDbContext.Employees.Any(emp => emp.UserName == e.UserName))
            {
                ModelState.AddModelError("UserName", "User Name must be unique.");
                return BadRequest(ModelState);
            }

            if (_employeeDbContext.Employees.Any(emp => emp.Email == e.Email))
            {
                ModelState.AddModelError("Email", "Email must be unique.");
                return BadRequest(ModelState);
            }

            await _employeeDbContext.Employees.AddAsync(e);
            await _employeeDbContext.SaveChangesAsync();
            return Ok(e);

        }
        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> getEmployee(int id)
        {
          var e = await _employeeDbContext.Employees.FirstOrDefaultAsync(x=> x.id==id);
            if(e == null)
            {
                return NotFound();
            }

            return Ok(e);
        }
        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> editEmployee( Employee employeechange)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (employeechange.id == null)
            {
                return NotFound();
            }
            if (_employeeDbContext.Employees.Any(emp => emp.UserName == employeechange.UserName && emp.id != employeechange.id))
            {
                ModelState.AddModelError("UserName", "User Name must be unique.");
                return BadRequest(ModelState);
            }

            if (_employeeDbContext.Employees.Any(emp => emp.Email == employeechange.Email && emp.id != employeechange.id))
            {
                ModelState.AddModelError("Email", "Email must be unique.");
                return BadRequest(ModelState);
            }
            _employeeDbContext.Employees.Update(employeechange);
            _employeeDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> deleteEmployee(int id)
        {
           var d = await _employeeDbContext.Employees.FindAsync(id);
            if(id == null)
            {
                return NotFound();
            }
             _employeeDbContext.Employees.Remove(d);
            await _employeeDbContext.SaveChangesAsync();

            return Ok(d);
        }
    }
}
