using System.Text.RegularExpressions;
using JwtStore.Core.SharedContext.Extensions;
using JwtStore.Core.SharedContext.ValueObjects;

namespace JwtStore.Core.AccountContext.ValueObjects
{
    public partial class Email : ValueObject
    {
        private const string Pattern = @"/\S+@\S+\. \S+/";

        public Email(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new Exception("Email inválido!");

            Address = address.Trim().ToLower();

            if (Address.Length < 5)
                throw new Exception("Email inválido!");

            if (!EmailRegex().IsMatch(Address))
                throw new Exception("Email inválido!");

        }

        public static implicit operator string(Email email) => email.ToString();

        public static implicit operator Email(string address) => new(address);

        public override string ToString() => Address;

        public string Address { get; }
        public string Hash => Address.ToBase64();

        [GeneratedRegex(Pattern)]
        private static partial Regex EmailRegex();
    }
}