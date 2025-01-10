using DAL.Mode;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    public class MyDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public MyDbContext(DbContextOptions options) : base(options) {}
    }
}
