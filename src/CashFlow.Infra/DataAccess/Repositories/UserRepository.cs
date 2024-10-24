﻿using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra.DataAccess.Repositories
{
    internal class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        private readonly CashflowDbContext _dbContext;

        public UserRepository(CashflowDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(User user)
        {
           await _dbContext.Users.AddAsync(user);
        }

        public async Task<bool> ExistActiveUserWithEmail(string email)
        {
           return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email));
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
        }
    }
}
