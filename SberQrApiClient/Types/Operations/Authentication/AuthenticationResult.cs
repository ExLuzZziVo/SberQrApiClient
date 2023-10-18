#region

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#endregion

namespace SberQrApiClient.Types.Operations.Authentication
{
    /// <summary>
    /// Результат аутентификации пользователя
    /// </summary>
    public class AuthenticationResult: OperationResult
    {
        /// <summary>
        /// Сгенерированный OAUTH-токен
        /// </summary>
        [Display(Name = "Сгенерированный OAUTH-токен")]
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Тип запрашиваемого токена
        /// </summary>
        /// <remarks>
        /// Всегда передается значение "Bearer"
        /// </remarks>
        [Display(Name = "Тип запрашиваемого токена")]
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// Время в секундах, в течение которого действует OAUTH-токен
        /// </summary>
        [Display(Name = "Время в секундах, в течение которого действует OAUTH-токен")]
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Список групп персональных данных, на получение которых выдан данный токен
        /// </summary>
        /// <remarks>
        /// Список групп персональных данных, на получение которых выдан данный токен. В список так же по умолчанию включается название сервиса API
        /// </remarks>
        [Display(Name = "Список групп персональных данных, на получение которых выдан данный токен")]
        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// Идентификатор сессии
        /// </summary>
        /// <remarks>
        /// Под каждый выпущенный токен создается сессия. Ее идентификатор передается в токене
        /// </remarks>
        [Display(Name = "Идентификатор сессии")]
        [JsonPropertyName("session_state")]
        public string SessionState { get; set; }
    }
}