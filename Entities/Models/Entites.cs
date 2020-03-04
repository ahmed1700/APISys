using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Company
    {
        [Column("CompanyId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Company Name is required")]
        [MaxLength(70,ErrorMessage ="Max Length is 70")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address Name is required")]
        [MaxLength(255, ErrorMessage = "Max Length is 255")]
        public string Address { get; set; }
        public string Country { get; set; }
        // Navigation 
        public ICollection<Employee> Employees { get; set; }
    }
    public class Employee
    {
        [Column("EmployeeId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Employee Name is required")]
        [MaxLength(70, ErrorMessage = "Max Length is 70")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Position Name is required")]
        [MaxLength(70, ErrorMessage = "Max Length is 70")]
        public string Position { get; set; }
        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
