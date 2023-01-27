#region

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

#endregion

namespace SberQrApiClient.Types.Common
{
    /// <summary>
    /// Параметры операции СБП
    /// </summary>
    public class RegistryOrderSbpOperationParams
    {
        /// <summary>
        /// Идентификатор операции в СБП
        /// </summary>
        [Display(Name = "Идентификатор операции в СБП")]
        [JsonProperty("sbpOperationId")]
        public string Id { get; set; }

        /// <summary>
        /// Замаскированный идентификатор плательщика
        /// </summary>
        [Display(Name = "Замаскированный идентификатор плательщика")]
        [JsonProperty("sbpPayerId")]
        public string PayerId { get; set; }
    }
}