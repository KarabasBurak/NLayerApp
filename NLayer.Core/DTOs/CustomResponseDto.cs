using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }


        public List<String> Errors { get; set; }


        /*
        1) Yukarıda tanımlanan entityler için aşağıda Success, Fail metotları tanımlandı.
        2) Success ve Fail olduğunda yukarıdaki entityler için statusCode, data, errors gibi nesneler üzerinden client'a cevap döneceğiz.
        3) Bu işlemleri CustomResponseDto sınıfında şu şekilde yaptık. CustomResponseDto sınıfında (yukarıda) tanımlanan entitylerden yeni nesneler (data, statusCode, errors) oluşturduk. Bu neseneler üzerinden client'a işlemin başarılı veya başarısız olduğunu StatusCode üzerinden cevap döneceğiz. 

        ÖRNEĞİN; client, bir ürün ekleme işlemi yapacak. Bu işlem başarılı olursa 201 StatusCode'u ile başarılı olduğunu client'a cevap olarak dönmem gerekiyor. Bunun için Success metotlarında oluşturulan nesneler ile client'a cevap döneceğiz ama statusCode'u client görmeyecek. Başarısız olursa da Fail static metotlarında tanımlanan nesneler ile cevap döneceğiz. 
         */
        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T> { Data = data, StatusCode = statusCode };
        }
        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };
        }

        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errors };
        }

        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { error } };
        }

    }
}

/*
 JsonIgnore attribute'u, genellikle veri gizliliği, güvenliği veya basitlik amacıyla, serileştirmede dikkate alınmasını istemediğiniz özelliklerin üzerine eklenir. Örneğin, kullanıcı şifreleri veya hassas kullanıcı bilgileri gibi verilerin JSON çıktısında yer almasını engellemek için kullanılabilir. JsonIgnore ile client'a cevap dönüşü engelledik.
 */
