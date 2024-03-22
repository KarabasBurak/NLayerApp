using FluentValidation;
using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Validation
{
    public class ProductDtoValidator:AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x=> x.Name).NotNull().WithMessage("{PropertyName} Alanı Zorunludur").NotEmpty().WithMessage("{PropertyName} Boş Geçilemez");
            RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} 0'dan Büyük Olmalıdır");
            RuleFor(x => x.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} 0'dan Büyük Olmalıdır");
            RuleFor(x => x.CategoryId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} 0'dan Büyük Olmalıdır");

        }
    }
}

/*
 int, decimal, Double, DateTime, bool (default değeri false) gibi sayısal değerler alabilen değişkenler value tiptir. Bu değişkenlerin default değerleri 0'dır. Bundan dolayı NotNull, NotEmpty gibi validator yazılmaz.
string değişkenler ise referans tiplidir. Default değerleri yoktur bundan dolayı validator yazmak istediğimizde NotNull veya NotEmpty validator yazılabilir.
 */
