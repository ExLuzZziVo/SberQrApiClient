#region

using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CoreLib.CORE.Helpers.Converters;
using CoreLib.CORE.Helpers.ObjectHelpers;
using CoreLib.CORE.Helpers.StringHelpers;
using CoreLib.CORE.Helpers.ValidationHelpers.Attributes;
using CoreLib.CORE.Resources;
using Newtonsoft.Json;
using SberQrApiClient.Types.Enums;
using SberQrApiClient.Types.Interfaces;

#endregion

namespace SberQrApiClient.Types.Operations.OrderRegistry
{
    /// <summary>
    /// Запрос реестра операций
    /// </summary>
    public class OrderRegistryOperation : Operation<OrderRegistryResult>
    {
        /// <summary>
        /// Запрос реестра операций
        /// </summary>
        /// <param name="startPeriod">Дата/время начала периода создания заказа в АС ППРБ.Карты по МСК</param>
        /// <param name="endPeriod">Дата/время конца периода создания заказа в АС ППРБ.Карты по МСК</param>
        /// <param name="registryType">Тип реестра</param>
        /// <param name="idQr">Идентификатор устройства, на котором сформирован заказ</param>
        public OrderRegistryOperation(DateTime startPeriod, DateTime endPeriod, RegistryType registryType, string idQr = null)
            : base("/order/v3/registry", "auth://qr/order.registry")
        {
            if (startPeriod > endPeriod)
            {
                throw new ArgumentOutOfRangeException(nameof(endPeriod), string.Format(
                    ValidationStrings.ResourceManager.GetString("CompareToGreaterThanOrEqualError"),
                    GetType().GetProperty(nameof(EndPeriod)).GetPropertyDisplayName(),
                    GetType().GetProperty(nameof(StartPeriod)).GetPropertyDisplayName()));
            }

            if (!idQr.IsNullOrEmptyOrWhiteSpace() && (idQr.Length > 20 ||
                !Regex.IsMatch(idQr, @"^[A-Za-z0-9_\\-]*$")))
            {
                throw new ArgumentException(
                    string.Format(
                        ValidationStrings.ResourceManager.GetString("StringFormatError"),
                        GetType().GetProperty(nameof(IdQr)).GetPropertyDisplayName()),
                    nameof(idQr));
            }
            
            IdQr = idQr;
            StartPeriod = startPeriod;
            EndPeriod = endPeriod;
            RegistryType = registryType;
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
        [JsonProperty("rqUid")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(32, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression("^(([0-9]|[a-f]|[A-F]){32})$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public override string RequestId { get; }

        /// <summary>
        /// Дата/время формирования запроса
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// </list>
        [Display(Name = "Дата/время формирования запроса")]
        [JsonProperty("rqTm")]
        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-ddTHH:mm:ssZ")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        public override DateTime RequestDateTime { get; }

        /// <summary>
        /// Идентификатор устройства, на котором сформирован заказ
        /// <para>Правила заполнения:</para>
        /// <list type="bullet">
        /// <item>для операции "Qr-код Продавца": Уникальный идентификатор устройства в системе "Плати QR"(<see cref="IdQr"/>);</item>
        /// <item>для операции "Qr-код СБП": Уникальный идентификатор терминала(<see cref="TerminalId"/>).</item>
        /// </list>
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// <item>Максимальная длина: 36</item>
        /// <item>Должно соответствовать регулярному выражению: "^[A-Za-z0-9_\\-]*$"</item>
        /// </list>
        [Display(Name = "Идентификатор устройства, на котором сформирован заказ")]
        [JsonProperty("idQR")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(36, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(@"^[A-Za-z0-9_\\-]*$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string IdQr { get; private set; }

        /// <summary>
        /// Дата/время начала периода создания заказа в АС ППРБ.Карты по МСК
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// </list>
        [Display(Name = "Дата/время начала периода создания заказа в АС ППРБ.Карты по МСК")]
        [JsonProperty("startPeriod")]
        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-ddTHH:mm:ssZ")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        public DateTime StartPeriod { get; }

        /// <summary>
        /// Дата/время конца периода создания заказа в АС ППРБ.Карты по МСК
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// <item>Должно быть больше или равно: <see cref="StartPeriod"/></item>
        /// </list>
        [Display(Name = "Дата/время конца периода создания заказа в АС ППРБ.Карты по МСК")]
        [JsonProperty("endPeriod")]
        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-ddTHH:mm:ssZ")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [CompareTo(nameof(StartPeriod), ComparisonType.GreaterOrEqual,
            ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "CompareToGreaterThanOrEqualError")]
        public DateTime EndPeriod { get; }

        /// <summary>
        /// Тип реестра
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// </list>
        [Display(Name = "Тип реестра")]
        [JsonProperty("registryType")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        public RegistryType RegistryType { get; }

        public override Task<OrderRegistryResult> ExecuteAsync(HttpClient httpClient, ISberQrApiSettings apiSettings)
        {
            if (IdQr.IsNullOrEmptyOrWhiteSpace())
            {
                IdQr = apiSettings.IdQr;
            }
            
            return base.ExecuteAsync(httpClient, apiSettings);
        }
    }
}