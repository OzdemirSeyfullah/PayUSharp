PayU Teknik Entegrasyon Rehberi'nde IPN sisteminin çalışması:

> Bir sipariş için provizyon ve onay verildiğinde, PayU sunucusu siparişle ilgili tüm bilgileri içeren bir veri yapısını
> sisteminizde daha önceden ayarlanmış olan bir URL’ye gönderir. Veriler HTTP POST aracılığıyla gönderilir. Veriler ayıca
> bilgilerin doğrulanması için bir imza içerecektir. İmza, istenen veri seti üzerinde bir ortak PayU/Satıcı anahtarı ile bir
> HMAC_MD5 işlevi uygulayarak elde edilir (HMAC, RFC 2104’te tanımlanmaktadır).

olarak anlatılmıştır.

PayUSharp kütüphanesi ile gönderilen IPN alanlarının işlenmesi ve yanıt olarak gönderilmesi gereken bilginin doğru şekilde oluşturulması işlemleri kolaylaştırılmıştır.

### Ayarlar

Herhangi bir PayU işlemi gerçekleştirilmeden önce (tercihen 1 kere uygulama başlangıcında) PayUSharp kütüphanesinin ayarlarının doğru bir şekilde yapılması gerekmektedir. Bunun için `PayU.Configuration` sınıfı kullanılmaktadır.

IPN için zorunlu alan `SignatureKey` alanıdır. Örnek kullanım şu şekildedir:

```{.cs language=csh}
  PayU.Configuration.Instance.SetSignatureKey('signaturekey');
```

### IPN Alanlarının İşlenmesi

Sipariş bilgilerinde verdiğiniz IPN adresinizin `http://example.com/ipn/default.aspx` olduğunu farzedelim. Sipariş onayı verildiğinde bu adrese IPN bilgileri HTTP POST olarak gönderilecektir. Sayfa kodunuzda, gönderilen bu bilgileri yorumlamak için aşağıdaki örnekte olduğu gibi `IPNRequest.FromHttpRequest` metodu kullanılarak yeni bir `IPNRequest` nesnesi yaratılmalıdır. Bu metod ile `IPNRequest` nesnesi yaratılırken gelen POST alanları doğru bir şekilde işlenerek nesnenin ilgili alanlarına kolay erişim icin eklenecektir.

```{.cs language=csh}
public partial class Default: System.Web.UI.Page {
  public void Page_Load() {
    // Some code here
    IPNRequest ipn = IPNRequest.FromHttpRequest(Request);

    Console.WriteLine("Siparis Durumu: {0}", ipn.OrderStatus);
    // Some code here
  }
}
```

### IPN Cevabının Oluşturulması

Eğer IPN isteği başarılı bir şekilde işlendiyse HTTP 200 kodu ile PayU dökumanında belirtilen XML formatında bir cevap dönülmelidir:

> PayU, aşağıdaki formatta (sayfanın herhangi bir yerinde) bir yanıt bekler:
> <epayment>DATE|HASH</epayment>

Bu cevap alanındaki Hash'i hesaplamak ve doğru XML'i oluşturmak için de `IPNRequest` nesnesinin `GenerateResponse` metodu kullanılmalıdır. Bunun için örnek kod şu şekildedir:

```{.cs language=csh}
public partial class Default : System.Web.UI.Page
{
  public void Page_Load() {
    var ipn = IPNRequest.FromHttpRequest(Request);

    Console.WriteLine("Siparis Durumu: {0}", ipn.OrderStatus);

    Response.ContentType = "text/xml";
    Response.Write(ipn.GenerateResponse());
    Response.End();
  }
}
```

### IPN Alanları

<<[fields/ipn_fields.md]
