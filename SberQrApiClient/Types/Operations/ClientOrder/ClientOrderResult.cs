#region

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

#endregion

namespace SberQrApiClient.Types.Operations.ClientOrder
{
    /// <summary>
    /// Результат проведения платежа по Qr-коду покупателя
    /// </summary>
    public class ClientOrderResult : OperationResult
    {
        /// <summary>
        /// Результат выполнения операции
        /// </summary>
        [Display(Name = "Результат выполнения операции")]
        [JsonProperty("PayRusClientQRRs")]
        public ClientOrderResultPayload OperationResult { get; set; }
    }
}