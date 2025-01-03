﻿#region

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#endregion

namespace SberQrApiClient.Types.Enums
{
    /// <summary>
    /// Тип реестра
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RegistryType: byte
    {
        /// <summary>
        /// Агрегация по количеству операций
        /// </summary>
        [Display(Name = "Агрегация по количеству операций")]
        QUANTITY,

        /// <summary>
        /// Детальный отчет по заказам
        /// </summary>
        [Display(Name = "Детальный отчет по заказам")]
        REGISTRY
    }
}