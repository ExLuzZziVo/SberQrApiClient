#region

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

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
        [JsonProperty("orderParams")]
        public RegistryOrdersParams OrdersParams { get; set; }
    }
}