LiveUpdate, son kullanıcıların PayU Ortak Ödeme Sayfası'na yönlendirilmesi ile ödeme yaptığı entegrasyon metodudur. Bu entegrasyon Merchant'ın sipariş ile ilgili bilgileri bir HTML Form'u olarak oluşturup bu form'un PayU LiveUpdate adresine POST edilmesini sağlaması ile gerçekleşir.

PayUSharp kütüphanesi ile bahsedilen HTML Form'unun oluşturulması ve, istenirse, bu form'un otomatik olarak POST edilmesi işlemleri kolaylaştırılmıştır.

### Başlangıç {#lustart}

Herhangi bir LiveUpdate işlemi gerçekleştirmeden önce yeni bir `PayU.LiveUpdate.LiveUpdateService` nesnesi yaratılmalıdır. Bu nesneye geçirilmesi zorunlu olan tek parametre `signatureKey` alanıdir ve PayU'dan alınan imza anahtarı değeri geçirilmelidir.

Örnek kullanım şu şekildedir:

```cs
  var service = new PayU.LiveUpdate.LiveUpdateService('signatureKey');
```

Ayrıca servisin testler sırasında kullanılabilecek `endpointUrl` parametresi de bulunmaktadır. Bu parametrenin varsayılan değerlerini PayU tarafından tavsiye edilmedikçe değiştirmemeniz önerilir.

### LiveUpdate Siparişinin Oluşturulması

LiveUpdate sipariş bilgileri PayUSharp kütüphanesinde `PayU.LiveUpdate.OrderDetails` sınıfı ile temsil edilmektedir. Yeni bir `PayU.LiveUpdate.OrderDetails` nesnesi yaratılarak ve bu nesnenin [alanları](#lufields) sipariş bilgileri ile doldurularak bir PayUSharp LiveUpdate siparişi oluşturulur:

```cs
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
      LastName = "Yilmaz",
      Email = "ahmet.yilmaz@payu.com.tr",
      City = "Kagithane", // Ilce/Semt
      State = "Istanbul", // Sehir
      CountryCode = "TR"
  };
```

### LiveUpdate Siparişinin Gönderilmesi

LiveUpdate sürecinin tamamlanabilmesi için oluşturulmuş olan `PayU.LiveUpdate.OrderDetails` tipindeki sipariş bilgisi HTML formu olarak Web sayfasına eklenmeli ve bu form PayU LiveUpdate adresine POST edilmelidir.

PayUSharp kütüphanesi HTML form işlemleri için [Başlangıç](#lustart) adımında yarattığımız `LiveUpdateService` nesnesini kullanmaktadır. Sipariş bilgisi bu nesnenin `RenderPaymentForm` metoduna verilerek bahsedilen HTML formu string olarak oluşturulabilir:

```cs
  var service = new LiveUpdateService('signatureKey');

  //... Order yaratilir

  // sadece Submit dugmesi adi verilerek
  string htmlForm = service.RenderPaymentForm(order, "PayU ile Ödeme Yap");

  // veya hem Submit dugmesi adi hem de Form Id'si belirterek
  string htmlFormWithId = service.RenderPaymentForm(order, "PayU ile Ödeme Yap", "PayULiveUpdateForm");
```

Oluşturulan bu HTML formu kullanılan web framework'üne uygun bir şekilde müşteriye gösterilecek sipariş sayfasına eklenmelidir. Bu işlem framework'ler arasında farklı şekillerde olmaktadır. ASP.NET Web Forms ile sayfada bir `Literal` alan belirlenip, form datası bu alana yazdırılabilir:

```cs
  ltrLiveUpdateForm.Text = service.RenderPaymentForm(order, "PayU ile Ödeme Yap", "PayULiveUpdateForm");
```

Son olarak, sipariş bilgilerini içeren bu HTML form'unun POST metodu ile submit edilmesi gerekmektedir. Eğer dükkanınızın akışı bu işlemi kullanıcının yapması üzerine kurulu ise yukarıda yapılanlar sizin için yeterli olacaktır. Fakat akışınız bu noktada kullanıcıyı otomatik olarak Ortak Ödeme Sayfasına yönlendirmek üzerine kurulu ise, o zaman sayfanızda aşağıdaki gibi bir JavaScript kod parçacığı eklenmelidir:

```html
<script>
  // DIKKAT: Olusturulmus olan PayU form'unun Form ID'si kullanilmali...
  document.getElementById('PayuLiveUpdateForm').submit();
</script>
```

### LiveUpdate Sipariş Sonucunun İşlenmesi

Eğer LiveUpdate ile ilk gönderilen sipariş bilgilerinde `ReturnUrl` alanı doldurulmuşsa, kullanıcı ödeme işlemi bitiminde PayU tarafından verilen URL'e yönlendirilecektir. Bu adresi uygulamanızdaki bir sayfa olarak tanımlamanız durumunda kullanıcılarınız ödeme işlemi sonrasında sizin sayfanıza otomatik olarak geleceklerdir. Siz de bu sayfada ödemenin referans kodu ile işlem yaparak müşteriye ödemenin alındığını söyleyebilir, ödeme detaylarını gösterebilirsiniz.

Bu sayfaya PayU dışında başka kaynaklardan istek gelmemesi için PayU tarafından gönderilen parametrelerin arasında bir de doğrulama kodu bulunmaktadır. Güvenlik nedeniyle bu kod doğrulanmadan ödeme bilgilerine güvenmemeniz çok önemlidir. Doğrulama kodu yeni yaratılan bir `PayU.LiveUpdate.LiveUpdateService` nesnesinin `VerifyControlSignature` metodu çağırılarak kontrol edilebilir:

```cs
  var service = new LiveUpdateService('signatureKey');

  // Verify the signature in the "ctrl" query string parameter
  var verified = service.VerifyControlSignature(Request);
```

### LiveUpdate Sipariş Alanları {#lufields}

<<[fields/lu_fields.md]
