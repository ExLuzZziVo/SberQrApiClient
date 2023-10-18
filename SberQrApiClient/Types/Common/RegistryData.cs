#region

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#endregion

namespace SberQrApiClient.Types.Common
{
    /// <summary>
    /// Данные реестра детального отчета по заказам
    /// </summary>
    public class RegistryData
    {
        /// <summary>
        /// Блок с перечнем заказов
        /// </summary>
        [Display(Name = "Блок с перечнем заказов")]
        [JsonPropertyName("orderParams")]
        public RegistryOrdersParams OrdersParams { get; set; }
    }
}