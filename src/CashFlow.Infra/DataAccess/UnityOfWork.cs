using CashFlow.Domain.Repositories;

namespace CashFlow.Infra.DataAccess
{
    internal class UnityOfWork : IUnityOfWork
    {
        private readonly CashflowDbContext _dbContext;

        public UnityOfWork(CashflowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit() => _dbContext.SaveChanges();
    }
}
