using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTOs;
using NLayer.Service.Exceptions;
using System.Text.Json;

namespace NLayer.API.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app) 
        {
            app.UseExceptionHandler(config =>
            {
                // Bir hata olduğunda çalışacak olan kod bloğu config.Run'dır. hata sayfası gösterme, hata loglama, özel hata mesajları gönderme işlemleri yapılacak. Run metodu config nesnesi üzerinde çağrılan bir metoddur ve ayrıca middleware sonlandırır.
                config.Run(async context =>
                {
                    // API'de yazdığımız için response tipini belirledik.
                    context.Response.ContentType = "application/json"; 
                                                                       // 
                 // IExceptionHandlerFeature interface ile fırlatılan hatayı yakaladık. context nesnesi üzerinden Feature koleksiyonuna erişip ve bu koleksiyondan IExceptionHandlerFeature arayüzü ile hata yakalandı ve exceptionFeature nesnesine atandı. 
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();  

                    // exceptionFeature nesnesine atanan hataya switch ifadesi ile statusCode atama işlemi yapılır. Aşağıda, Client tarafından hata ise 400, Client tarafından değilse 500 olarak belirledik.
                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        _ => 500
                    };

                    // Yukarıda belirlenen statusCode, HttpContext context'in response'un StatusCoduna atadık.
                    context.Response.StatusCode = statusCode;


                    // Fail durumunda yukarıdan gelen statusCode ve hata mesajını response nesnesine atadık
                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);

                    // response nesnesini Json formatına dönüştürerek yazdırdık.API controllerda json dönüştürme işlemi otomatik oluyor ama middleware'de otomatik olmaz o yüzden biz yazdık
                    // context.Response.WriteAsync HTTP yanıtının gövdesine veri yazmak için kullanılan bir metoddur.
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));

                });
            });

        }
    }
}








/*
 * EXTENSION METOD
 1) Extension metot; var olan bir sınıfa yeni metodlar eklememizi sağlayan metodlardır. Extension metod sınıfı değiştirmeden sınıfın yeteneklerini genişletmemize olanak tanır. Yani bir sınıfa yeni özellikler eklemek için extension  metod kullanırız. 
 2) Extension metot yazacağımız sınıf ve sınıfın içine yazacağımız extension metot static olmalıdır.
 3) Extension metodunun genişletmek istediğiniz sınıf türündeki ilk parametresi "this" anahtar kelimesi ile işaretlenir. Bu, metodun hangi tür üzerinde genişletme yaptığını belirler.
 
MIDDLEWARE
1) Middleware; web uygulamasında request ve response'larda araya girerek herhangi bir işlem yapar. 
2 ) Bir web uygulamasına yapılan HTTP isteği, tanımlanan middleware bileşenleri üzerinden sırasıyla geçer. her middleware görevi farklıdır ve isteği işleyebilir. Örneğin; güvenlik kontrolü yapabilir, isteğin içeriğini değiştirebilir, loglama yapabilir. Yani middleware yazılmışsa Request durumunda adım adım kontrollerden geçer ve action metoda ulaşır. Server, response döndüğünde de aynı şekilde hangi middleware var ise onlara uğrayarak response döner. Request ulaşana kadar hangi middleware işlemleri var ise onlara uğramadan action metoda gitmez. Response durumu da aynısıdır.

*** Burada UseCustomExceptionHandler sınıfı bir middleware'dır. UseCustomExceptionHandler sınıfı global hata yönetimini yapacağız ama bunu customize ederek yapacağız. Yani UseCustomExceptionHandler sınıfı request ve response durumlarında araya girerek hata yönetimi yapacak yani middleware olacak. Biz custom bir middleware oluşturacağımız için extension metodlar tanımladık. Extension metod olacağı için de UseCustomExceptionHandler sınıfı static oldu.

Middleware'den geçemeyen bir request veya response daha ileriye gitmeden geri döner. Zaten amaç da budur. aslında UseCustomExceptionHandler middleware'den bir kontrol mekanizmasıdır. UseCustomExceptionHandler sınıfında yaptığım kontrolden geçerse request veya response yoluna devam etsin eğer bu sınıfa takılırsa yoluna devam edemesin

UseExceptionHandler metodu .Net Core'da hataları yakalayan bir metoddur. Bu metodu UseCustomExceptionHandler sınıfında kullanıyoruz.
 */