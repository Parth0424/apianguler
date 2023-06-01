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
    [Required(ErrorMessage = "First Name is required.")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    [Required(ErrorMessage = "Last Name is required.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Age is required.")]
    [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
    public int Age { get; set; }

    [Display(Name = "Contact Number")]
    [Required(ErrorMessage = "Contact Number is required.")]
    [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Contact Number must be a 10-digit number.")]
    public long Contact { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email.")]
    public string Email { get; set; }

    
    public string StreetAddress { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    [Required(ErrorMessage = "UserName is required.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public string Password { get; set; }
    }
}
