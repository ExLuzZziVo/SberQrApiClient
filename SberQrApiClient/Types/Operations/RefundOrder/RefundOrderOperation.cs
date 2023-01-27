#region

using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CoreLib.CORE.Helpers.ObjectHelpers;
using CoreLib.CORE.Helpers.StringHelpers;
using CoreLib.CORE.Resources;
using Newtonsoft.Json;
using SberQrApiClient.Types.Converters;
using SberQrApiClient.Types.Enums;
using SberQrApiClient.Types.Interfaces;

#endregion

namespace SberQrApiClient.Types.Operations.RefundOrder
{
    /// <summary>
    /// Отмена/возврат финансовой операции
    /// </summary>
    public class RefundOrderOperation : Operation<RefundOrderResult>
    {
        /// <summary>
        /// Отмена/возврат финансовой операции
        /// </summary>
        /// <param name="orderId">Идентификатор заказа в АС ППРБ.Карты</param>
        /// <param name="operationId">Идентификатор операции в АС ППРБ.Карты, которую требуется отменить</param>
        /// <param name="authCode">Код авторизации</param>
        /// <param name="cancelOperationSum">Сумма отмены/возврата</param>
        /// <param name="idQr">Идентификатор устройства, на котором сформирован заказ</param>
        /// <param name="terminalId">Уникальный идентификатор терминала</param>
        public RefundOrderOperation(string orderId, string operationId, string authCode,
            decimal cancelOperationSum, string idQr = null, string terminalId = null) : base("/order/v3/cancel", "https://api.sberbank.ru/qr/order.cancel")
        {
            if (orderId.IsNullOrEmptyOrWhiteSpace() || orderId.Length > 36 ||
                !Regex.IsMatch(orderId, @"^[A-Za-z0-9_\\-]*$"))
            {
                throw new ArgumentException(
                    string.Format(
                        ValidationStrings.ResourceManager.GetString("StringFormatError"),
                        GetType().GetProperty(nameof(OrderId)).GetPropertyDisplayName()),
                    nameof(orderId));
            }

            if (operationId.IsNullOrEmptyOrWhiteSpace() || operationId.Length > 50 ||
                !Regex.IsMatch(operationId, @"^[A-Za-z0-9_\\-]*$"))
            {
                throw new ArgumentException(
                    string.Format(
                        ValidationStrings.ResourceManager.GetString("StringFormatError"),
                        GetType().GetProperty(nameof(OperationId)).GetPropertyDisplayName()),
                    nameof(operationId));
            }

            if (authCode.IsNullOrEmptyOrWhiteSpace() || authCode.Length > 8 ||
                !Regex.IsMatch(authCode, @"^[A-Za-z0-9_\\-]*$"))
            {
                throw new ArgumentException(
                    string.Format(
                        ValidationStrings.ResourceManager.GetString("StringFormatError"),
                        GetType().GetProperty(nameof(AuthCode)).GetPropertyDisplayName()),
                    nameof(authCode));
            }

            if (!cancelOperationSum.IsInRange(0.01m, 9999999999999.99m))
            {
                throw new ArgumentOutOfRangeException(nameof(cancelOperationSum),
                    string.Format(ValidationStrings.ResourceManager.GetString("DigitRangeValuesError"),
                        GetType().GetProperty(nameof(CancelOperationSum)).GetPropertyDisplayName(), 0.01,
                        9999999999999.99));
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
            
            if (!terminalId.IsNullOrEmptyOrWhiteSpace() && (terminalId.Length > 8 ||
                !Regex.IsMatch(terminalId, @"^[A-Za-z0-9_\\-]*$")))
            {
                throw new ArgumentException(
                    string.Format(
                        ValidationStrings.ResourceManager.GetString("StringFormatError"),
                        GetType().GetProperty(nameof(TerminalId)).GetPropertyDisplayName()),
                    nameof(terminalId));
            }
            
            OrderId = orderId;
            OperationId = operationId;
            AuthCode = authCode;
            IdQr = idQr;
            TerminalId = terminalId;
            CancelOperationSum = cancelOperationSum;
        }

        /// <summary>
        /// Идентификатор заказа в АС ППРБ.Карты
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// <item>Максимальная длина: 36</item>
        /// <item>Должно соответствовать регулярному выражению: "^[A-Za-z0-9_\\-]*$"</item>
        /// </list>
        [Display(Name = "Идентификатор заказа в АС ППРБ.Карты")]
        [JsonProperty("order_id")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(36, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(@"^[A-Za-z0-9_\\-]*$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string OrderId { get; }

        /// <summary>
        /// Тип операции
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// </list>
        [Display(Name = "Тип операции")]
        [JsonProperty("operation_type")]
        public RefundOperationType? OperationType { get; set; }

        /// <summary>
        /// Идентификатор операции в АС ППРБ.Карты, которую требуется отменить
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// <item>Максимальная длина: 50</item>
        /// <item>Должно соответствовать регулярному выражению: "^[A-Za-z0-9_\\-]*$"</item>
        /// </list>
        [Display(Name = "Идентификатор операции в АС ППРБ.Карты, которую требуется отменить")]
        [JsonProperty("operation_id")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(@"^[A-Za-z0-9_\\-]*$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string OperationId { get; }

        /// <summary>
        /// Код авторизации
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// <item>Должно соответствовать регулярному выражению: "^[A-Za-z0-9_\\-]*$"</item>
        /// </list>
        [Display(Name = "Код авторизации")]
        [JsonProperty("auth_code")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(8, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(@"^[A-Za-z0-9_\\-]*$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string AuthCode { get; }

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
        [JsonProperty("id_qr")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(36, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(@"^[A-Za-z0-9_\\-]*$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string IdQr { get; private set; }

        /// <summary>
        /// Уникальный идентификатор терминала
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// <item>Максимальная длина: 8</item>
        /// <item>Должно соответствовать регулярному выражению: "^[A-Za-z0-9_\\-]*$"</item>
        /// </list>
        [Display(Name = "Уникальный идентификатор терминала")]
        [JsonProperty("tid")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(8, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(@"^[A-Za-z0-9_\\-]*$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string TerminalId { get; private set; }

        /// <summary>
        /// Сумма отмены/возврата
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// <item>Должно лежать в диапазоне: 0.01-9999999999999.99</item>
        /// </list>
        [Display(Name = "Сумма отмены/возврата")]
        [JsonProperty("cancel_operation_sum")]
        [JsonConverter(typeof(AmountConverter))]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [Range(0.01, 9999999999999.99, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "DigitRangeValuesError")]
        public decimal CancelOperationSum { get; }

        /// <summary>
        /// Код валюты операции
        /// </summary>
        /// <remarks>
        /// ISO 4217
        /// Значение по умолчанию: 643(Российский рубль)
        /// </remarks>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// <item>Максимальная длина: 3</item>
        /// <item>Должно соответствовать регулярному выражению: <see cref="RegexExtensions.PositiveNumberPattern"/></item>
        /// </list>
        [Display(Name = "Код валюты операции")]
        [JsonProperty("operation_currency")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(3, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(RegexExtensions.PositiveNumberPattern, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string Currency { get; set; } = "643";

        /// <summary>
        /// Идентификатор получателя
        /// </summary>
        /// <remarks>
        /// Возможные значения: Номер мобильного телефона клиента физического лица в формате "+79001234567"
        /// </remarks>
        /// <list type="bullet">
        /// <item>Максимальная длина: 36</item>
        /// <item>Должно соответствовать регулярному выражению: "^[A-Za-z0-9_\\-\\s\\+()\\*]*$"</item>
        /// </list>
        [Display(Name = "Идентификатор получателя")]
        [JsonProperty("sbp_payer_id")]
        [MaxLength(36, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(@"^[A-Za-z0-9_\\-\\s\\+()\\*]*$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string SbpPayerId { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        /// <list type="bullet">
        /// <item>Максимальная длина: 140</item>
        /// <item>Спецсимволы требуется экранировать</item>
        /// </list>
        [Display(Name = "Назначение платежа")]
        [JsonProperty("operation_description")]
        [MaxLength(140, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        public string OperationDescription { get; set; }

        public override Task<RefundOrderResult> ExecuteAsync(HttpClient httpClient, ISberQrApiSettings apiSettings)
        {
            if (IdQr.IsNullOrEmptyOrWhiteSpace())
            {
                IdQr = apiSettings.IdQr;
            }
            
            if (TerminalId.IsNullOrEmptyOrWhiteSpace())
            {
                TerminalId = apiSettings.IdQr;
            }
            
            return base.ExecuteAsync(httpClient, apiSettings);
        }
    }
}