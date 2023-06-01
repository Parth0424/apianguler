using Manage.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manage.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext( DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
   
}
