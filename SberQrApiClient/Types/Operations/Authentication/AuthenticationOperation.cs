#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoreLib.CORE.Helpers.ObjectHelpers;
using CoreLib.CORE.Helpers.StringHelpers;
using CoreLib.CORE.Resources;
using CoreLib.CORE.Types;
using Newtonsoft.Json;
using SberQrApiClient.Resources;
using SberQrApiClient.Types.Interfaces;

#endregion

namespace SberQrApiClient.Types.Operations.Authentication
{
    /// <summary>
    /// Аутентификация пользователя
    /// </summary>
    public class AuthenticationOperation
    {
        private IDictionary<string, object> _formData;

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="scope">Область видимости запроса</param>
        public AuthenticationOperation(string scope)
        {
            if (scope.IsNullOrEmptyOrWhiteSpace())
            {
                throw new ArgumentException(
                    string.Format(
                        ValidationStrings.ResourceManager.GetString("StringFormatError"),
                        GetType().GetProperty(nameof(Scope)).GetPropertyDisplayName()),
                    nameof(scope));
            }

            Scope = scope;
            RequestId = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// Уникальный идентификатор запроса
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// <item>Максимальная длина: 32</item>
        /// <item>Должно соответствовать регулярному выражению: "^(([0-9]|[a-f]|[A-F]){32})$"</item>
        /// </list>
        [Display(Name = "Уникальный идентификатор запроса")]
        [JsonIgnore]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(32, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression("^(([0-9]|[a-f]|[A-F]){32})$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string RequestId { get; }

        /// <summary>
        /// Область видимости запроса
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// </list>
        [Display(Name = "Область видимости запроса")]
        [JsonProperty("scope")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        public string Scope { get; }

        /// <summary>
        /// Тип разрешения
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// </list>
        [Display(Name = "Тип разрешения")]
        [JsonProperty("grant_type")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        public string GrantType { get; } = "client_credentials";

        /// <summary>
        /// Данные для выполнения запроса
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, object> FormData
        {
            get => _formData ?? (_formData = new Dictionary<string, object>());
            set => _formData = value;
        }

        /// <summary>
        /// Асинхронная аутентификация пользователя
        /// </summary>
        /// <param name="apiSettings">Настройки подключения к api</param>
        /// <returns>Задача, представляющая асинхронную операцию аутентификации пользователя</returns>
        public async Task<AuthenticationResult> ExecuteAsync(ISberQrApiSettings apiSettings)
        {
            using (var httpClient = new HttpClient())
            {
                var result = await ExecuteAsync(httpClient, apiSettings);

                return result;
            }
        }

        /// <summary>
        /// Асинхронная аутентификация пользователя при помощи <see cref="HttpClient"/>
        /// </summary>
        /// <param name="httpClient">HttpClient</param>
        /// <param name="apiSettings">Настройки подключения к api</param>
        /// <returns>Задача, представляющая асинхронную операцию аутентификации пользователя</returns>
        public async Task<AuthenticationResult> ExecuteAsync(HttpClient httpClient, ISberQrApiSettings apiSettings)
        {
            if (apiSettings == null)
            {
                throw new ArgumentNullException(nameof(apiSettings));
            }

            if (httpClient == null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (apiSettings.ClientId.IsNullOrEmptyOrWhiteSpace() ||
                apiSettings.ClientSecret.IsNullOrEmptyOrWhiteSpace())
            {
                throw new ValidationException(ErrorStrings.ResourceManager.GetString("NoClientDataApiSettingsError"));
            }

            var validationResults = new List<ValidationResult>(32);

            Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true);

            if (validationResults.Count() != 0)
            {
                throw new ExtendedValidationException(validationResults);
            }

            var serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore, TypeNameHandling = TypeNameHandling.None
            };

            var serializedData = JsonConvert.SerializeObject(this,
                Formatting.None, serializerSettings);

            var dataToSend =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedData, serializerSettings);

            string responseResult;

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api.sberbank.ru/prod/tokens/v3/oauth")
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

            var result = JsonConvert.DeserializeObject<AuthenticationResult>(responseResult);

            return result;
        }
    }
}