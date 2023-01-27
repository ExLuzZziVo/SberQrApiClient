#region

using System;
using System.ComponentModel.DataAnnotations;
using CoreLib.CORE.Helpers.Converters;
using Newtonsoft.Json;

#endregion

namespace SberQrApiClient.Types.Operations
{
    public abstract class OperationResult
    {
        /// <summary>
        /// Уникальный идентификатор запроса
        /// </summary>
        [Display(Name = "Уникальный идентификатор запроса")]
        [JsonProperty("rq_uid")]
        public virtual string RequestId { get; set; }

        /// <summary>
        /// Дата/время формирования запроса
        /// </summary>
        [Display(Name = "Дата/время формирования запроса")]
        [JsonProperty("rq_tm")]
        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-ddTHH:mm:ssZ")]
        public virtual DateTime RequestDateTime { get; set; }

        /// <summary>
        /// Код выполнения запроса
        /// </summary>
        [Display(Name = "Код выполнения запроса")]
        [JsonProperty("error_code")]
        public virtual string ErrorCode { get; set; }

        /// <summary>
        /// Описание ошибки выполнения запроса
        /// </summary>
        [Display(Name = "Описание ошибки выполнения запроса")]
        [JsonProperty("error_description")]
        public virtual string ErrorDescription { get; set; }
    }
}