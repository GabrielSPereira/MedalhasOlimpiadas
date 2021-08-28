using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedalhasOlimpiadas.Models
{
    public class EntMedalha
    {
        public Int32 IdMedalha { get; set; }
        public Int32 posicaoQuadro { get; set; }
        public Int32 quantidadeOuro { get; set; }
        public Int32 quantidadePrata { get; set; }
        public Int32 quantidadeBronze { get; set; }
        public Int32 quantidadeMedalha
        {
            get
            {
                return quantidadeOuro + quantidadePrata + quantidadeBronze;
            }
        }
        private EntTipoMedalha _TipoMedalha;
        public EntTipoMedalha TipoMedalha
        {
            get
            {
                if (_TipoMedalha == null)
                {
                    _TipoMedalha = new EntTipoMedalha();
                }
                return _TipoMedalha;
            }

            set { _TipoMedalha = value; }
        }
        private EntAtleta _Atleta;
        public EntAtleta Atleta
        {
            get
            {
                if (_Atleta == null)
                {
                    _Atleta = new EntAtleta();
                }
                return _Atleta;
            }

            set { _Atleta = value; }
        }
        public Boolean Ativo { get; set; }
    }
}
