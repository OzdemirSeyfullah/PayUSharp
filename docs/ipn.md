## PayUSharp ile IPN Request isleme

PayU Teknik Entegrasyon Rehberi'nde anlatildigi uzere IPN sisteminin calismasi:

> Bir sipariş için provizyon ve onay verildiğinde, PayU sunucusu siparişle ilgili tüm bilgileri içeren bir veri yapısını 
> sisteminizde daha önceden ayarlanmış olan bir URL’ye gönderir. Veriler HTTP POST aracılığıyla gönderilir. Veriler ayıca 
> bilgilerin doğrulanması için bir imza içerecektir. İmza, istenen veri seti üzerinde bir ortak PayU/Satıcı anahtarı ile bir 
> HMAC_MD5 işlevi uygulayarak elde edilir (HMAC, RFC 2104’te tanımlanmaktadır).

olarak anlatilmistir.

PayUSharp kutuphanesi ile gonderilen IPN alanlarinin islenmesi ve yanit olarak gonderilmesi gereken bilginin dogru sekilde olusturulmasi islemleri kolaylastirilmistir.

### Ayarlar

Hehangi bir PayU islemi gerceklestirilmeden once (tercihen 1 kere uygulama baslangicinda) PayUSharp kutuphanesinin ayarlarinin dogru bir sekilde yapilmasi gerekmektedir. Bunun icin `PayU.Configuration` sinifi kullanilacaktir. IPN icin en onemli alan `SignatureKey` alanidir. Ornek kullanim su sekildedir:

```csharp
  PayU.Configuration.Instance.SetSignatureKey('signaturekey');
```

### IPN alanlarinin islenmesi

Siparis bilgilerinde verdiginiz IPN adresinizin http://example.com/ipn/default.aspx oldugunu farzedelim. Siparis onayi verildiginde bu adrese IPN bilgileri HTTP POST olarak gonderilecektir. Bu durumda bu bilgilere kolay bir sekilde erisebilmek icin asagidaki ornekte oldugu gibi `IPNRequest.FromHttpRequest` metodu kullanilarak yeni bir `IPNRequest` nesnesi yaratilmalidir. Bu metod ile `IPNRequest` nesnesi yaratilirken gelen POST alanlari dogru bir sekilde islenerek nesnenin ilgili alanlarina kolay erisim icin eklenecektir.

```csharp
public partial class Default: System.Web.UI.Page {
  public void Page_Load() {
    // Some code here
    IPNRequest ipn = IPNRequest.FromHttpRequest(Request);

    Console.WriteLine("Siparis Durumu: {0}", ipn.OrderStatus);
    // Some code here
  }
}
```

### IPN cevabinin olusturulmasi

Eger IPN istegi basarili bir sekilde islendiyse HTTP 200 kodu ile PayU dokumaninda belirtilen XML formatinda bir cevap dondurulmelidir:

> PayU, aşağıdaki formatta (sayfanın herhangi bir yerinde) bir yanıt bekler:
> <epayment>DATE|HASH</epayment>

Bu cevap alanindaki Hash'i hesaplamak ve dogru XML'i olusturmak icin de `IPNRequest` nesnesinin `GenerateResponse` methodu kullanilmalidir. Bunun icin ornek kod su sekildedir:

```csharp
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