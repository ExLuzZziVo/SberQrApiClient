#region

using System;
using System.ComponentModel.DataAnnotations;
using CoreLib.CORE.Helpers.Converters;
using System.Text.Json.Serialization;

#endregion

namespace SberQrApiClient.Types.Operations
{
    public abstract class OperationResult
    {
        /// <summary>
        /// Уникальный идентификатор запроса
        /// </summary>
        [Display(Name = "Уникальный идентификатор запроса")]
        [JsonPropertyName("rq_uid")]
        public virtual string RequestId { get; set; }

        /// <summary>
        /// Дата/время формирования запроса
        /// </summary>
        [Display(Name = "Дата/время формирования запроса")]
        [JsonPropertyName("rq_tm")]
        [CustomDateTimeConverter("yyyy-MM-ddTHH:mm:ssZ")]
        public virtual DateTime RequestDateTime { get; set; }

        /// <summary>
        /// Код выполнения запроса
        /// </summary>
        [Display(Name = "Код выполнения запроса")]
        [JsonPropertyName("error_code")]
        public virtual string ErrorCode { get; set; }

        /// <summary>
        /// Описание ошибки выполнения запроса
        /// </summary>
        [Display(Name = "Описание ошибки выполнения запроса")]
        [JsonPropertyName("error_description")]
        public virtual string ErrorDescription { get; set; }
    }
}