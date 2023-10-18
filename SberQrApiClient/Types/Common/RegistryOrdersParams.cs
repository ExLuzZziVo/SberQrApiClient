#region

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#endregion

namespace SberQrApiClient.Types.Common
{
    /// <summary>
    /// Блок с перечнем заказов
    /// </summary>
    public class RegistryOrdersParams
    {
        /// <summary>
        /// Перечень заказов
        /// </summary>
        [Display(Name = "Перечень заказов")]
        [JsonPropertyName("orderParam")]
        public RegistryOrderParams[] OrderParams { get; set; }
    }
}