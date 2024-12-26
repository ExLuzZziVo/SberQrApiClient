#region

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CoreLib.CORE.Helpers.Converters;
using SberQrApiClient.Types.Converters;
using SberQrApiClient.Types.Enums;

#endregion

namespace SberQrApiClient.Types.Common
{
    /// <summary>
    /// Параметры заказа
    /// </summary>
    public class RegistryOrderParams
    {
        /// <summary>
        /// Идентификатор заказа в АС ППРБ.Карты
        /// </summary>
        [Display(Name = "Идентификатор заказа в АС ППРБ.Карты")]
        [JsonPropertyName("orderId")]
        public string Id { get; set; }

        /// <summary>
        /// Дата/время совершения операции
        /// </summary>
        [Display(Name = "Дата/время совершения операции")]
        [JsonPropertyName("operationDateTime")]
        [CustomDateTimeConverter("yyyy-MM-ddTHH:mm:ssZ")]
        public DateTime OperationDateTime { get; set; }

        /// <summary>
        /// Номер заказа в системе продавца
        /// </summary>
        [Display(Name = "Номер заказа в системе продавца")]
        [JsonPropertyName("partnerOrderNumber")]
        public string OrderNumber { get; set; }

        /// <summary>
        /// Сумма заказа
        /// </summary>
        [Display(Name = "Сумма заказа")]
        [JsonPropertyName("amount")]
        [JsonConverter(typeof(AmountConverter))]
        public decimal Amount { get; set; }

        /// <summary>
        /// Код валюты операции
        /// </summary>
        /// <remarks>
        /// ISO 4217
        /// </remarks>
        [Display(Name = "Код валюты операции")]
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        [Display(Name = "Статус заказа")]
        [JsonPropertyName("order_state")]
        public OrderState OrderState { get; set; }

        /// <summary>
        /// Блок с перечнем операций, привязанных к данному заказу с детализацией по каждой операции
        /// </summary>
        [Display(Name = "Блок с перечнем операций, привязанных к данному заказу с детализацией по каждой операции")]
        [JsonPropertyName("orderOperationParams")]
        public RegistryOrderOperationsParams OrderOperationsParams { get; set; }
    }
}