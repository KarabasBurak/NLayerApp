using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Exceptions
{
    public class ClientSideException:Exception
    {
        public ClientSideException(string message):base(message)
        {
            
        }
    }
}

/*
 ClientSideException sınıfında ClientSideException adında constructor oluşturduk. Client tarafından alınan hatayı message nesnesine yazdırıcağız.

base(message); Exception sınıfının constructor'ına gidiyor.
 */

