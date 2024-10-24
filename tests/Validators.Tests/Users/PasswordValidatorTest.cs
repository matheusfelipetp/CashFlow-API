﻿using CashFlow.Application.UseCases.Users;
using CashFlow.Comunication.Requests;
using CommonTestUtilities.Requests;
using FluentAssertions;
using FluentValidation;

namespace Validators.Tests.Users
{
    public class PasswordValidatorTest
    {

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        [InlineData("a")]
        [InlineData("aa")]
        [InlineData("aaa")]
        [InlineData("aaaa")]
        [InlineData("aaaaa")]
        [InlineData("aaaaaa")]
        [InlineData("aaaaaaa")]
        [InlineData("aaaaaaaa")]
        [InlineData("AAAAAAAA")]
        [InlineData("Aaaaaaaa")]
        [InlineData("Aaaaaaa1")]
        public void ErrorPasswordInvalid(string password)
        {
            var validator = new PasswordValidator<RequestRegisterUserJson>();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Password = password;

            var result = validator.IsValid(new ValidationContext<RequestRegisterUserJson>(new RequestRegisterUserJson()), password);

            result.Should().BeFalse();
        }
    }
}
