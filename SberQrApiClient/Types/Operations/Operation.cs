#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CoreLib.CORE.Helpers.Converters;
using CoreLib.CORE.Helpers.StringHelpers;
using CoreLib.CORE.Resources;
using CoreLib.CORE.Types;
using SberQrApiClient.Resources;
using SberQrApiClient.Types.Interfaces;
using SberQrApiClient.Types.Operations.Authentication;

#endregion

namespace SberQrApiClient.Types.Operations
{
    public abstract class Operation<T>: IValidatableObject where T : OperationResult
    {
        protected static readonly JsonSerializerOptions OperationJsonSerializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        protected static readonly JsonSerializerOptions OperationResultJsonSerializerOptions =
            new JsonSerializerOptions(JsonSerializerDefaults.Web);

        /// <summary>
        /// Создание запроса к api
        /// </summary>
        /// <param name="apiPath">Абсолютный адрес запроса к api платежного шлюза</param>
        /// <param name="scope">Область видимости запроса</param>
        protected Operation(string apiPath, string scope)
        {
            ApiPath = apiPath;
            Scope = scope;
            RequestId = Guid.NewGuid().ToString("N");
            RequestDateTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Абсолютный адрес запроса к api платежного шлюза
        /// </summary>
        [JsonIgnore]
        internal string ApiPath { get; }

        /// <summary>
        /// Область видимости запроса
        /// </summary>
        [JsonIgnore]
        internal string Scope { get; }

        /// <summary>
        /// Уникальный идентификатор запроса
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// <item>Максимальная длина: 32</item>
        /// </list>
        [Display(Name = "Уникальный идентификатор запроса")]
        [JsonPropertyName("rq_uid")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(32, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression("^(([0-9]|[a-f]|[A-F]){32})$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public virtual string RequestId { get; }

        /// <summary>
        /// Дата/время формирования запроса
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// </list>
        [Display(Name = "Дата/время формирования запроса")]
        [JsonPropertyName("rq_tm")]
        [CustomDateTimeConverter("yyyy-MM-ddTHH:mm:ssZ")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        public virtual DateTime RequestDateTime { get; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return ValidationResult.Success;
        }

        /// <summary>
        /// Асинхронное выполнение текущего запроса
        /// </summary>
        /// <param name="apiSettings">Настройки подключения к api</param>
        /// <returns>Задача, представляющая асинхронную операцию выполнения текущего запроса к api</returns>
        public async Task<T> ExecuteAsync(ISberQrApiSettings apiSettings)
        {
            if (apiSettings == null)
            {
                throw new ArgumentNullException(nameof(apiSettings));
            }

            var certificate = await apiSettings.GetCertificateAsync();

            if (certificate == null)
            {
                throw new ValidationException(
                    ErrorStrings.ResourceManager.GetString("NoCertificateInApiSettingsError"));
            }

            using (var handler = new HttpClientHandler())
            {
                handler.ClientCertificates.Add(certificate);

                using (var httpClient = new HttpClient(handler))
                {
                    var result = await ExecuteAsync(httpClient, apiSettings);

                    return result;
                }
            }
        }

        /// <summary>
        /// Асинхронное выполнение текущего запроса при помощи <see cref="HttpClient"/>
        /// </summary>
        /// <param name="httpClient">HttpClient</param>
        /// <param name="apiSettings">Настройки подключения к api</param>
        /// <returns>Задача, представляющая асинхронную операцию выполнения текущего запроса к api</returns>
        public virtual async Task<T> ExecuteAsync(HttpClient httpClient, ISberQrApiSettings apiSettings)
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

            var validationResults = new List<ValidationResult>(32);

            Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true);

            if (validationResults.Count != 0)
            {
                throw new ExtendedValidationException(validationResults);
            }

            var authenticationOperation = new AuthenticationOperation(Scope);

            var authenticationResult = await authenticationOperation.ExecuteAsync(httpClient, apiSettings);

            var dataToSend = JsonSerializer.Serialize(this, GetType(), OperationJsonSerializerOptions);

            var uriBuilder = new UriBuilder(apiSettings.ApiHost + "/qr" + ApiPath);

            string responseResult;

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, uriBuilder.Uri)
                   {
                       Content = new StringContent(dataToSend, Encoding.UTF8, "application/json")
                   })
            {
                requestMessage.Headers.Add("Accept", "application/json");
                requestMessage.Headers.Add("Authorization", $"Bearer {authenticationResult.AccessToken}");
                requestMessage.Headers.Add("RqUID", RequestId);

                using (var response =
                       await httpClient.SendAsync(requestMessage))
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

            var result = JsonSerializer.Deserialize<T>(responseResult, OperationResultJsonSerializerOptions);

            return result;
        }
    }
}