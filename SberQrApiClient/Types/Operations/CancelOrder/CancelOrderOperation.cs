#region

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using CoreLib.CORE.Helpers.ObjectHelpers;
using CoreLib.CORE.Helpers.StringHelpers;
using CoreLib.CORE.Resources;
using System.Text.Json.Serialization;

#endregion

namespace SberQrApiClient.Types.Operations.CancelOrder
{
    /// <summary>
    /// Отмена сформированного заказа
    /// </summary>
    /// <remarks>
    /// До проведения финансовой операции
    /// </remarks>
    public class CancelOrderOperation : Operation<CancelOrderResult>
    {
        /// <summary>
        /// Отмена сформированного заказа
        /// </summary>
        /// <remarks>
        /// До проведения финансовой операции
        /// </remarks>
        /// <param name="orderId">Идентификатор заказа в АС ППРБ.Карты</param>
        public CancelOrderOperation(string orderId) : base("/order/v3/revocation",
            "https://api.sberbank.ru/qr/order.revoke")
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

            OrderId = orderId;
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
        [JsonPropertyName("order_id")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(36, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        [RegularExpression(@"^[A-Za-z0-9_\\-]*$", ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringFormatError")]
        public string OrderId { get; }
    }
}