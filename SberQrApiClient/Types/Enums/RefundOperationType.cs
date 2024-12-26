#region

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#endregion

namespace SberQrApiClient.Types.Enums
{
    /// <summary>
    /// Тип операции возврата/отмены
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RefundOperationType: byte
    {
        /// <summary>
        /// Возврат
        /// </summary>
        [Display(Name = "Возврат")] REFUND,

        /// <summary>
        /// Отмена
        /// </summary>
        [Display(Name = "Отмена")] REVERSE
    }
}