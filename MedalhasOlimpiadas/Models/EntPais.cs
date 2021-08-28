using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedalhasOlimpiadas.Models
{
    public class EntPais
    {
        public Int32 IdPais { get; set; }
        public String Nome { get; set; }
        public String Sigla { get; set; }
        public String Bandeira { get; set; }
        public Boolean Ativo { get; set; }
    }
}
