#region

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

#endregion

namespace SberQrApiClient.Types.Common
{
    /// <summary>
    /// Блок с перечнем операций, привязанных к заказу с детализацией по каждой операции
    /// </summary>
    public class RegistryOrderOperationsParams
    {
        /// <summary>
        /// Параметры операции
        /// </summary>
        /// <remarks>
        /// Информация только по успешным операциям
        /// </remarks>
        [Display(Name = "Параметры операции")]
        [JsonProperty("orderOperationParam")]
        public RegistryOrderOperationParams[] OrderOperationParams { get; set; }
    }
}