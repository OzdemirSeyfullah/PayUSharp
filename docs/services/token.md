Token servisi işyerlerinin bir sipariş sırasında müşterilerden aldıkları ödeme bilgilerini PayU aracılığı ile saklayabildikleri ve aynı müşterinin sonraki siparişlerinde kaydedilmiş ödeme bilgilerini kullanarak işlem yapabildikleri bir hizmettir. PayU kaydedilmesi istenen ödeme bilgilerini işyeri adına belirli bir süre PCI-DSS güvenlik kurallarına uygun bir şekilde saklar ve bu bilgilere karşılık bir `token` bilgisi üretir. Üretilen `token` kendi başına bir anlam taşımayan ama işyeri tarafından güvenli bir şekilde iletildiğinde ödeme bilgisinin kullanılmasını sağlayan bir karakter dizisidir.

Token işlemleri **Tek Tıkla Ödeme** veya **Tekrarlayan Ödeme** gibi müşterilerden tekrar kart bilgisi girmesinin istenilmeyeceği akışlarda kullanılmak üzere yaratılmıştır.

### Başlangıç {#tokenstart}

PayUSharp kütüphanesi ile herhangi bir Token işlemi gerçekleştirmeden önce yeni bir `PayU.Token.TokenService` nesnesi yaratılmalıdır. Bu nesneye geçirilmesi zorunlu olan parametreler `merchant` ve `signatureKey` alanlarıdır ve bu alanlara PayU'dan alınan işyeri ismi ve imza anahtarı değerleri geçirilmelidir.

Örnek kullanım şu şekildedir:

```cs
  var service = new PayU.Token.TokenService('merchant', 'signatureKey');
```

Ayrıca servisin testler sırasında kullanılabilecek `endpointUrl` ve `ignoreSSLCertificate` parametreleri de bulunmaktadır. Bu parametrelerin varsayılan değerlerini PayU tarafından tavsiye edilmedikçe değiştirmemeniz önerilir.

Her token işleminde ödeme bilgisi yerine kullanmak için son kullanıcının PayU tarafında saklanan kart bilgisini temsil eden bir `token` alanı gereklidir. Bu `token` bilgisi [LiveUpdate](#lu) veya [Automatic LiveUpdate](#alu) servislerinde sipariş bilgilerinde `TokenEnable` ve `TokenType` alanlarının doldurularak gönderilmesi ve akabinde gelecek olan [IPN](#ipn) bilgisinde `CreditCardToken` alanının okunması ile oluşturulur:

```cs
// LiveUpdate isteminde
var order = new PayU.LiveUpdate.OrderDetails();
order.TokenEnable = true;
order.TokenType = PayU.Core.Base.TokenType.PAY_BY_CLICK; // veya PAY_ON_TIME


// Daha sonra gelen IPN isteginde
var request = service.ParseRequest(Request);
var token = request.CreditCardToken;
```

### Yeni Satış Oluşturulması

Yeni bir satış yapmak için, [Başlangıç](#tokenstart) adımında yaratılmış olan `TokenService` nesnesinin `NewSale` metodu bu müşteri için alınmış olan `token`, satış için işyeri tarafında tekil belirteç olarak kullanılacak olan `orderRef`, satış bedelini temsil eden `amount` ve opsiyonel olarak satış para birimini temsilen `currency` bilgileri ile çağırılmalıdır. `currecy` alanı verilmezse para biriminin `TRY` (Türk Lirası) olduğu varsayılacaktır. Bu metod `TokenResponse` tipinden bir sonuç döndürecektir. Bu sonuç bilgisinin [alanları](#tokenfields) okunarak isteğin başarılı olarak gerçekleşip gerçekleşmediği ve varsa hata mesajları öğrenilebilir.

```cs
var service = new PayU.Token.TokenService('merchant', 'signatureKey');

var response = service.NewSale('token', 'Order-1234acf34', 12.50M);

if (response.Code == 0) {
    // Sonuc basarili
} else {
    // Sonuc basarisiz
    var message = response.Message;
}
```

### Token Bilgilerinin Alınması

Elinizdeki bir `token`'a ait sipariş geçmişi, `token`'ın geçerlilik durumu ve son kullanma tarihi gibi bilgilere ulaşmak için [Başlangıç](#tokenstart) adımında yaratılmış olan `TokenService` nesnesinin `GetInfo` metodu çağırılmalıdır. Bu metod sadece `token` bilgisini istemekte ve `TokenResponse` tipinden bir sonuç döndürmektedir. Sonuç bilgisinin [alanları](#tokenfields) okunarak isteğin başarılı olarak gerçekleşip gerçekleşmediği, varsa hata mesajları ve `token`'a ait bilgiler öğrenilebilir.

```cs
var service = new PayU.Token.TokenService('merchant', 'signatureKey');

var response = service.GetInfo('token');

if (response.Code == 0) {
    // Sonuc basarili
    var status = response.TokenStatus;
    foreach (var entry in response.History)
    {
      var index = entry.Key;
      var date = entry.Value.Date;
      var refNumber = entry.Value.ReferenceNumber;
      var amount = entry.Value.Amount;
      var currency = entry.Value.Currency;
      // Yukaridaki bilgiler kullanilarak token gecmisi islenebilir
    }

} else {
    // Sonuc basarisiz
    var message = response.Message;
}
```

### Token İptali

Elinizdeki bir `token`'ı müşteri talebi üzerine veya artık kullanılmadığı için iptal etmek isteyebilirsiniz. Bu durumda [Başlangıç](#tokenstart) adımında yaratılmış olan `TokenService` nesnesinin `Cancel` metodu çağırılmalıdır. Bu metoda `token` bilgisi ve opsiyonel olarak iptal sebebini belirten `reason` bilgisi geçirilmelidir. Bu metod `TokenResponse` tipinden bir sonuç döndürmektedir ve sonuç bilgisinin [alanları](#tokenfields) okunarak isteğin başarılı olarak gerçekleşip gerçekleşmediği ve varsa hata mesajları öğrenilebilir.

```cs
var service = new PayU.Token.TokenService('merchant', 'signatureKey');

var response = service.Cancel('token', 'musteri iptali');

if (response.Code == 0) {
    // Iptal basarili
} else {
    // Iptal basarisiz
    var message = response.Message;
}
```

### Token Alanları {#tokenfields}

<<[fields/token_fields.md]
