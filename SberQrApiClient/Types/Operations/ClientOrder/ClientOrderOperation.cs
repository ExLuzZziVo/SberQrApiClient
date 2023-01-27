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
using SberQrApiClient.Types.Interfaces;

#endregion

namespace SberQrApiClient.Types.Operations.ClientOrder
{
    /// <summary>
    /// Проведение платежа по Qr-коду покупателя
    /// </summary>
    public class ClientOrderOperation : Operation<ClientOrderResult>
    {
        /// <summary>
        /// Проведение платежа по Qr-коду покупателя
        /// </summary>
        /// <param name="orderNumber">Номер заказа в системе продавца</param>
        /// <param name="amount">Сумма заказа</param>
        /// <param name="clientQrPayload">Данные, полученные при сканировании Qr-кода, отображаемого у покупателя в приложении СБОЛ</param>
        /// <param name="operationDescription">Сообщение при платеже для отображения ТСТ</param>
        /// <param name="terminalId">Уникальный идентификатор терминала</param>
        /// <param name="merchantId">Идентификатор продавца</param>
        /// <param name="idQr">Идентификатор устройства, на котором сформирован заказ</param>
        public ClientOrderOperation(string orderNumber, decimal amount,
            string clientQrPayload, string operationDescription, string terminalId, string merchantId = null, string idQr = null) : base("/bscanc/v1/pay",
            "https://api.sberbank.ru/qr/order.pay")
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

            if (clientQrPayload.IsNullOrEmptyOrWhiteSpace() || clientQrPayload.Length > 512 ||
                !Regex.IsMatch(clientQrPayload, @"^[A-Za-z0-9:\\/\&?.=]*$"))
            {
                throw new ArgumentException(
                    string.Format(
                        ValidationStrings.ResourceManager.GetString("StringFormatError"),
                        GetType().GetProperty(nameof(ClientQrPayload)).GetPropertyDisplayName()),
                    nameof(clientQrPayload));
            }

            if (!amount.IsInRange(0.01m, 9999999999999.99m))
            {
                throw new ArgumentOutOfRangeException(nameof(amount),
                    string.Format(ValidationStrings.ResourceManager.GetString("DigitRangeValuesError"),
                        GetType().GetProperty(nameof(Amount)).GetPropertyDisplayName(), 0.01, 9999999999999.99));
            }

            if (operationDescription.IsNullOrEmptyOrWhiteSpace() || operationDescription.Length > 128)
            {
                throw new ArgumentException(
                    string.Format(
                        ValidationStrings.ResourceManager.GetString("StringFormatError"),
                        GetType().GetProperty(nameof(OperationDescription)).GetPropertyDisplayName()),
                    nameof(operationDescription));
            }

            if (terminalId.IsNullOrEmptyOrWhiteSpace() || terminalId.Length > 8 ||
                !Regex.IsMatch(terminalId, @"^[A-Za-z0-9_\\-]*$"))
            {
                throw new ArgumentException(
                    string.Format(
                        ValidationStrings.ResourceManager.GetString("StringFormatError"),
                        GetType().GetProperty(nameof(TerminalId)).GetPropertyDisplayName()),
                    nameof(terminalId));
            }
            
            if (!merchantId.IsNullOrEmptyOrWhiteSpace() && (merchantId.Length != 8 ||
                !Regex.IsMatch(merchantId, @"^[A-Za-z0-9_\\-]{8}$")))
            {
                throw new ArgumentException(
                    string.Format(
                        ValidationStrings.ResourceManager.GetString("StringFormatError"),
                        GetType().GetProperty(nameof(MerchantId)).GetPropertyDisplayName()),
                    nameof(merchantId));
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
            
            MerchantId = merchantId;
            OrderNumber = orderNumber;
            TerminalId = terminalId;
            IdQr = idQr;
            ClientQrPayload = clientQrPayload;
            Amount = amount;
            OperationDescription = operationDescription;
        }

        /// <summary>
        /// Идентификатор продавца
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// <item>Максимальная длина: 8</item>
        /// <item>Должно соответствовать регулярному выражению: "^[A-Za-z0-9_\\-]{8}$"</item>
        /// </list>
        [Display(Name = "Идентификатор продавца")]
        [JsonProperty("member_id")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(8, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(@"^[A-Za-z0-9_\\-]{8}$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string MerchantId { get; private set; }

        /// <summary>
        /// Номер заказа в системе продавца
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// <item>Максимальная длина: 36</item>
        /// <item>Должно соответствовать регулярному выражению: "^[A-Za-z0-9_\\-]*$"</item>
        /// </list>
        [Display(Name = "Номер заказа в системе продавца")]
        [JsonProperty("partner_order_number")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(36, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(@"^[A-Za-z0-9_\\-]*$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string OrderNumber { get; }

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
        public string TerminalId { get; }

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
        /// <item>Максимальная длина: 20</item>
        /// <item>Должно соответствовать регулярному выражению: "^[A-Za-z0-9_\\-]*$"</item>
        /// </list>
        [Display(Name = "Идентификатор устройства, на котором сформирован заказ")]
        [JsonProperty("id_qr")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(@"^[A-Za-z0-9_\\-]*$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string IdQr { get; private set; }

        /// <summary>
        /// Данные, полученные при сканировании Qr-кода, отображаемого у покупателя в приложении СБОЛ
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// <item>Максимальная длина: 512</item>
        /// <item>Должно соответствовать регулярному выражению: "^[A-Za-z0-9:\\/\&?.=]*$"</item>
        /// </list>
        [Display(Name = "Данные, полученные при сканировании Qr-кода, отображаемого клиентом в приложении СБОЛ")]
        [JsonProperty("pay_load")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(512, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(@"^[A-Za-z0-9:\\/\&?.=]*$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string ClientQrPayload { get; }

        /// <summary>
        /// Сумма заказа
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// <item>Должно лежать в диапазоне: 0.01-9999999999999.99</item>
        /// </list>
        [Display(Name = "Сумма заказа")]
        [JsonProperty("order_sum")]
        [JsonConverter(typeof(AmountConverter))]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [Range(0.01, 9999999999999.99, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "DigitRangeValuesError")]
        public decimal Amount { get; }

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
        [JsonProperty("currency")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(3, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(RegexExtensions.PositiveNumberPattern, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string Currency { get; set; } = "643";

        /// <summary>
        /// Сообщение при платеже для отображения ТСТ
        /// </summary>
        /// <remarks>
        /// При наличии технической возможности
        /// </remarks>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// <item>Максимальная длина: 128</item>
        /// </list>
        [Display(Name = "Сообщение при платеже для отображения ТСТ")]
        [JsonProperty("operation_message")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(128, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        public string OperationDescription { get; }

        public override Task<ClientOrderResult> ExecuteAsync(HttpClient httpClient, ISberQrApiSettings apiSettings)
        {
            if (MerchantId.IsNullOrEmptyOrWhiteSpace())
            {
                MerchantId = apiSettings.MerchantId;
            }

            if (IdQr.IsNullOrEmptyOrWhiteSpace())
            {
                IdQr = apiSettings.IdQr;
            }
            
            return base.ExecuteAsync(httpClient, apiSettings);
        }
    }
}