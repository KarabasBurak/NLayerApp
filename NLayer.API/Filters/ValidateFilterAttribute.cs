using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;

namespace NLayer.API.Filters
{
    public class ValidateFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage).ToList();

                context.Result=new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400,errors));

            }
        }
    }
}

/*
 Select, koleksiyondaki her eleman için tek bir sonuç üretirken; SelectMany, her eleman için birden fazla sonuç üretebilir ve bunları tek bir koleksiyonda birleştirir.

 Select genellikle basit dönüşümler ve projeksiyonlar için kullanılır. SelectMany ise, iç içe koleksiyonların birleştirilmesi veya bir elemandan birden çok değer elde edilmesi gereken durumlar için idealdir.

Yukarıda ModelState'den gelen verilerde bir hatanın birden fazla bileşeni olabilir bu bileşenleri tek bir koleksiyona toplamak istedik ve SelectMany metodunu kullandık.
Bir hatanın da bir mesajını yakalamak için de Select metodunu kullandık ve bunları ToList() ile listeledik.

 
 */

/*
 ModelState, client'dan gelen requestleri server tarafına iletirken bir model olarak sınıf aracılığı ile DB'ye göndermek ister. Bu model üzerinde tanımlı olan veri doğrulama kuralları (Data Annotations gibi) aracılığıyla, gelen verilerin doğruluğu ve uygunluğu kontrol edilir. ModelState bu doğrulama sürecinin sonuçlarını saklar ve bu sonuçlara göre bir isteğin işlenip işlenemeyeceğine karar verilmesine yardımcı olur.

Eğer hata var ise ModelState bu hataları saklar.
 */

/*
 Program.cs'2 bu ValidateFilterAttribute işlemlerini bildireceğiz. Burada kendimiz custom olarak bir hata dönüş modeli oluşturduk. Bu modeli program.cs'e bildirmemiz lazım çünkü API'de default olarak bir model var ama biz o modeli kullanmak istemedik. Bundan dolayı API'nin hata dönüşlerini inaktif yapmamız gerekiyor

MVC tarafında default olarak bir model yok o yüzden herhangi bir hata dönüş modelini inaktif yapmamıza gerek yok.(API'deki default modeli "SuppressModelStateInvalidFilter" ile inaktif yapacağız)
 */
