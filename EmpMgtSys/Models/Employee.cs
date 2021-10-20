using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMgtSys.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("FirstName: ")]
        [Required]
        public string FirstName { get; set; }
        [DisplayName("LastName: ")]

        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public string department { get; set; }
  
    }
}