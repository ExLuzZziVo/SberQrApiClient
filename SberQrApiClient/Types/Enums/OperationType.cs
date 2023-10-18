#region

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#endregion

namespace SberQrApiClient.Types.Enums
{
    /// <summary>
    /// Тип операции
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OperationType : byte
    {
        /// <summary>
        /// Оплата
        /// </summary>
        [Display(Name = "Оплата")] PAY,

        /// <summary>
        /// Отмена
        /// </summary>
        [Display(Name = "Отмена")] REVERSE,

        /// <summary>
        /// Возврат
        /// </summary>
        [Display(Name = "Возврат")] REFUND
    }
}