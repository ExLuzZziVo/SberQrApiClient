#region

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

#endregion

namespace SberQrApiClient.Types.Common
{
    /// <summary>
    /// Параметры операции СБП
    /// </summary>
    public class SbpOperationParams
    {
        /// <summary>
        /// Идентификатор операции в СБП
        /// </summary>
        [Display(Name = "Идентификатор операции в СБП")]
        [JsonProperty("sbp_operation_id")]
        public string Id { get; set; }

        /// <summary>
        /// Замаскированный идентификатор плательщика
        /// </summary>
        [Display(Name = "Замаскированный идентификатор плательщика")]
        [JsonProperty("sbp_masked_payer_id")]
        public string PayerId { get; set; }
    }
}