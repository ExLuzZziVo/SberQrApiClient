#region

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#endregion

namespace SberQrApiClient.Types.Operations.ClientOrder
{
    /// <summary>
    /// Результат проведения платежа по Qr-коду покупателя
    /// </summary>
    public class ClientOrderResult: OperationResult
    {
        /// <summary>
        /// Результат выполнения операции
        /// </summary>
        [Display(Name = "Результат выполнения операции")]
        [JsonPropertyName("PayRusClientQRRs")]
        public ClientOrderResultPayload OperationResult { get; set; }
    }
}