using CashFlow.Comunication.Responses;

namespace CashFlow.Application.UseCases.Expenses.GetAllExpenses
{
    public interface IGetAllExpensesUseCase
    {
        Task<ResponseExpensesJson> Execute();
    }
}
