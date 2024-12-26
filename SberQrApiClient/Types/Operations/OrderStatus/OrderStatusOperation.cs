#region

using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CoreLib.CORE.Helpers.ObjectHelpers;
using CoreLib.CORE.Helpers.StringHelpers;
using CoreLib.CORE.Resources;
using SberQrApiClient.Types.Interfaces;

#endregion

namespace SberQrApiClient.Types.Operations.OrderStatus
{
    /// <summary>
    /// Запрос статуса заказа
    /// </summary>
    public class OrderStatusOperation: Operation<OrderStatusResult>
    {
        /// <summary>
        /// Запрос статуса заказа
        /// </summary>
        /// <param name="orderNumber">Номер заказа в системе продавца</param>
        /// <param name="terminalId">Уникальный идентификатор терминала</param>
        public OrderStatusOperation(string orderNumber, string terminalId = null): base("/order/v3/status",
            "https://api.sberbank.ru/qr/order.status")
        {
            if (orderNumber.IsNullOrEmptyOrWhiteSpace() || orderNumber.Length > 36 ||
                !Regex.IsMatch(orderNumber, @"^[A-Za-z0-9_\\-]*$"))
            {
                throw new ArgumentException(
                    string.Format(
                        ValidationStrings.ResourceManager.GetString("StringFormatError"),
                        GetType().GetProperty(nameof(OrderNumber)).GetPropertyDisplayName()),
                    nameof(orderNumber));
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

            TerminalId = terminalId;
            OrderNumber = orderNumber;
        }

        /// <summary>
        /// Идентификатор заказа в АС ППРБ.Карты
        /// </summary>
        /// <list type="bullet">
        /// <item>Максимальная длина: 36</item>
        /// <item>Должно соответствовать регулярному выражению: "^[A-Za-z0-9_\\-]*$"</item>
        /// </list>
        [Display(Name = "Идентификатор заказа в АС ППРБ.Карты")]
        [JsonPropertyName("order_id")]
        [MaxLength(36, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(@"^[A-Za-z0-9_\\-]*$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string OrderId { get; set; }

        /// <summary>
        /// Уникальный идентификатор терминала
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// <item>Максимальная длина: 8</item>
        /// <item>Должно соответствовать регулярному выражению: "^[A-Za-z0-9_\\-]*$"</item>
        /// </list>
        [Display(Name = "Уникальный идентификатор терминала")]
        [JsonPropertyName("tid")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(8, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(@"^[A-Za-z0-9_\\-]*$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string TerminalId { get; private set; }

        /// <summary>
        /// Номер заказа в системе продавца
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// <item>Максимальная длина: 36</item>
        /// <item>Должно соответствовать регулярному выражению: "^[A-Za-z0-9_\\-]*$"</item>
        /// </list>
        [Display(Name = "Номер заказа в системе продавца")]
        [JsonPropertyName("partner_order_number")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(36, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(@"^[A-Za-z0-9_\\-]*$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string OrderNumber { get; }

        public override Task<OrderStatusResult> ExecuteAsync(HttpClient httpClient, ISberQrApiSettings apiSettings)
        {
            if (TerminalId.IsNullOrEmptyOrWhiteSpace())
            {
                TerminalId = apiSettings.IdQr;
            }

            return base.ExecuteAsync(httpClient, apiSettings);
        }
    }
}