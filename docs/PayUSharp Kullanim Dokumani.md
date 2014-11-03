## Giriş

**PayUSharp** kütüphanesi .NET platformu üzerinde işlem yapan işyerlerinin PayU Türkiye servisleri ile entegrasyonunu kolaylaştırmak amacı ile geliştirilmiştir. Kütüphane PayU Türkiye'nin sunduğu [LiveUpdate](#lu), [IPN](#ipn), [Automatic LiveUpdate](#alu) ve [Token](#token) servisleri için kolay kullanılabilir nesne yönelimli sınıflar sağlamaktadır.

**PayUSharp** bir adet çekirdek, 4 adet servis kütüphasi olmak üzere 5 adet DLL'den oluşmaktadır:

1. `PayUSharp.Core.dll`
2. `PayUSharp.LiveUpdate.dll`
3. `PayUSharp.AutomaticLiveUpdate.dll`
4. `PayUSharp.IPN.dll`
5. `PayUSharp.Token.dll`

İşyerleri kullandıkları servislere ait tüm DLL'leri ve `PayUSharp.Core` DLL'ini projelerine `Reference` olarak eklemelidirler.

## LiveUpdate Servisi {#lu}

<<[services/lu.md]

\pagebreak

## IPN Servisi {#ipn}

<<[services/ipn.md]

\pagebreak

## Automatic LiveUpdate Servisi {#alu}

<<[services/alu.md]

\pagebreak

## Token Servisi {#token}

<<[services/token.md]
