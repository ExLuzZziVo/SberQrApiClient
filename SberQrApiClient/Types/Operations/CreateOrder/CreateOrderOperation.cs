#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CoreLib.CORE.Helpers.Converters;
using CoreLib.CORE.Helpers.ObjectHelpers;
using CoreLib.CORE.Helpers.StringHelpers;
using CoreLib.CORE.Helpers.ValidationHelpers.Attributes;
using CoreLib.CORE.Resources;
using System.Text.Json.Serialization;
using SberQrApiClient.Resources;
using SberQrApiClient.Types.Common;
using SberQrApiClient.Types.Converters;
using SberQrApiClient.Types.Interfaces;

#endregion

namespace SberQrApiClient.Types.Operations.CreateOrder
{
    /// <summary>
    /// Создание заказа
    /// </summary>
    public class CreateOrderOperation : Operation<CreateOrderResult>
    {
        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <param name="orderNumber">Номер заказа в системе продавца</param>
        /// <param name="amount">Сумма заказа</param>
        /// <param name="orderCreationDate">Дата/время формирования заказа</param>
        /// <param name="merchantId">Идентификатор продавца</param>
        /// <param name="idQr">Идентификатор устройства, на котором сформирован заказ</param>
        public CreateOrderOperation(string orderNumber, decimal amount,
            DateTime orderCreationDate, string merchantId = null, string idQr = null) : base("/order/v3/creation", "https://api.sberbank.ru/qr/order.create")
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

            if (!amount.IsInRange(0.01m, 9999999999999.99m))
            {
                throw new ArgumentOutOfRangeException(nameof(amount),
                    string.Format(ValidationStrings.ResourceManager.GetString("DigitRangeValuesError"),
                        GetType().GetProperty(nameof(Amount)).GetPropertyDisplayName(), 0.01, 9999999999999.99));
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
            OrderCreationDate = orderCreationDate;
            IdQr = idQr;
            Amount = amount;
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
        [JsonPropertyName("member_id")]
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
        [JsonPropertyName("order_number")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(36, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(@"^[A-Za-z0-9_\\-]*$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string OrderNumber { get; }

        /// <summary>
        /// Дата/время формирования заказа
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// </list>
        [Display(Name = "Дата/время формирования заказа")]
        [JsonPropertyName("order_create_date")]
        [CustomDateTimeConverter("yyyy-MM-ddTHH:mm:ssZ")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        public DateTime OrderCreationDate { get; }

        /// <summary>
        /// Товарные позиции заказа
        /// </summary>
        /// <list type="bullet">
        /// <item>Сумма заказа(<see cref="Amount"/>) должна равняться сумме всех товарных позиций</item>
        /// </list>
        [Display(Name = "Товарные позиции заказа")]
        [JsonPropertyName("order_params_type")]
        [ComplexObjectCollectionValidation(AllowNullItems = false, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "ComplexObjectCollectionValidationError")]
        public OrderPosition[] OrderPositions { get; set; }

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
        [JsonPropertyName("id_qr")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(36, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(@"^[A-Za-z0-9_\\-]*$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string IdQr { get; private set; }

        /// <summary>
        /// Сумма заказа
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// <item>Должно лежать в диапазоне: 0.01-9999999999999.99</item>
        /// </list>
        [Display(Name = "Сумма заказа")]
        [JsonPropertyName("order_sum")]
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
        [JsonPropertyName("currency")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(3, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(RegexExtensions.PositiveNumberPattern, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string Currency { get; set; } = "643";

        /// <summary>
        /// Описание заказа
        /// </summary>
        /// <list type="bullet">
        /// <item>Максимальная длина: 256</item>
        /// <item>Спецсимволы требуется экранировать</item>
        /// </list>
        [Display(Name = "Описание заказа")]
        [JsonPropertyName("description")]
        [MaxLength(256, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        public string Description { get; set; }

        /// <summary>
        /// Проведение операции через СБП
        /// </summary>
        [Display(Name = "Проведение операции через СБП")]
        [JsonIgnore]
        public bool IsSbpPayment { get; set; }

        /// <summary>
        /// Идентификатор банка-участника \"ПАО СберБанк\" в СБП
        /// <para/>
        /// Значение по умолчанию: 100000000111, если <see cref="IsSbpPayment"/> равен true, иначе - null
        /// </summary>
        /// <list type="bullet">
        /// <item>Максимальная длина: 12</item>
        /// <item>Должно соответствовать регулярному выражению: <see cref="RegexExtensions.PositiveNumberPattern"/></item>
        /// </list>
        [Display(Name = "Идентификатор банка-участника \"ПАО СберБанк\" в СБП")]
        [JsonPropertyName("sbp_member_id")]
        [MaxLength(12, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(RegexExtensions.PositiveNumberPattern, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string SbpMemberId => IsSbpPayment ? "100000000111" : null;

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var orderPositionsSum = OrderPositions?.Sum(op => op.Sum);

            if (orderPositionsSum == null)
            {
                yield return ValidationResult.Success;
            }
            else if (orderPositionsSum != Amount)
            {
                yield return new ValidationResult(
                    ErrorStrings.ResourceManager.GetString("OrderPositionsSumNotEqualsAmountError"),
                    new[] { nameof(Amount) });
            }
        }

        public override Task<CreateOrderResult> ExecuteAsync(HttpClient httpClient, ISberQrApiSettings apiSettings)
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