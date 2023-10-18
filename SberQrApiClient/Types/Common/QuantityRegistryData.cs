#region

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SberQrApiClient.Types.Converters;

#endregion

namespace SberQrApiClient.Types.Common
{
    /// <summary>
    /// Данные реестра агрегации по количеству операций
    /// </summary>
    public class QuantityRegistryData
    {
        /// <summary>
        /// Общее количество успешных операций
        /// </summary>
        [Display(Name = "Общее количество успешных операций")]
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        /// <summary>
        /// Общая сумма покупок
        /// </summary>
        [Display(Name = "Общая сумма покупок")]
        [JsonPropertyName("totalPaymentAmount")]
        [JsonConverter(typeof(AmountConverter))]
        public decimal TotalPaymentAmount { get; set; }

        /// <summary>
        /// Общая сумма возвратов и отмен
        /// </summary>
        [Display(Name = "Общая сумма возвратов и отмен")]
        [JsonPropertyName("totalRefundAmount")]
        [JsonConverter(typeof(AmountConverter))]
        public decimal TotalRefundAmount { get; set; }

        /// <summary>
        /// Сумма покупок за вычетом возвратов и отмен
        /// </summary>
        [Display(Name = "Сумма покупок за вычетом возвратов и отмен")]
        [JsonPropertyName("totalAmount")]
        [JsonConverter(typeof(AmountConverter))]
        public decimal TotalAmount { get; set; }
    }
}