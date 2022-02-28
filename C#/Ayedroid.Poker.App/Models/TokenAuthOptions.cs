using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Ayedroid.Poker.App.Models
{
    public class TokenAuthOptions
    {
        public string Audience { get; } = "Participant";
        public string Issuer { get; } = "Ayedroid.Poker.App";
        public SigningCredentials SigningCredentials { get; private set; }
        public RsaSecurityKey Key { get; private set; }

        public TokenAuthOptions()
        {
            Key = GenerateRandomKey();
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256);
        }

        private static RsaSecurityKey GenerateRandomKey()
        {
            using var rsa = new RSACryptoServiceProvider(2048);
            try
            {
                var keyParams = rsa.ExportParameters(true);
                return new RsaSecurityKey(keyParams);
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }
        }
    }
}
