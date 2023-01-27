# SberQrApiClient
Библиотека для работы с API SberPay QR/Плати QR банка Сбербанк.
<br/>
<br/>
Создана по официальной документации со следующих источников: <a href="https://api.developer.sber.ru/product/PlatiQR/doc/v1/QR_API_doc1" rel="nofollow">1</a>.
<br/>
<br/>
<b>Использовать только на свой страх и риск.</b>
<br/>
<br/>
Пример использования:
<br/>

1. Для начала необходимо создать класс с настройками подключения к API банка, унаследовав его от интерфейса <b>SberQrApiClient.Types.Interfaces.ISberQrApiSettings</b>, например:

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

2. После этого, задав значения свойств этих настроек(<b>ApiHost</b>, <b>ClientId</b>, <b>ClientSecret</b> - обязательны. <b>MerchantId</b>, <b>IdQr</b> - необязательны. Это значения по умолчанию. Метод <b>GetCertificateAsync()</b> необходимо реализовать, если не планируете использовать свой экземпляр HttpClient), можно запустить нужную операцию следующим образом:

```csharp
var apiSettings = new SberQrApiSettings();
// Если используете HttpClient через внедрение зависимостей
var result = await new <Операция>.ExecuteAsync(_httpClient, apiSettings);
// Если нет
var result = await new <Операция>.ExecuteAsync(apiSettings);
```

Все доступные операции находятся в пространстве имен <b>SberQrApiSettings.Types.Operations</b>
<br/>
<br/>
Зависимости CoreLib вы можете найти <a href="https://github.com/ExLuzZziVo/CoreLib" rel="nofollow">тут</a>.