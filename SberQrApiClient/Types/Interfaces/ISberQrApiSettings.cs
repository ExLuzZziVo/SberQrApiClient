#region

using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

#endregion

namespace SberQrApiClient.Types.Interfaces
{
    public interface ISberQrApiSettings
    {
        /// <summary>
        /// Адрес для отправки запросов
        /// </summary>
        string ApiHost { get; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        string ClientId { get; }

        /// <summary>
        /// Секрет пользователя
        /// </summary>
        string ClientSecret { get; }

        /// <summary>
        /// Идентификатор продавца
        /// </summary>
        string MerchantId { get; }
        
        /// <summary>
        /// Идентификатор устройства, на котором сформирован заказ
        /// </summary>
        string IdQr { get; }
        
        /// <summary>
        /// Асинхронное получение сертификата пользователя для отправки запросов
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию получения сертификата пользователя для отправки запросов</returns>
        Task<X509Certificate2> GetCertificateAsync();
    }
}