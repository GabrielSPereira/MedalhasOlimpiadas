using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedalhasOlimpiadas.Models
{
    public class EntModalidade
    {
        public Int32 IdModalidade { get; set; }
        public String Modalidade { get; set; }
        public String ModalidadeGenero
        {
            get
            {
                if (IsMulher)
                {
                    return Modalidade + " - Feminino";
                }
                return Modalidade + " - Masculino";
            }
        }
        public Boolean IsMulher { get; set; }
        public Boolean Ativo { get; set; }
    }
}
