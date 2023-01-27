#region

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using SberQrApiClient.Types.Enums;

#endregion

namespace SberQrApiClient.Types.Operations.CreateOrder
{
    /// <summary>
    /// Результат создания заказа
    /// </summary>
    public class CreateOrderResult : OperationResult
    {
        /// <summary>
        /// Номер заказа в системе продавца
        /// </summary>
        [Display(Name = "Номер заказа в системе продавца")]
        [JsonProperty("order_number")]
        public string OrderNumber { get; set; }

        /// <summary>
        /// Идентификатор заказа в АС ППРБ.Карты
        /// </summary>
        [Display(Name = "Идентификатор заказа в АС ППРБ.Карты")]
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        [Display(Name = "Статус заказа")]
        [JsonProperty("order_state")]
        public OrderState? OrderState { get; set; }

        /// <summary>
        /// Ссылка на считывание QR-кода
        /// </summary>
        [Display(Name = "Ссылка на считывание QR-кода")]
        [JsonProperty("order_form_url")]
        public string OrderFormUrl { get; set; }
    }
}