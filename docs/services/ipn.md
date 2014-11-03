PayU Teknik Entegrasyon Rehberi'nde IPN sisteminin çalışması:

> Bir sipariş için provizyon ve onay verildiğinde, PayU sunucusu siparişle ilgili tüm bilgileri içeren bir veri yapısını
> sisteminizde daha önceden ayarlanmış olan bir URL’ye gönderir. Veriler HTTP POST aracılığıyla gönderilir. Veriler ayıca
> bilgilerin doğrulanması için bir imza içerecektir. İmza, istenen veri seti üzerinde bir ortak PayU/Satıcı anahtarı ile bir
> HMAC_MD5 işlevi uygulayarak elde edilir (HMAC, RFC 2104’te tanımlanmaktadır).

olarak anlatılmıştır.

PayUSharp kütüphanesi ile gönderilen IPN alanlarının işlenmesi ve yanıt olarak gönderilmesi gereken bilginin doğru şekilde oluşturulması işlemleri kolaylaştırılmıştır.

### Başlangıç {#ipnstart}

Herhangi bir LiveUpdate işlemi gerçekleştirmeden önce yeni bir `PayU.IPN.IPNService` nesnesi yaratılmalıdır. Bu nesneye geçirilmesi zorunlu olan tek parametre `signatureKey` alanıdir ve PayU'dan alınan imza anahtarı değeri geçirilmelidir.

Örnek kullanım şu şekildedir:

```cs
  var service = new PayU.IPN.IPNService('signatureKey');
```

### IPN Alanlarının İşlenmesi

Sipariş bilgilerinde verdiğiniz IPN adresinizin `http://example.com/ipn/default.aspx` olduğunu farzedelim. Sipariş onayı verildiğinde bu adrese IPN bilgileri HTTP POST olarak gönderilecektir. Sayfa kodunuzda, gönderilen bu bilgileri yorumlamak için aşağıdaki örnekte olduğu gibi [Başlangıç](#ipnstart) adımında yarattığımız `IPNService` nesnesinin `ParseRequest` metodu kullanılarak yeni bir `IPNRequest` nesnesi yaratılmalıdır. Bu metod ile `IPNRequest` nesnesi yaratılırken gelen POST alanları doğru bir şekilde işlenerek nesnenin ilgili alanlarına kolay erişim icin eklenecektir.

```.cs
public partial class Default: System.Web.UI.Page {
  public void Page_Load() {
    // Some code here
    var service = new PayU.IPN.IPNService('signatureKey');

    PayU.IPN.IPNRequest ipn = service.ParseRequest(Request);

    Console.WriteLine("Siparis Durumu: {0}", ipn.OrderStatus);
    // Some code here
  }
}
```

### IPN Cevabının Oluşturulması

Eğer IPN isteği başarılı bir şekilde işlendiyse HTTP 200 kodu ile PayU dökumanında belirtilen XML formatında bir cevap dönülmelidir:

> PayU, aşağıdaki formatta (sayfanın herhangi bir yerinde) bir yanıt bekler:
> <epayment>DATE|HASH</epayment>

Bu cevap alanındaki Hash'i hesaplamak ve doğru XML'i oluşturmak için de [Başlangıç](#ipnstart) adımında yarattığımız `IPNService` nesnesinin `GenerateResponseForRequest` metodu kullanılmalıdır. Bunun için örnek kod şu şekildedir:

```.cs
public partial class Default : System.Web.UI.Page
{
  public void Page_Load() {
    var service = new PayU.IPN.IPNService('signatureKey');

    var ipn = service.ParseRequest(Request);

    Console.WriteLine("Siparis Durumu: {0}", ipn.OrderStatus);

    Response.ContentType = "text/xml";
    Response.Write(service.GenerateResponseForRequest(ipn));
    Response.End();
  }
}
```

### IPN Alanları

<<[fields/ipn_fields.md]
