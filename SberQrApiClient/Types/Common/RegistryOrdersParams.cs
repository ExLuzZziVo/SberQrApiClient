#region

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

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
        [JsonProperty("orderParam")]
        public RegistryOrderParams[] OrderParams { get; set; }
    }
}