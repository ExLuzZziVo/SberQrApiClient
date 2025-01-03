﻿#region

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CoreLib.CORE.Helpers.Converters;
using SberQrApiClient.Types.Common;
using SberQrApiClient.Types.Enums;

#endregion

namespace SberQrApiClient.Types.Operations.OrderRegistry
{
    /// <summary>
    /// Результат запроса реестра операций
    /// </summary>
    public class OrderRegistryResult: OperationResult
    {
        /// <summary>
        /// Уникальный идентификатор запроса
        /// </summary>
        [Display(Name = "Уникальный идентификатор запроса")]
        [JsonPropertyName("rqUid")]
        public override string RequestId { get; set; }

        /// <summary>
        /// Дата/время формирования запроса
        /// </summary>
        [Display(Name = "Дата/время формирования запроса")]
        [JsonPropertyName("rqTm")]
        [CustomDateTimeConverter("yyyy-MM-ddTHH:mm:ssZ")]
        public override DateTime RequestDateTime { get; set; }

        /// <summary>
        /// Код выполнения запроса
        /// </summary>
        [Display(Name = "Код выполнения запроса")]
        [JsonPropertyName("errorCode")]
        public override string ErrorCode { get; set; }

        /// <summary>
        /// Описание ошибки выполнения запроса
        /// </summary>
        [Display(Name = "Описание ошибки выполнения запроса")]
        [JsonPropertyName("errorDescription")]
        public override string ErrorDescription { get; set; }

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
        [JsonPropertyName("idQR")]
        public string IdQr { get; set; }

        /// <summary>
        /// Данные отчета
        /// </summary>
        /// <remarks>
        /// Заполняется в случае, если выбран тип реестра: <see cref="RegistryType.QUANTITY"/>
        /// </remarks>
        [Display(Name = "Данные отчета")]
        [JsonPropertyName("quantityData")]
        public QuantityRegistryData QuantityReportData { get; set; }

        /// <summary>
        /// Данные отчета
        /// </summary>
        /// <remarks>
        /// Заполняется в случае, если выбран тип реестра: <see cref="RegistryType.REGISTRY"/>
        /// </remarks>
        [Display(Name = "Данные отчета")]
        [JsonPropertyName("registryData")]
        public RegistryData RegistryReportData { get; set; }
    }
}