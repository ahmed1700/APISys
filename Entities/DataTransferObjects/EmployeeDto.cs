using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
   public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }

    }


    public class EmployeeForManipulationDTO
    {
        [MaxLength(70, ErrorMessage = "Max Length is 70")]
        public string Name { get; set; }
        [Range(12, int.MaxValue, ErrorMessage = "Age is Required and can't be lower than 12")]
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Position Name is required")]
        [MaxLength(70, ErrorMessage = "Max Length is 70")]
        public string Position { get; set; }
    }
    public class EmployeeForCreateDto : EmployeeForManipulationDTO
    {
    }
    public class EmployeeForUpdateDto : EmployeeForManipulationDTO
    {
    }
}
