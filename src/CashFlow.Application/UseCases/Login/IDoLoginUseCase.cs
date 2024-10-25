using CashFlow.Comunication.Requests;
using CashFlow.Comunication.Responses;

namespace CashFlow.Application.UseCases.Login
{
    public interface IDoLoginUseCase
    {
        Task<ResponseRegisterUserJson> Execute(RequestLoginJson request);
    }
}
