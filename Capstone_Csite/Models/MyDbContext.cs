using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Capstone_Csite.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() :  base("DbContextContext")
       {
              
    }
        public DbSet<SignUp> SignUp { get; set; }

        //public DbSet<Login> Login { get; set; }

    }
}