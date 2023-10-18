# SberQrApiClient
Библиотека для работы с API SberPay QR/Плати QR банка Сбербанк.


Создана по официальной документации со следующих источников: [1](https://api.developer.sber.ru/product/PlatiQR/doc/v1/QR_API_doc1).


**Использовать только на свой страх и риск.**


Пример использования:


1. Для начала необходимо создать класс с настройками подключения к API банка, унаследовав его от интерфейса **SberQrApiClient.Types.Interfaces.ISberQrApiSettings**, например:

```csharp
public class SberQrApiSettings: ISberQrApiSettings
{
    public string ApiHost { get; set; }

    public string ClientId { get; set; }

    public string ClientSecret { get; set; }

    public string MerchantId { get; set; }

    public string IdQr { get; set; }

    // Пример получения сертификата клиента из файла. Можно не реализовывать, если для запуска операций используется свой экземпляр HttpClient.
    public Task<X509Certificate2> GetCertificateAsync()
    {
        var pathToCertificate = $"{Directory.GetCurrentDirectory()}/cert.p12";
	
        if (!File.Exists(pathToCertificate))
        {
            throw new FileNotFoundException("Certificate not found", _pathToCertificate);
        }

        // Без флага X509KeyStorageFlags.MachineKeySet может не соединяться на боевом сервере
        var certificate = new X509Certificate2(pathToCertificate, "p@$Sw0rD", X509KeyStorageFlags.MachineKeySet);

        return Task.FromResult(certificate);
    }
}
```

2. После этого, задав значения свойств этих настроек(**ApiHost**, **ClientId**, **ClientSecret** - обязательны. **MerchantId**, **IdQr** - необязательны. Это значения по умолчанию. Метод **GetCertificateAsync()** необходимо реализовать, если не планируете использовать свой экземпляр HttpClient), можно запустить нужную операцию следующим образом:

```csharp
var apiSettings = new SberQrApiSettings();
// Если используете HttpClient через внедрение зависимостей
var result = await new <Операция>.ExecuteAsync(_httpClient, apiSettings);
// Если нет
var result = await new <Операция>.ExecuteAsync(apiSettings);
```

Все доступные операции находятся в пространстве имен **SberQrApiSettings.Types.Operations**


Зависимости CoreLib вы можете найти [тут](https://github.com/ExLuzZziVo/CoreLib).