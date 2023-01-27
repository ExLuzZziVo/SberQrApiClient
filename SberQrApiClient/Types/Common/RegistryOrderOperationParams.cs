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
    /// Параметры операции
    /// </summary>
    public class RegistryOrderOperationParams
    {
        /// <summary>
        /// Идентификатор операции в АС ППРБ.Карты
        /// </summary>
        [Display(Name = "Идентификатор операции в АС ППРБ.Карты")]
        [JsonProperty("operationId")]
        public string Id { get; set; }

        /// <summary>
        /// Дата/время совершения операции
        /// </summary>
        [Display(Name = "Дата/время совершения операции")]
        [JsonProperty("operationDateTime")]
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
        [JsonProperty("operationType")]
        public OperationType Type { get; set; }

        /// <summary>
        /// Сумма операции
        /// </summary>
        [Display(Name = "Сумма операции")]
        [JsonProperty("operationSum")]
        [JsonConverter(typeof(AmountConverter))]
        public decimal Sum { get; set; }

        /// <summary>
        /// Код валюты операции
        /// </summary>
        /// <remarks>
        /// ISO 4217
        /// </remarks>
        [Display(Name = "Код валюты операции")]
        [JsonProperty("operationCurrency")]
        public string Currency { get; set; }

        /// <summary>
        /// Код авторизации
        /// </summary>
        [Display(Name = "Код авторизации")]
        [JsonProperty("authCode")]
        public string AuthCode { get; set; }

        /// <summary>
        /// Код выполнения операции
        /// </summary>
        [Display(Name = "Код выполнения операции")]
        [JsonProperty("responseCode")]
        public string ResponseCode { get; set; }

        /// <summary>
        /// Описание кода выполнения операции
        /// </summary>
        [Display(Name = "Описание кода выполнения операции")]
        [JsonProperty("responseDesc")]
        public string ResponseDescription { get; set; }

        /// <summary>
        /// Параметры операции СБП
        /// </summary>
        /// <remarks>
        /// Передается только для операции оплаты через СБП
        /// </remarks>
        [Display(Name = "Параметры операции СБП")]
        [JsonProperty("sbpOperationParams")]
        public RegistryOrderSbpOperationParams SbpOperationParams { get; set; }
    }
}