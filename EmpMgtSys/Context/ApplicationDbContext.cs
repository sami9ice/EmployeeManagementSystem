 using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmpMgtSys.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext():base ("ApplicationDbConnection ")
        {

        }
        public DbSet<Models.Employee> Employees { get; set;   }
        public DbSet<Models.Role> Roles { get; set; }
        public DbSet<Models.UserAccount> UserAccounts { get; set; }
        public DbSet<Models.UsersInRoles> UsersInRole { get; set; }


    }
}