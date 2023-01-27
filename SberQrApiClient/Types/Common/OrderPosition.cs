#region

using System;
using System.ComponentModel.DataAnnotations;
using CoreLib.CORE.Helpers.ObjectHelpers;
using CoreLib.CORE.Helpers.StringHelpers;
using CoreLib.CORE.Resources;
using Newtonsoft.Json;
using SberQrApiClient.Types.Converters;

#endregion

namespace SberQrApiClient.Types.Common
{
    /// <summary>
    /// Товарная позиция
    /// </summary>
    public class OrderPosition
    {
        /// <summary>
        /// Товарная позиция
        /// </summary>
        /// <param name="name">Наименование товарной позиции</param>
        public OrderPosition(string name)
        {
            if (name.IsNullOrEmptyOrWhiteSpace() || name.Length > 256)
            {
                throw new ArgumentException(
                    string.Format(
                        ValidationStrings.ResourceManager.GetString("StringFormatError"),
                        GetType().GetProperty(nameof(Name)).GetPropertyDisplayName()),
                    nameof(name));
            }

            Name = name;
        }

        /// <summary>
        /// Наименование товарной позиции
        /// </summary>
        /// <list type="bullet">
        /// <item>Обязательное поле</item>
        /// <item>Максимальная длина: 256</item>
        /// </list>
        [Display(Name = "Наименование товарной позиции")]
        [JsonProperty("position_name")]
        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(256, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        public string Name { get; }

        /// <summary>
        /// Количество штук товарной позиции
        /// </summary>
        /// <list type="bullet">
        /// <item>Должно лежать в диапазоне: 1-999999</item>
        /// </list>
        [Display(Name = "Количество штук товарной позиции")]
        [JsonProperty("position_count")]
        [Range(1, 999999, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "DigitRangeValuesError")]
        public int? Count { get; set; }

        /// <summary>
        /// Сумма товарной позиции
        /// </summary>
        /// <list type="bullet">
        /// <item>Должно лежать в диапазоне: 0.01-9999999999999.99</item>
        /// </list>
        [Display(Name = "Сумма товарной позиции")]
        [JsonProperty("position_sum")]
        [JsonConverter(typeof(AmountConverter))]
        [Range(0.01, 9999999999999.99, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "DigitRangeValuesError")]
        public decimal? Sum { get; set; }

        /// <summary>
        /// Описание товарной позиции
        /// </summary>
        /// <list type="bullet">
        /// <item>Максимальная длина: 1024</item>
        /// <item>Спецсимволы требуется экранировать</item>
        /// </list>
        [Display(Name = "Описание товарной позиции")]
        [JsonProperty("position_description")]
        [MaxLength(1024, ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "StringMaxLengthError")]
        public string Description { get; set; }
    }
}