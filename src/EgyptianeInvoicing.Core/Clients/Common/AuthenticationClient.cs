﻿using EgyptianeInvoicing.Core.Clients.Common.Abstractions;
using EgyptianeInvoicing.Core.Data.Abstractions.Repositories;
using EgyptianeInvoicing.Core.Services.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace EgyptianeInvoicing.Core.Clients.Common
{
    public class AuthenticationClient : IAuthenticationClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _idSrvClient;
        private readonly ICompanyRepository _companyRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationClient(IHttpClientFactory httpClientFactory, ICompanyRepository companyRepository, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _idSrvClient = httpClientFactory.CreateClient("IdSrvBaseUrl");
            _companyRepository = companyRepository;
             _configuration = new ConfigurationBuilder()
                .AddJsonFile("signersettings.json", optional: false, reloadOnChange: true)
                .Build();
            //_configuration = configuration;
        }
        //POST
        //{{idSrvBaseUrl}}/connect/token
        public async Task<string> LoginAndGetAccessTokenAsync(Guid companyId, string clientId, string clientSecret, string registrationNumber = null)
        {
            var environment = _configuration.GetSection("E-InvoiceEnvironment").Value;
            var envConfig = _configuration.GetSection($"E-InvoiceEnvironments:{environment}");
            var idSrvBaseUrl = envConfig["IdSrvBaseUrl"];
            //var clientId = envConfig["ClientId"];
            //var clientSecret = envConfig["ClientSecret"];

            var formData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("scope", "InvoicingAPI")
            };

            if (!string.IsNullOrEmpty(registrationNumber))
            {
                _idSrvClient.DefaultRequestHeaders.Add("onbehalfof", registrationNumber);
            }

            var body = new FormUrlEncodedContent(formData);

            var response = await _idSrvClient.PostAsync($"{idSrvBaseUrl}/connect/token", body);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseBody);
                var accessToken = tokenResponse.access_token;
                await _companyRepository.SaveCompanyTokenAsync(companyId, accessToken);
                return tokenResponse.access_token;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                var errorDetails = JsonSerializer.Deserialize<ErrorResponse>(errorResponse);
                throw new HttpRequestException($"Bad request: {errorDetails.error_description}");
            }
            else
            {
                throw new HttpRequestException($"Unexpected status code: {response.StatusCode}");
            }
        }
    }
}
