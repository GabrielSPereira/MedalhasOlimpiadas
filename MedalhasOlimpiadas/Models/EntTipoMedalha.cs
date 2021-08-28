using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedalhasOlimpiadas.Models
{
    public class EntTipoMedalha
    {
        public const Int32 TIPO_MEDALHA_OURO = 1;
        public const Int32 TIPO_MEDALHA_PRATA = 2;
        public const Int32 TIPO_MEDALHA_BRONZE = 3;
        public Int32 IdTipoMedalha { get; set; }
        public String TipoMedalha { get; set; }
        public Boolean Ativo { get; set; }
    }
}
