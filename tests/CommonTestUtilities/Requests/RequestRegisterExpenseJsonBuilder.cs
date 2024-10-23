﻿using Bogus;
using CashFlow.Comunication.Enums;
using CashFlow.Comunication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestRegisterExpenseJsonBuilder
    {
        public static RequestRegisterExpenseJson Build()
        {
            return new Faker<RequestRegisterExpenseJson>()
                .RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
                .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
                .RuleFor(r => r.Date, faker => faker.Date.Past())
                .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentsType>())
                .RuleFor(r => r.Amount, faker => faker.Random.Decimal(min: 1, max: 1000));
        }
    }
}