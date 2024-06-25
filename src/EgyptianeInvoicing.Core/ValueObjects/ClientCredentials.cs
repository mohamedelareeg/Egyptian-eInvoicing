using BuildingBlocks.Primitives;
using BuildingBlocks.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.ValueObjects
{
    public sealed class ClientCredentials : ValueObject
    {
        public string ClientId { get; private set; }
        public string ClientSecret1 { get; private set; }
        public string ClientSecret2 { get; private set; }
        public string TokenPin { get; private set; }
        public string Certificate { get; private set; } = "MCDR CA 2018";

        private ClientCredentials() { }

        private ClientCredentials(string clientId, string clientSecret1, string clientSecret2, string tokenPin, string certificate)
        {
            ClientId = clientId;
            ClientSecret1 = clientSecret1;
            ClientSecret2 = clientSecret2;
            TokenPin = tokenPin;
            Certificate = certificate;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return ClientId;
            yield return ClientSecret1;
            yield return ClientSecret2;
            yield return TokenPin;
            yield return Certificate;
        }

        public static Result<ClientCredentials> Create(string clientId, string clientSecret1, string clientSecret2, string tokenPin, string certificate)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(clientId))
                errors.Add("ClientId is required.");

            if (string.IsNullOrEmpty(clientSecret1))
                errors.Add("ClientSecret1 is required.");

            if (string.IsNullOrEmpty(clientSecret2))
                errors.Add("ClientSecret2 is required.");

            if (string.IsNullOrEmpty(tokenPin))
                errors.Add("TokenPin is required.");

            if (errors.Any())
            {
                var errorMessage = string.Join("; ", errors);
                return Result.Failure<ClientCredentials>("ClientCredentials.Create", errorMessage);
            }

            var credentials = new ClientCredentials(clientId, clientSecret1, clientSecret2, tokenPin, certificate);
            return Result.Success(credentials);
        }

        public Result<bool> Modify(string clientId = null, string clientSecret1 = null, string clientSecret2 = null, string tokenPin = null, string? certificate = null)
        {
            var errors = new List<string>();

            if (clientId != null)
            {
                if (string.IsNullOrEmpty(clientId))
                    errors.Add("ClientId cannot be null or empty.");
                else
                    ClientId = clientId;
            }

            if (clientSecret1 != null)
            {
                if (string.IsNullOrEmpty(clientSecret1))
                    errors.Add("ClientSecret1 cannot be null or empty.");
                else
                    ClientSecret1 = clientSecret1;
            }

            if (clientSecret2 != null)
            {
                if (string.IsNullOrEmpty(clientSecret2))
                    errors.Add("ClientSecret2 cannot be null or empty.");
                else
                    ClientSecret2 = clientSecret2;
            }

            if (tokenPin != null)
            {
                if (string.IsNullOrEmpty(tokenPin))
                    errors.Add("TokenPin cannot be null or empty.");
                else
                    TokenPin = tokenPin;
            }
            if (certificate != null)
            {
                if (string.IsNullOrEmpty(certificate))
                    errors.Add("Certificate cannot be null or empty.");
                else
                    Certificate = certificate;
            }

            if (errors.Any())
            {
                var errorMessage = string.Join("; ", errors);
                return Result.Failure<bool>("ClientCredentials.Modify", errorMessage);
            }

            return Result.Success(true);
        }
    }

}
