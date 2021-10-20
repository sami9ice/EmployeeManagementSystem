using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMgtSys.Models
{
   public class UserAccount
    {


        [Key]
        public int UserID { get; set; }
        [DisplayName("StaffName ")]
        [Required]
        public string StaffName { get; set; }
        [DisplayName("Email: ")]
        [Required]
        public string StaffID { get; set; }
        [DisplayName("Email: ")]

        [Required]
        public string Email { get; set; }
        [DisplayName("Password: ")]

        [Required]
        public string Password { get; set; }
     
        [DisplayName("ComFirmPassword: ")]

        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }



       


     
    }
}
