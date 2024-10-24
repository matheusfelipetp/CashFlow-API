using CashFlow.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace CashFlow.Infra.Security.Cryptography
{
    public class BCrypt : IPasswordEncripter
    {
        public string Encrypt(string password)
        {
            string passwordHase = BC.HashPassword(password);
            return passwordHase;
        }

        public bool Verify(string password, string passwordHase)
        {
            return BC.Verify(password, passwordHase);
        }
    }
}

