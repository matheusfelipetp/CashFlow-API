using CashFlow.Comunication.Requests;
using CashFlow.Comunication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public interface IRegisterExpenseUseCase
    {
        Task<ResponseRegisterExpenseJson> Execute(RequestExpenseJson request);
    }
}
