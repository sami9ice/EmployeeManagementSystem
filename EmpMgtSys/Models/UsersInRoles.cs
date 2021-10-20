﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EmpMgtSys.Models
{
     public class UsersInRoles
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]


        public int Id { get; set; }

        public int RoleId { get; set; }
        //public string RoleName { get; set; }

        public virtual Role Role { get; set; }
        public int UserID { get; set; }

        public virtual UserAccount UserAccount { get; set; }

    }
}
