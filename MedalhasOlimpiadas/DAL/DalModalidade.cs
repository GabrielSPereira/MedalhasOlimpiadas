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
    public class DalModalidade
    {
        public List<EntModalidade> ObterTodos(String Modalidade, Int32 IsMulher, Int32 Status, SqlCommand db)
        {
            SqlCommand scCommand = new SqlCommand("STP_ModalidadeSelecionarTodos", db.Connection);
            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@Modalidade", SqlDbType.VarChar).Value = Modalidade;
            scCommand.Parameters.Add("@IsMulher", SqlDbType.Bit).Value = Utils.ToBooleanNull(IsMulher);
            scCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = Utils.ToBooleanNull(Status);

            SqlDataReader reader = scCommand.ExecuteReader();

            if (reader.FieldCount > 0)
            {
                return this.Popular(reader);
            }
            else
            {
                return new List<EntModalidade>();
            }
        }

        private List<EntModalidade> Popular(SqlDataReader dtrDados)
        {
            List<EntModalidade> listEntReturn = new List<EntModalidade>();
            EntModalidade entReturn;

            try
            {
                while (dtrDados.Read())
                {
                    entReturn = new EntModalidade();

                    entReturn.IdModalidade = (int) dtrDados["PK_MODALIDADE"];
                    entReturn.Modalidade = dtrDados["TX_MODALIDADE"].ToString();
                    entReturn.IsMulher = (Boolean) dtrDados["IS_MULHER"];
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

        public EntModalidade ObterPorId(Int32 IdModalidade, SqlCommand db)
        {
            SqlCommand scCommand = new SqlCommand("STP_ModalidadeSelecionarPorId", db.Connection);
            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@IdModalidade", SqlDbType.Int).Value = IdModalidade;

            SqlDataReader reader = scCommand.ExecuteReader();

            if (reader.FieldCount > 0)
            {
                return this.Popular(reader)[0];
            }
            else
            {
                return new EntModalidade();
            }
        }
    }
}
