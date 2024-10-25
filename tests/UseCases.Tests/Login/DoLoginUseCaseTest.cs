﻿using CashFlow.Application.UseCases.Login;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using FluentAssertions;

namespace UseCases.Tests.Login
{
    public class DoLoginUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();
            var request = RequestLoginJsonBuilder.Build();
            request.Email = user.Email;

            var useCase = CreateUseCase(user, request.Password);
            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Name.Should().Be(user.Name);
            result.Token.Should().NotBeNull();
        }

        [Fact]
        public async Task ErrorUserNotFound()
        {
            var user = UserBuilder.Build();
            var request = RequestLoginJsonBuilder.Build();

            var useCase = CreateUseCase(user, request.Password);

            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<InvalidLoginException>();

            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID));
        }

        [Fact]
        public async Task ErrorPasswordNotMatch()
        {
            var user = UserBuilder.Build();

            var request = RequestLoginJsonBuilder.Build();
            request.Email = user.Email;

            var useCase = CreateUseCase(user);

            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<InvalidLoginException>();

            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID));
        }

        private DoLoginUseCase CreateUseCase(CashFlow.Domain.Entities.User user, string? password = null)
        {
            var tokenGenerator = JwtTokenGeneratorBuilder.Build();
            var readRepository = new UserReadOnlyRepositoryBuilder().GetUserByEmail(user).Build();
            var passwordEncriper = new PasswordEncrypterBuilder().Verify(password).Build();

            return new DoLoginUseCase(readRepository, passwordEncriper, tokenGenerator);
        }
    }
}