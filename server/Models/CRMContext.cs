using Microsoft.EntityFrameworkCore;

namespace CRM
{
    public class CRMContext : DbContext
    {
        public CRMContext(DbContextOptions<CRMContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Lead> Lead { get; set; }

    }
}