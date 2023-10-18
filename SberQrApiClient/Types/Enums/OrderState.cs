#region

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#endregion

namespace SberQrApiClient.Types.Enums
{
    /// <summary>
    /// Статус заказа
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderState : byte
    {
        [Display(Name =
            "В привязке к заказу есть успешная операция покупки на сумму заказа/успешная операция покупки и неуспешная(-ые) операции отмены")]
        PAID,

        /// <summary>
        /// Создан или в привязке к заказу есть неуспешные авторизации
        /// </summary>
        [Display(Name = "Создан или в привязке к заказу есть неуспешные авторизации")]
        CREATED,

        /// <summary>
        /// В привязке к заказу есть успешные операции Полной отмены/Частичной отмены
        /// </summary>
        /// <remarks>
        /// Может быть выполнено однократно в течении одних суток после оплаты заказа. После вызова REVERSE нельзя вызвать REFUND/REVERSE. Рекомендуется для сервиса OrderCancelQrRq[REVERSE] использовать полную отмену.
        /// </remarks>
        [Display(Name = "В привязке к заказу есть успешные операции Полной отмены/Частичной отмены")]
        REVERSED,

        /// <summary>
        /// В привязке к заказу есть успешные операции Полного возврата/Частичного возврата
        /// </summary>
        [Display(Name = "В привязке к заказу есть успешные операции Полного возврата/Частичного возврата")]
        REFUNDED,

        /// <summary>
        /// Отменен до проведения фин. операции
        /// </summary>
        [Display(Name = "Отменен до проведения фин. операции")]
        REVOKED,

        /// <summary>
        /// Заказ отклонен - не прошла оплата по SberPay
        /// </summary>
        [Display(Name = "Заказ отклонен - не прошла оплата по SberPay")]
        DECLINED,

        /// <summary>
        /// Истек срок жизни заказа
        /// </summary>
        [Display(Name = "Истек срок жизни заказа")]
        EXPIRED,

        /// <summary>
        /// После 1-й стадии двухстадийной оплаты
        /// </summary>
        [Display(Name = "После 1-й стадии двухстадийной оплаты")]
        AUTHORIZED,

        /// <summary>
        /// После 2-й стадии двухстадийной оплаты
        /// </summary>
        [Display(Name = "После 2-й стадии двухстадийной оплаты")]
        CONFIRMED,

        /// <summary>
        /// Ожидает подтверждения платежа от СБП
        /// </summary>
        [Display(Name = "Ожидает подтверждения платежа от СБП")]
        ON_PAYMENT
    }
}