﻿using AutoMapper;
using CashFlow.Comunication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAllExpenses
{
    public class GetAllExpensesUseCase : IGetAllExpensesUseCase
    {
        private readonly IExpenseReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllExpensesUseCase(IExpenseReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseExpensesJson> Execute()
        {
            var result = await _repository.GetAll();

            return new ResponseExpensesJson
            {
                Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(result)
            };
        }
    }
}
