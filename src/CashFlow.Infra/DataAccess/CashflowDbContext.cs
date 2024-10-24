using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra.DataAccess
{
    internal class CashflowDbContext : DbContext
    {
        public CashflowDbContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

