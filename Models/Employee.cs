using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Manage.Models
{
    public class Employee
    {
        
       public int id { get; set; }

    [Display(Name = "First Name")]
    
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
   
    public string LastName { get; set; }

    
    public int Age { get; set; }

    [Display(Name = "Contact Number")]
  
    public long Contact { get; set; }

   
    public string Email { get; set; }

    
    public string StreetAddress { get; set; }

    public string City { get; set; }

    public string State { get; set; }

 
    public string UserName { get; set; }

   
    public string Password { get; set; }
    }
}
