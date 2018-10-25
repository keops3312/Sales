using Sales.Common.Models;
using Sales.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sales.Backend.Models
{
    public class LocalDbContext:DbContext
    {
        public LocalDbContext() : base("DefaultConnection")
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}