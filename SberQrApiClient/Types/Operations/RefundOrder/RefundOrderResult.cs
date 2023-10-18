#region

using System;
using System.ComponentModel.DataAnnotations;
using CoreLib.CORE.Helpers.Converters;
using System.Text.Json.Serialization;
using SberQrApiClient.Types.Common;
using SberQrApiClient.Types.Converters;
using SberQrApiClient.Types.Enums;

#endregion

namespace SberQrApiClient.Types.Operations.RefundOrder
{
    /// <summary>
    /// Результат отмены/возврата финансовой операции
    /// </summary>
    public class RefundOrderResult : OperationResult
    {
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
        [JsonPropertyName("order_status")]
        public OrderState? OrderState { get; set; }

        /// <summary>
        /// Идентификатор операции в АС ППРБ.Карты
        /// </summary>
        [Display(Name = "Идентификатор операции в АС ППРБ.Карты")]
        [JsonPropertyName("operation_id")]
        public string OperationId { get; set; }

        /// <summary>
        /// Дата/Время операции в АС ППРБ.Карты
        /// </summary>
        [Display(Name = "Дата/Время операции в АС ППРБ.Карты")]
        [JsonPropertyName("operation_date_time")]
        [CustomDateTimeConverter("yyyy-MM-ddTHH:mm:ssZ")]
        public DateTime? OperationDateTime { get; set; }

        /// <summary>
        /// Тип операции
        /// </summary>
        [Display(Name = "Тип операции")]
        [JsonPropertyName("operation_type")]
        public OperationType? Type { get; set; }

        /// <summary>
        /// Сумма операции
        /// </summary>
        [Display(Name = "Сумма операции")]
        [JsonPropertyName("operation_sum")]
        [JsonConverter(typeof(AmountConverter))]
        public decimal? Sum { get; set; }

        /// <summary>
        /// Код валюты операции
        /// </summary>
        /// <remarks>
        /// ISO 4217
        /// </remarks>
        [Display(Name = "Код валюты операции")]
        [JsonPropertyName("operation_currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Код авторизации
        /// </summary>
        [Display(Name = "Код авторизации")]
        [JsonPropertyName("auth_code")]
        public string AuthCode { get; set; }

        /// <summary>
        /// RRN операции
        /// </summary>
        [Display(Name = "RRN операции")]
        [JsonPropertyName("rrn")]
        public string Rrn { get; set; }

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
        /// Параметры операции СБП
        /// </summary>
        /// <remarks>
        /// Передается только для операции оплаты через СБП
        /// </remarks>
        [Display(Name = "Параметры операции СБП")]
        [JsonPropertyName("sbp_operation_params")]
        public SbpRefundOperationParams SbpOperationParams { get; set; }
    }
}