using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            
            var idValue = context.ActionArguments.Values.SingleOrDefault();

            if(idValue == null )
            {
                await next.Invoke();
                return;
            }

            var id=(int)idValue;
            var anyEntity=await _service.AnyAsync(x=>x.Id==id);

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"There is no{id} and {typeof(T).Name} Not Found"));
        }
    }
}

/*
Filter mantığı; Exception olarak yönettiğimiz hataları burada da yapabileceğimizdir. Yani UseExceptionHandler yapmadan da Filter ile hata yönetimi yapılabilir.

Filter; Request action metoda girmeden çalışır.

 Bir Filter'ın Constructor'ında herhangi bir Sınıfı, Interface'i DI olarak geçiyorsa bu Filter'ı program.cs'de geçmemeiz lazım.
 */
