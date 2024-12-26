#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CoreLib.CORE.Helpers.ObjectHelpers;
using CoreLib.CORE.Helpers.StringHelpers;
using CoreLib.CORE.Resources;
using SberQrApiClient.Resources;
using SberQrApiClient.Types.Interfaces;

#endregion

namespace SberQrApiClient.Types.Operations.Authentication
{
    /// <summary>
    /// Аутентификация пользователя
    /// </summary>
    public class AuthenticationOperation: Operation<AuthenticationResult>
    {
        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="scope">Область видимости запроса</param>
        public AuthenticationOperation(string scope): base("/tokens/v3/oauth", scope)
        {
            if (scope.IsNullOrEmptyOrWhiteSpace())
            {
                throw new ArgumentException(
                    string.Format(
                        ValidationStrings.ResourceManager.GetString("StringFormatError"),
                        GetType().GetProperty(nameof(Scope)).GetPropertyDisplayName()),
                    nameof(scope));
            }
        }

        /// <summary>
        /// Тип разрешения
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// </list>
        [Display(Name = "Тип разрешения")]
        [JsonPropertyName("grant_type")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        public string GrantType { get; } = "client_credentials";

        public override async Task<AuthenticationResult> ExecuteAsync(HttpClient httpClient,
            ISberQrApiSettings apiSettings)
        {
            if (apiSettings == null)
            {
                throw new ArgumentNullException(nameof(apiSettings));
            }

            if (httpClient == null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (apiSettings.ApiHost.IsNullOrEmptyOrWhiteSpace())
            {
                throw new InvalidOperationException(
                    ErrorStrings.ResourceManager.GetString("NoApiHostInApiSettingsError"));
            }

            if (apiSettings.ClientId.IsNullOrEmptyOrWhiteSpace() ||
                apiSettings.ClientSecret.IsNullOrEmptyOrWhiteSpace())
            {
                throw new ValidationException(ErrorStrings.ResourceManager.GetString("NoClientDataApiSettingsError"));
            }

            var dataToSend = new Dictionary<string, string>
            {
                { "grant_type", GrantType },
                { "scope", Scope }
            };

            var uriBuilder = new UriBuilder(apiSettings.ApiHost + "/" + ApiPath);

            string responseResult;

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, uriBuilder.Uri)
                   {
                       Content = new FormUrlEncodedContent(dataToSend)
                   })
            {
                requestMessage.Headers.Add("Accept", "application/json");

                requestMessage.Headers.Add("Authorization",
                    $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiSettings.ClientId}:{apiSettings.ClientSecret}"))}");

                requestMessage.Headers.Add("RqUID", RequestId);

                using (var response = await httpClient.SendAsync(requestMessage))
                {
                    responseResult = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new InvalidOperationException(string.Format(
                            ErrorStrings.ResourceManager.GetString("ApiRequestError"), responseResult));
                    }
                }
            }

            if (responseResult.IsNullOrEmptyOrWhiteSpace())
            {
                return null;
            }

            var result =
                JsonSerializer.Deserialize<AuthenticationResult>(responseResult, OperationResultJsonSerializerOptions);

            return result;
        }
    }
}