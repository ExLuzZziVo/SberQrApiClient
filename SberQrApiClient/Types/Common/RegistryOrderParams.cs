#region

using System;
using System.ComponentModel.DataAnnotations;
using CoreLib.CORE.Helpers.Converters;
using Newtonsoft.Json;
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
        [JsonProperty("orderId")]
        public string Id { get; set; }

        /// <summary>
        /// Дата/время совершения операции
        /// </summary>
        [Display(Name = "Дата/время совершения операции")]
        [JsonProperty("operationDateTime")]
        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-ddTHH:mm:ssZ")]
        public DateTime OperationDateTime { get; set; }

        /// <summary>
        /// Номер заказа в системе продавца
        /// </summary>
        [Display(Name = "Номер заказа в системе продавца")]
        [JsonProperty("partnerOrderNumber")]
        public string OrderNumber { get; set; }

        /// <summary>
        /// Сумма заказа
        /// </summary>
        [Display(Name = "Сумма заказа")]
        [JsonProperty("amount")]
        [JsonConverter(typeof(AmountConverter))]
        public decimal Amount { get; set; }

        /// <summary>
        /// Код валюты операции
        /// </summary>
        /// <remarks>
        /// ISO 4217
        /// </remarks>
        [Display(Name = "Код валюты операции")]
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        [Display(Name = "Статус заказа")]
        [JsonProperty("order_state")]
        public OrderState OrderState { get; set; }

        /// <summary>
        /// Блок с перечнем операций, привязанных к данному заказу с детализацией по каждой операции
        /// </summary>
        [Display(Name = "Блок с перечнем операций, привязанных к данному заказу с детализацией по каждой операции")]
        [JsonProperty("orderOperationParams")]
        public RegistryOrderOperationsParams OrderOperationsParams { get; set; }
    }
}