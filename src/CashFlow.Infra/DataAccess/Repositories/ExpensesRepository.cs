using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infra.DataAccess.Repositories
{
    internal class ExpensesRepository : IExpensesRepository
    {

        private readonly CashflowDbContext _dbContext;

        public ExpensesRepository(CashflowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Expense expense)
        {
            _dbContext.Expenses.Add(expense);
        }
    }
}
