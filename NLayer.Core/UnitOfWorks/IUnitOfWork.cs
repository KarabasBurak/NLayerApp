using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        // SaveChangeAsync metodu ve SaveChange metodu

        Task CommitAsync();
        void Commit();

    }
}



    /*
        UnitOfWork : Db'ye yapılacak olan işlemleri toplu bir şekilde,  tek transaction (Veri silme, ekleme ve güncelleme işlemleri) üzerinden yönetmemizi sağlar.
       Örneğin:
       Category ve Product Repositoryde yapılan CRUD işlemleri yaptık. Bu CRUD işlemleri EF Core'un SaveChange metodunu çağırana kadar memoryde tutar. Yani Db'ye yansıtmaz. SaveChange metodunu çağırdığımızda CRUD işlemlerini DB'ye yansıtır. Biz bu SaveChange metodunu kontrol altına almamız gerekiyor.

       UnitOfWork kullanmasaydık;
       Category Repositoryde CRUD işlemleri yaptık savechange dedik ve dbye yansıdı fakat Product Repositoryde CRUD işlemleri yaptık ve problem oldu DB'ye yansımadı. Burada tutarsız veriler ile karşılaşabiliriz işte bununla karşılaşmamak için UnitOfWork bu iki repositorydeki CRUD işlemlerini yaptıktan sonra aynı anda SaveChange metodunu çağırıp DB'ye yansıtır. Eğer iki repositoryden birinde problem olursa DB'ye yansıtmaz yani tutarsız veri depolamanın önüne geçmiş olur. Biz UnitOfWork interface üzerinden ne zaman SaveChange metodunu çağırırsak o zaman DB'ye kaydedecek
        */

