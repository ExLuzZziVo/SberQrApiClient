#region

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("orderOperationParam")]
        public RegistryOrderOperationParams[] OrderOperationParams { get; set; }
    }
}