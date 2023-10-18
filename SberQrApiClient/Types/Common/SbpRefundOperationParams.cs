#region

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#endregion

namespace SberQrApiClient.Types.Common
{
    /// <summary>
    /// Параметры операции СБП
    /// </summary>
    public class SbpRefundOperationParams
    {
        /// <summary>
        /// Идентификатор операции в СБП
        /// </summary>
        [Display(Name = "Идентификатор операции в СБП")]
        [JsonPropertyName("sbp_cancel_operation_id")]
        public string Id { get; set; }

        /// <summary>
        /// Наименование юр. лица продавца
        /// </summary>
        [Display(Name = "Наименование юр. лица продавца")]
        [JsonPropertyName("sbp_merchant_name")]
        public string MerchantName { get; set; }
    }
}