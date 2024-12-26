#region

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SberQrApiClient.Types.Common;
using SberQrApiClient.Types.Enums;

#endregion

namespace SberQrApiClient.Types.Operations.OrderStatus
{
    /// <summary>
    /// Результат запроса статуса заказа
    /// </summary>
    public class OrderStatusResult: OperationResult
    {
        /// <summary>
        /// Идентификатор продавца
        /// </summary>
        [Display(Name = "Идентификатор продавца")]
        [JsonPropertyName("mid")]
        public string MerchantId { get; set; }

        /// <summary>
        /// Уникальный идентификатор терминала
        /// </summary>
        [Display(Name = "Уникальный идентификатор терминала")]
        [JsonPropertyName("tid")]
        public string TerminalId { get; set; }

        /// <summary>
        /// Идентификатор устройства, на котором сформирован заказ
        /// <para>Правила заполнения:</para>
        /// <list type="bullet">
        /// <item>для операции "Qr-код Продавца": Уникальный идентификатор устройства в системе "Плати QR"(<see cref="IdQr"/>);</item>
        /// <item>для операции "Qr-код СБП": Уникальный идентификатор терминала(<see cref="TerminalId"/>).</item>
        /// </list>
        /// </summary>
        [Display(Name = "Идентификатор устройства, на котором сформирован заказ")]
        [JsonPropertyName("id_qr")]
        public string IdQr { get; set; }

        /// <summary>
        /// Идентификатор заказа в АС ППРБ.Карты
        /// </summary>
        [Display(Name = "Идентификатор заказа в АС ППРБ.Карты")]
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        [Display(Name = "Статус заказа")]
        [JsonPropertyName("order_state")]
        public OrderState OrderState { get; set; }

        /// <summary>
        /// Операции, привязанные к заказу
        /// </summary>
        [Display(Name = "Операции, привязанные к заказу")]
        [JsonPropertyName("order_operation_params")]
        public OperationParams[] OrderOperationParams { get; set; }

        /// <summary>
        /// Параметры операции СБП
        /// </summary>
        /// <remarks>
        /// Передается только для операции оплаты через СБП
        /// </remarks>
        [Display(Name = "Параметры операции СБП")]
        [JsonPropertyName("sbp_operation_params")]
        public SbpOperationParams SbpOperationParams { get; set; }
    }
}