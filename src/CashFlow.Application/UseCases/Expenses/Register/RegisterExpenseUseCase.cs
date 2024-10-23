﻿using AutoMapper;
using CashFlow.Comunication.Requests;
using CashFlow.Comunication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase : IRegisterExpenseUseCase
    {
        private readonly IExpensesWriteOnlyRepository _repository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;

        public RegisterExpenseUseCase(IExpensesWriteOnlyRepository repository,
            IUnityOfWork unityOfWork,
            IMapper mapper
            )
        {
            _repository = repository;
            _unityOfWork = unityOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseRegisterExpenseJson> Execute(RequestExpenseJson request)
        {
            Validate(request);

            var entity = _mapper.Map<Expense>(request);

            await _repository.Add(entity);
            await _unityOfWork.Commit();

            return _mapper.Map<ResponseRegisterExpenseJson>(entity);
        }

        private static void Validate(RequestExpenseJson request)
        {
            var validator = new ExpenseValidator();
            var result = validator.Validate(request);

            if(!result.IsValid)
            {
                var messages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(messages);
            }
        }
    }
}
 