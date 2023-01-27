#region

using System;
using System.ComponentModel.DataAnnotations;
using CoreLib.CORE.Helpers.Converters;
using Newtonsoft.Json;
using SberQrApiClient.Types.Enums;

#endregion

namespace SberQrApiClient.Types.Common
{
    /// <summary>
    /// Уведомление об оплате заказа
    /// </summary>
    public class PaymentOperationNotificationData : RegistryOrderOperationParams
    {
        /// <summary>
        /// Уникальный идентификатор запроса
        /// </summary>
        [Display(Name = "Уникальный идентификатор запроса")]
        [JsonProperty("rqUid")]
        public string RequestId { get; set; }

        /// <summary>
        /// Дата/время формирования запроса
        /// </summary>
        [Display(Name = "Дата/время формирования запроса")]
        [JsonProperty("rqTm")]
        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-ddTHH:mm:ssZ")]
        public DateTime RequestDateTime { get; set; }

        /// <summary>
        /// Идентификатор продавца
        /// </summary>
        [Display(Name = "Идентификатор продавца")]
        [JsonProperty("memberId")]
        public string MerchantId { get; set; }

        /// <summary>
        /// Идентификатор устройства, на котором сформирован заказ
        /// <para>Правила заполнения:</para>
        /// <list type="bullet">
        /// <item>для операции "Qr-код Продавца": Уникальный идентификатор устройства в системе "Плати QR"(<see cref="IdQr"/>);</item>
        /// <item>для операции "Qr-код СБП": Уникальный идентификатор терминала(<see cref="TerminalId"/>).</item>
        /// </list>
        /// </summary>
        [Display(Name = "Идентификатор устройства, на котором сформирован заказ")]
        [JsonProperty("idQR")]
        public string IdQr { get; set; }

        /// <summary>
        /// Уникальный идентификатор терминала
        /// </summary>
        [Display(Name = "Уникальный идентификатор терминала")]
        [JsonProperty("tid")]
        public string TerminalId { get; set; }

        /// <summary>
        /// Идентификатор заказа в АС ППРБ.Карты
        /// </summary>
        [Display(Name = "Идентификатор заказа в АС ППРБ.Карты")]
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// Номер заказа в системе продавца
        /// </summary>
        [Display(Name = "Номер заказа в системе продавца")]
        [JsonProperty("partnerOrderNumber")]
        public string OrderNumber { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        [Display(Name = "Статус заказа")]
        [JsonProperty("orderState")]
        public OrderState OrderState { get; set; }

        /// <summary>
        /// Замаскированное ФИО плательщика
        /// </summary>
        [Display(Name = "Замаскированное ФИО плательщика")]
        [JsonProperty("clientName")]
        public string ClientName { get; set; }
    }
}