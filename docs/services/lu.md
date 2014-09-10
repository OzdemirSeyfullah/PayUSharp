LiveUpdate, son kullanıcıların PayU Ortak Ödeme Sayfası'na yönlendirilmesi ile ödeme yaptığı entegrasyon metodudur. Bu entegrasyon Merchant'ın sipariş ile ilgili bilgileri bir HTML Form'u olarak oluşturup bu form'un PayU LiveUpdate adresine POST edilmesini sağlaması ile gerçekleşir.

PayUSharp kütüphanesi ile bahsedilen HTML Form'unun oluşturulması ve, istenirse, bu form'un otomatik olarak POST edilmesi işlemleri kolaylaştırılmıştır.

### Ayarlar

Herhangi bir PayU işlemi gerçekleştirilmeden önce (tercihen 1 kere uygulama başlangıcında) PayUSharp kütüphanesinin ayarlarının doğru bir şekilde yapılması gerekmektedir. Bunun için `PayU.Configuration` sınıfı kullanılmaktadır.

LiveUpdate için zorunlu ayarlar `SignatureKey` ve `Environment` alanlarıdır. Örnek kullanım şu şekildedir:

```csharp
  PayU.Configuration.Instance
      .SetSignatureKey('signaturekey')
      .SetEnvironment("https://secure.payuodeme.com/order/");
```

### LiveUpdate Siparişinin Oluşturulması

LiveUpdate sipariş bilgileri PayUSharp kütüphanesinde `PayU.LiveUpdate.OrderDetails` sınıfı ile temsil edilmektedir. Yeni bir `PayU.LiveUpdate.OrderDetails` nesnesi yaratılarak ve bu nesnenin [alanları][LUFields] sipariş bilgileri ile doldurularak bir PayUSharp LiveUpdate siparişi oluşturulur:

```csharp
  var order = new PayU.LiveUpdate.OrderDetails();

  order.Merchant = "PAYUDEMO";
  order.OrderRef = "6112457";

  var product1 = new PayU.LiveUpdate.ProductDetails
  {
      Code = "Product1 code",
      Name = "Product1 name",
      Quantity = 2,
      VAT = 8.00M,
      UnitPrice = 20.00M,
      Information = "Product1 info",
      PriceType = PriceType.GROSS
  }

  var product2 = new PayU.LiveUpdate.ProductDetails
  {
      Code = "Product2 code",
      Name = "Product2 name",
      Quantity = 1,
      VAT = 8.00M,
      UnitPrice = 40.00M,
      Information = "Product2 info",
      PriceType = PriceType.GROSS
  }

  order.ProductDetails.Add(product1);
  order.ProductDetails.Add(product2);

  order.ShippingCosts = 10.00M;
  order.DestinationCity = "Ankara";
  order.DestinationState = "Ankara";
  order.DestinationCountry = "TR";

  order.BillingDetails = new PayU.LiveUpdate.BillingDetails {
      FirstName = "Ahmet",
      LastName = "Yılmaz",
      Email = "ahmet.yılmaz@payu.com.tr",
      City = "Kağıthane", // Ilce/Semt
      State = "Istanbul", // Sehir
      CountryCode = "TR"
  };
```

### LiveUpdate Siparişinin Gönderilmesi

LiveUpdate sürecinin tamamlanabilmesi için oluşturulmuş olan `PayU.LiveUpdate.OrderDetails` tipindeki sipariş bilgisi HTML formu olarak Web sayfasına eklenmeli ve bu form PayU LiveUpdate adresine POST edilmelidir.

PayUSharp kütüphanesi HTML form işlemleri için `PayU.LiveUpdate.LiveUpdateRequest` sınıfını kullanmaktadır. Sipariş bilgisi ile yaratılan yeni bir `PayU.LiveUpdate.LiveUpdateRequest` nesnesinin `RenderPaymentForm` metodu çağırılarak bu HTML formu string olarak oluşturulabilir:

```csharp
  var request = new LiveUpdateRequest(order);

  // sadece Submit düğmesi adı verilerek
  string htmlForm = request.RenderPaymentForm("PayU ile Ödeme Yap");

  // veya hem Submit düğmesi adı hem de Form Id'si belirterek
  string htmlFormWithId = request.RenderPaymentForm("PayU ile Ödeme Yap", "PayULiveUpdateForm");
```

Oluşturulan bu HTML formu kullanılan web framework'üne uygun bir şekilde müşteriye gösterilecek sipariş sayfasına eklenmelidir. Bu işlem framework'ler arasında farklı şekillerde olmaktadır. ASP.NET Web Forms ile sayfada bir `Literal` alan belirlenip, form datası bu alana yazdırılabilir:

```csharp
  var request = new LiveUpdateRequest(order);

  ltrLiveUpdateForm.Text = request.RenderPaymentForm("PayU ile Ödeme Yap", "PayULiveUpdateForm");
```

Son olarak, sipariş bilgilerini içeren bu HTML form'unun POST metodu ile submit edilmesi gerekmektedir. Eğer dükkanınızın akışı bu işlemi kullanıcının yapması üzerine kurulu ise yukarıda yapılanlar sizin için yeterli olacaktır. Fakat akışınız bu noktada kullanıcıyı otomatik olarak Ortak Ödeme Sayfasına yönlendirmek üzerine kurulu ise, o zaman sayfanızda aşağıdaki gibi bir JavaScript kod parçacığı eklenmelidir:

```html
<script>
  // DIKKAT: Oluşturulmuş olan PayU form'unun Form ID'si kullanılmalı...
  document.getElementById('PayuLiveUpdateForm').submit();
</script>
```

### LiveUpdate Sipariş Sonucunun İşlenmesi

Eğer LiveUpdate ile ilk gönderilen sipariş bilgilerinde `ReturnUrl` alanı doldurulmuşsa, kullanıcı ödeme işlemi bitiminde PayU tarafından verilen URL'e yönlendirilecektir. Bu adresi uygulamanızdaki bir sayfa olarak tanımlamanız durumunda kullanıcılarınız ödeme işlemi sonrasında sizin sayfanıza otomatik olarak geleceklerdir. Siz de bu sayfada ödemenin referans kodu ile işlem yaparak müşteriye ödemenin alındığını söyleyebilir, ödeme detaylarını gösterebilirsiniz.

Bu sayfaya PayU dışında başka kaynaklardan istek gelmemesi için PayU tarafından gönderilen parametrelerin arasında bir de doğrulama kodu bulunmaktadır. Güvenlik nedeniyle bu kod doğrulanmadan ödeme bilgilerine güvenmemeniz çok önemlidir. Doğrulama kodu `PayU.LiveUpdate.LiveUpdateRequest` sınıfının `VerifyControlSignature` metodu çağırılarak kontrol edilebilir:

```csharp
  // Verify the signature in the "ctrl" query string parameter
  var verified = LiveUpdateRequest.VerifyControlSignature(Request);
```

### LiveUpdate Sipariş Alanları [LUFields]

<<[fields/lu_fields.md]