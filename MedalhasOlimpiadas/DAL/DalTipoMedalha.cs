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
    public class DalTipoMedalha
    {
        public List<EntTipoMedalha> ObterTodos(Int32 Status, SqlCommand db)
        {
            SqlCommand scCommand = new SqlCommand("STP_TipoMedalhaSelecionarTodos", db.Connection);
            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = Utils.ToBooleanNull(Status);

            SqlDataReader reader = scCommand.ExecuteReader();

            if (reader.FieldCount > 0)
            {
                return this.Popular(reader);
            }
            else
            {
                return new List<EntTipoMedalha>();
            }
        }

        private List<EntTipoMedalha> Popular(SqlDataReader dtrDados)
        {
            List<EntTipoMedalha> listEntReturn = new List<EntTipoMedalha>();
            EntTipoMedalha entReturn;

            try
            {
                while (dtrDados.Read())
                {
                    entReturn = new EntTipoMedalha();

                    entReturn.IdTipoMedalha = (int) dtrDados["PK_TIPO_MEDALHA"];
                    entReturn.TipoMedalha = dtrDados["TX_TIPO_MEDALHA"].ToString();
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

        public EntTipoMedalha ObterPorId(Int32 IdTipoMedalha, SqlCommand db)
        {
            SqlCommand scCommand = new SqlCommand("STP_TipoMedalhaObterPorId", db.Connection);
            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@IdTipoMedalha", SqlDbType.Int).Value = IdTipoMedalha;

            SqlDataReader reader = scCommand.ExecuteReader();

            if (reader.FieldCount > 0)
            {
                return this.Popular(reader)[0];
            }
            else
            {
                return new EntTipoMedalha();
            }
        }
    }
}
