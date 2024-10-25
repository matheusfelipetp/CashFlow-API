using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Cryptography
{
    public class PasswordEncrypterBuilder
    {
        private readonly Mock<IPasswordEncrypter> _mock;

        public PasswordEncrypterBuilder()
        {
            _mock = new Mock<IPasswordEncrypter>();
            _mock.Setup(config => config.Encrypt(It.IsAny<string>())).Returns("!%fdfsd343242!");
        }

        public PasswordEncrypterBuilder Verify(string? password)
        {
            if(string.IsNullOrWhiteSpace(password) == false)
            {
                _mock.Setup(config => config.Verify(password, It.IsAny<string>())).Returns(true);
            }

            return this;
        }

        public IPasswordEncrypter Build() => _mock.Object;
    }
}
