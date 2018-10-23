using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Models
{
    using System.Data.Entity;
    public class DataContext:DbContext

    {
       
        public DataContext() : base("DefaultConnection")//esta se toma del pryecto backend
        {

        }

        public System.Data.Entity.DbSet<Sales.Common.Models.Product> Products { get; set; }
    }


}

