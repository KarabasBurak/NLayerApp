using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        //CreateActionResult bir endpoint değildir. Swagger bunu endpoint olarak algılamaması için [NonAction] attibute kullandık. Bu metodu API tarafındaki action metotlarda yani endpointler için kullanacağız
        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> customResponseDto)
        {
            if (customResponseDto.StatusCode == 204)
                return new ObjectResult(null)
                {
                    StatusCode = customResponseDto.StatusCode
                };


            return new ObjectResult(customResponseDto)
            {
                StatusCode = customResponseDto.StatusCode
            };
        }
    }
}
