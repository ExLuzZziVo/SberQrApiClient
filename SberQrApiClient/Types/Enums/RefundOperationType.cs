#region

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

#endregion

namespace SberQrApiClient.Types.Enums
{
    /// <summary>
    /// Тип операции возврата/отмены
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RefundOperationType : byte
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