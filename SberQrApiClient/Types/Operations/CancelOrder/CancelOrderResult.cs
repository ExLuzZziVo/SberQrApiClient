#region

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using SberQrApiClient.Types.Enums;

#endregion

namespace SberQrApiClient.Types.Operations.CancelOrder
{
    /// <summary>
    /// Результат отмены сформированного заказа
    /// </summary>
    /// <remarks>
    /// До проведения финансовой операции
    /// </remarks>
    public class CancelOrderResult : OperationResult
    {
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
    }
}