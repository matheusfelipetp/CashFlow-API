using AutoMapper;
using CashFlow.Comunication.Requests;
using CashFlow.Comunication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IMapper _mapper;
        private readonly IPasswordEncrypter _passwordEncripter;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IAccessTokenGenerator _tokenGenerator;

        public RegisterUserUseCase(
            IMapper mapper, 
            IPasswordEncrypter passwordEncripter, 
            IUserReadOnlyRepository userReadOnlyRepository, 
            IUserWriteOnlyRepository userWriteOnlyRepository, 
            IUnityOfWork unityOfWork,
            IAccessTokenGenerator tokenGenerator
            )
        {
            _mapper = mapper;
            _passwordEncripter = passwordEncripter;
            _userReadOnlyRepository = userReadOnlyRepository;
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _unityOfWork = unityOfWork;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request);

            var user = _mapper.Map<User>(request);
            user.Password = _passwordEncripter.Encrypt(request.Password);
            user.UserIdentifier = Guid.NewGuid();

            await _userWriteOnlyRepository.Add(user);
            await _unityOfWork.Commit();

            return new ResponseRegisterUserJson
            {
                Name = user.Name,
                Token = _tokenGenerator.Generate(user),
            };
        }

        private async Task Validate(RequestRegisterUserJson request)
        {
            var result = new RegisterUserValidator().Validate(request);
            var emailExists = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

            if (emailExists)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
            }

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
