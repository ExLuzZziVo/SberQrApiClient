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
    /// Данные операции
    /// </summary>
    public class OperationParams
    {
        /// <summary>
        /// Идентификатор операции в АС ППРБ.Карты
        /// </summary>
        [Display(Name = "Идентификатор операции в АС ППРБ.Карты")]
        [JsonProperty("operation_id")]
        public string Id { get; set; }

        /// <summary>
        /// Дата/время совершения операции
        /// </summary>
        [Display(Name = "Дата/время совершения операции")]
        [JsonProperty("operation_date_time")]
        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-ddTHH:mm:ssZ")]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// RRN операции
        /// </summary>
        [Display(Name = "RRN операции")]
        [JsonProperty("rrn")]
        public string Rrn { get; set; }

        /// <summary>
        /// Тип операции
        /// </summary>
        [Display(Name = "Тип операции")]
        [JsonProperty("operation_type")]
        public OperationType Type { get; set; }

        /// <summary>
        /// Сумма операции
        /// </summary>
        [Display(Name = "Сумма операции")]
        [JsonProperty("operation_sum")]
        [JsonConverter(typeof(AmountConverter))]
        public decimal Sum { get; set; }

        /// <summary>
        /// Код валюты операции
        /// </summary>
        /// <remarks>
        /// ISO 4217
        /// </remarks>
        [Display(Name = "Код валюты операции")]
        [JsonProperty("operation_currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Код авторизации
        /// </summary>
        [Display(Name = "Код авторизации")]
        [JsonProperty("auth_code")]
        public string AuthCode { get; set; }

        /// <summary>
        /// Код выполнения операции
        /// </summary>
        [Display(Name = "Код выполнения операции")]
        [JsonProperty("response_code")]
        public string ResponseCode { get; set; }

        /// <summary>
        /// Описание кода выполнения операции
        /// </summary>
        [Display(Name = "Описание кода выполнения операции")]
        [JsonProperty("response_desc")]
        public string ResponseDescription { get; set; }

        /// <summary>
        /// Замаскированное ФИО плательщика
        /// </summary>
        [Display(Name = "Замаскированное ФИО плательщика")]
        [JsonProperty("client_name")]
        public string ClientName { get; set; }
    }
}