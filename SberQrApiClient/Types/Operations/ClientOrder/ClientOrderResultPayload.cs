#region

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SberQrApiClient.Types.Common;
using SberQrApiClient.Types.Enums;

#endregion

namespace SberQrApiClient.Types.Operations.ClientOrder
{
    /// <summary>
    /// Полезная нагрузка результата проведения платежа по Qr-коду покупателя
    /// </summary>
    public class ClientOrderResultPayload : OperationResult
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
        /// Номер заказа в системе продавца
        /// </summary>
        [Display(Name = "Номер заказа в системе продавца")]
        [JsonPropertyName("partner_order_number")]
        public string OrderNumber { get; set; }

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
        /// Параметры заказа
        /// </summary>
        [Display(Name = "Параметры заказа")]
        [JsonPropertyName("order_operation_params")]
        public OperationParams OrderOperationParams { get; set; }
    }
}