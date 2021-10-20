using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EmpMgtSys.ViewModel
{
    public class UserAccountModel
    {


        [Key]
        public Guid UserID { get; set; }

        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Staff ID is required and it must be an integer number (0-9)")]

        public string StaffID { get; set; }
        [Required(ErrorMessage = "Staff Name is required")]
        public string StaffName { get; set; }

      


        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Please confrim your Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        //public string ResetPasswordCode { get; set; }
     

    }
}