#region

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

#endregion

namespace SberQrApiClient.Types.Enums
{
    /// <summary>
    /// Тип операции
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
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