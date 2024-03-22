using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class NoContentDto
    {
        public List<String> Errors { get; set; }
    }
}


/*
 Client'a içeriği olmayan başarılı veya başarısız durum kodunu döneceğimiz zaman bu sınıfı kullanacağız. Amaç; update veya delete gibi işlemlerde bir içerik yok ama işlemin başarılı olduğunu client'a dönmemiz gerekiyor. Bu sınıf bu işlemi yapacak
 */
