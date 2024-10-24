﻿using CashFlow.Infra.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infra.Migrations
{
    public static class DataBaseMigration
    {
        public static async Task MigrateDatabase(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<CashflowDbContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
