using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedalhasOlimpiadas.Models
{
    public class EntAtleta //Atributos de Atletas
    {
        public Int32 IdAtleta { get; set; }
        public String Nome { get; set; }
        public String NomePais
        {
            get
            {
                return Nome + " - " + Pais.Nome;
            }
        }
        private EntModalidade _Modalidade;
        public EntModalidade Modalidade
        {
            get
            {
                if (_Modalidade == null)
                {
                    _Modalidade = new EntModalidade();
                }
                return _Modalidade;
            }

            set { _Modalidade = value; }
        }
        private EntPais _Pais;
        public EntPais Pais
        {
            get
            {
                if (_Pais == null)
                {
                    _Pais = new EntPais();
                }
                return _Pais;
            }

            set { _Pais = value; }
        }
        public Boolean Ativo { get; set; }
    }
}
