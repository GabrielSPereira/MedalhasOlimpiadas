using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MedalhasOlimpiadas.Commons;

namespace MedalhasOlimpiadas.Models
{
    public class DalPais
    {
        public List<EntPais> ObterTodos(String Nome, Int32 Status, SqlCommand db)
        {
            SqlCommand scCommand = new SqlCommand("STP_PaisSelecionarTodos", db.Connection);
            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@Nome", SqlDbType.VarChar).Value = Nome;
            scCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = Utils.ToBooleanNull(Status);

            SqlDataReader reader = scCommand.ExecuteReader();

            if (reader.FieldCount > 0)
            {
                return this.Popular(reader);
            }
            else
            {
                return new List<EntPais>();
            }
        }

        private List<EntPais> Popular(SqlDataReader dtrDados)
        {
            List<EntPais> listEntReturn = new List<EntPais>();
            EntPais entReturn;

            try
            {
                while (dtrDados.Read())
                {
                    entReturn = new EntPais();

                    entReturn.IdPais = (int) dtrDados["PK_PAIS"];
                    entReturn.Nome = dtrDados["TX_NOME"].ToString();
                    entReturn.Sigla = dtrDados["TX_SIGLA"].ToString();
                    entReturn.Ativo = (Boolean) dtrDados["FL_ATIVO"];
                    listEntReturn.Add(entReturn);
                }

                dtrDados.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listEntReturn;
        }

        public EntPais ObterPorId(Int32 IdPais, SqlCommand db)
        {
            SqlCommand scCommand = new SqlCommand("STP_PaisSelecionarPorId", db.Connection);
            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@IdPais", SqlDbType.Int).Value = IdPais;

            SqlDataReader reader = scCommand.ExecuteReader();

            if (reader.FieldCount > 0)
            {
                return this.Popular(reader)[0];
            }
            else
            {
                return new EntPais();
            }
        }
    }
}
