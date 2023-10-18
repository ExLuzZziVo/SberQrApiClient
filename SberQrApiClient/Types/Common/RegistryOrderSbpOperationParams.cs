#region

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("sbpOperationId")]
        public string Id { get; set; }

        /// <summary>
        /// Замаскированный идентификатор плательщика
        /// </summary>
        [Display(Name = "Замаскированный идентификатор плательщика")]
        [JsonPropertyName("sbpPayerId")]
        public string PayerId { get; set; }
    }
}