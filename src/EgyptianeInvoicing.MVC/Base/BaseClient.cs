using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;

namespace EgyptianeInvoicing.MVC.Base
{
    public abstract class BaseClient
    {
        protected readonly HttpClient HttpClient;
        protected readonly ILogger<BaseClient> Logger;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        protected BaseClient(HttpClient httpClient, ILogger<BaseClient> logger, IHttpContextAccessor httpContextAccessor)
        {
            HttpClient = httpClient;
            Logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        protected async Task<T> GetAsync<T>(string endpoint, string token = null)
        {
            try
            {
                token = _httpContextAccessor.HttpContext.Session.GetString("Token");
                var acceptLanguage = GetAcceptLanguage();
                AddAuthorizationHeader(token);
                HttpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(acceptLanguage.ToString()));
                HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await HttpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    //var resopnse = await response.Content.ReadAsStringAsync();
                    //return JsonSerializer.Deserialize<T>(resopnse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return await response.Content.ReadFromJsonAsync<T>();
                }
                else
                {
                    Logger.LogError($"Failed to get data from endpoint '{endpoint}'. Status code: {response.StatusCode}");
                    return default;
                }
            }
            catch (HttpRequestException ex)
            {
                Logger.LogError($"Error occurred while fetching data from endpoint '{endpoint}': {ex.Message}");
                throw;
            }
        }

        protected async Task PostAsync<T>(string endpoint, T content, string token = null)
        {
            try
            {
                token = _httpContextAccessor.HttpContext.Session.GetString("Token");
                var acceptLanguage = GetAcceptLanguage();
                AddAuthorizationHeader(token);
                HttpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(acceptLanguage.ToString()));
                HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await HttpClient.PostAsJsonAsync(endpoint, content);

                if (!response.IsSuccessStatusCode)
                {
                    Logger.LogError($"Failed to post data to endpoint '{endpoint}'. Status code: {response.StatusCode}");
                    throw new HttpRequestException($"Failed to post data to endpoint '{endpoint}'. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Logger.LogError($"Error occurred while posting data to endpoint '{endpoint}': {ex.Message}");
                throw;
            }
        }

        protected async Task PutAsync<T>(string endpoint, T content, string token = null)
        {
            try
            {
                token = _httpContextAccessor.HttpContext.Session.GetString("Token");
                var acceptLanguage = GetAcceptLanguage();
                AddAuthorizationHeader(token);
                HttpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(acceptLanguage.ToString()));
                HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await HttpClient.PutAsJsonAsync(endpoint, content);

                if (!response.IsSuccessStatusCode)
                {
                    Logger.LogError($"Failed to put data to endpoint '{endpoint}'. Status code: {response.StatusCode}");
                    throw new HttpRequestException($"Failed to put data to endpoint '{endpoint}'. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Logger.LogError($"Error occurred while putting data to endpoint '{endpoint}': {ex.Message}");
                throw;
            }
        }

        protected async Task DeleteAsync(string endpoint, string token = null)
        {
            try
            {
                token = _httpContextAccessor.HttpContext.Session.GetString("Token");
                var acceptLanguage = GetAcceptLanguage();
                AddAuthorizationHeader(token);
                var response = await HttpClient.DeleteAsync(endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    Logger.LogError($"Failed to delete data from endpoint '{endpoint}'. Status code: {response.StatusCode}");
                    throw new HttpRequestException($"Failed to delete data from endpoint '{endpoint}'. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Logger.LogError($"Error occurred while deleting data from endpoint '{endpoint}': {ex.Message}");
                throw;
            }
        }

        protected async Task<T2> GetAsync<T, T2>(string endpoint, string token = null)
        {
            try
            {
                token = _httpContextAccessor.HttpContext.Session.GetString("Token");
                var acceptLanguage = GetAcceptLanguage();
                AddAuthorizationHeader(token);
                HttpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(acceptLanguage.ToString()));
                HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await HttpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<T2>();
                }
                else
                {
                    Logger.LogError($"Failed to get data from endpoint '{endpoint}'. Status code: {response.StatusCode}");
                    return default;
                }
            }
            catch (HttpRequestException ex)
            {
                Logger.LogError($"Error occurred while fetching data from endpoint '{endpoint}': {ex.Message}");
                throw;
            }
        }

        protected async Task<T2> PostAsync<T, T2>(string endpoint, T content, string acceptHeader = "", string token = null)
        {
            try
            {
                token = _httpContextAccessor.HttpContext.Session.GetString("Token");
                var acceptLanguage = GetAcceptLanguage();
                AddAuthorizationHeader(token);
                HttpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(acceptLanguage.ToString()));

                if (!string.IsNullOrEmpty(acceptHeader))
                {
                    HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptHeader));
                }
                else
                {
                    HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                var response = await HttpClient.PostAsJsonAsync(endpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    // Read response content as a string
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Log the response content
                    Logger.LogInformation($"Response Content: {responseContent}");

                    // Deserialize the response content to the desired type T2
                    return await response.Content.ReadFromJsonAsync<T2>();
                }

                else
                {
                    Logger.LogError($"Failed to post data to endpoint '{endpoint}'. Status code: {response.StatusCode}");
                    throw new HttpRequestException($"Failed to post data to endpoint '{endpoint}'. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Logger.LogError($"Error occurred while posting data to endpoint '{endpoint}': {ex.Message}");
                throw;
            }
        }

        protected async Task<T2> PutAsync<T, T2>(string endpoint, T content, string token = null)
        {
            try
            {
                token = _httpContextAccessor.HttpContext.Session.GetString("Token");
                var acceptLanguage = GetAcceptLanguage();
                AddAuthorizationHeader(token);
                HttpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(acceptLanguage.ToString()));
                HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await HttpClient.PutAsJsonAsync(endpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<T2>();
                }
                else
                {
                    Logger.LogError($"Failed to put data to endpoint '{endpoint}'. Status code: {response.StatusCode}");
                    throw new HttpRequestException($"Failed to put data to endpoint '{endpoint}'. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Logger.LogError($"Error occurred while putting data to endpoint '{endpoint}': {ex.Message}");
                throw;
            }
        }

        protected async Task<T2> DeleteAsync<T2>(string endpoint, string token = null)
        {
            try
            {
                token = _httpContextAccessor.HttpContext.Session.GetString("Token");
                var acceptLanguage = GetAcceptLanguage();
                AddAuthorizationHeader(token);
                var response = await HttpClient.DeleteAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<T2>();
                }
                else
                {
                    Logger.LogError($"Failed to delete data from endpoint '{endpoint}'. Status code: {response.StatusCode}");
                    throw new HttpRequestException($"Failed to delete data from endpoint '{endpoint}'. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Logger.LogError($"Error occurred while deleting data from endpoint '{endpoint}': {ex.Message}");
                throw;
            }
        }

        private AcceptLanguage GetAcceptLanguage()
        {
            var currentCulture = CultureInfo.CurrentCulture;
            var currentLanguage = currentCulture.TwoLetterISOLanguageName.ToLower();

            if (Enum.TryParse(currentLanguage, ignoreCase: true, out AcceptLanguage acceptLanguage))
            {
                return acceptLanguage;
            }
            else
            {
                return AcceptLanguage.EnUS;
            }
        }

        private void AddAuthorizationHeader(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
    public enum AcceptLanguage
    {

        [System.Runtime.Serialization.EnumMember(Value = @"en-US")]
        EnUS = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"ar-EG")]
        ArEG = 1,

        [System.Runtime.Serialization.EnumMember(Value = @"de-DE")]
        DeDE = 2,

    }
}
