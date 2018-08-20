using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Xipu.Models
{
    public class XipuDbContext : DbContext
    {
        public XipuDbContext():base("name=DefaultConnection")
        {
            
        }

        public DbSet<Chef> Chefs { get; set; }
    }
}