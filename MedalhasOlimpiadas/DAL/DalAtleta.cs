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
    public class DalAtleta
    {
        public void Inserir(EntAtleta objAtleta, SqlCommand db)
        {
            SqlCommand scCommand = new SqlCommand("STP_AtletaInserir", db.Connection); //Chama a Procedure responsável pela inserção do atleta
            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@PK_ATLETA", SqlDbType.Int).Value = objAtleta.IdAtleta; //Passa os Parâmetros necessários para a tabela atleta
            scCommand.Parameters.Add("@FK_MODALIDADE", SqlDbType.Int).Value = objAtleta.Modalidade.IdModalidade;
            scCommand.Parameters.Add("@FK_PAIS", SqlDbType.Int).Value = objAtleta.Pais.IdPais;
            scCommand.Parameters.Add("@TX_NOME", SqlDbType.VarChar).Value = objAtleta.Nome;
            scCommand.Parameters.Add("@FL_ATIVO", SqlDbType.Bit).Value = objAtleta.Ativo;

            scCommand.ExecuteNonQuery();
        }

        public void Alterar(EntAtleta objAtleta, SqlCommand db)
        {
            SqlCommand scCommand = new SqlCommand("STP_AtletaAlterar", db.Connection);
            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@PK_ATLETA", SqlDbType.Int).Value = objAtleta.IdAtleta;
            scCommand.Parameters.Add("@FK_MODALIDADE", SqlDbType.Int).Value = objAtleta.Modalidade.IdModalidade;
            scCommand.Parameters.Add("@FK_PAIS", SqlDbType.Int).Value = objAtleta.Pais.IdPais;
            scCommand.Parameters.Add("@TX_NOME", SqlDbType.VarChar).Value = objAtleta.Nome;
            scCommand.Parameters.Add("@FL_ATIVO", SqlDbType.Bit).Value = objAtleta.Ativo;

            scCommand.ExecuteNonQuery();
        }

        public void Remover(EntAtleta objAtleta, SqlCommand db)
        {
            SqlCommand scCommand = new SqlCommand("STP_AtletaRemover", db.Connection);
            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@PK_ATLETA", SqlDbType.Int).Value = objAtleta.IdAtleta;
            scCommand.Parameters.Add("@FL_ATIVO", SqlDbType.Bit).Value = objAtleta.Ativo;

            scCommand.ExecuteNonQuery();
        }

        public List<EntAtleta> ObterTodos(Int32 IdModalidade, Int32 IdPais, String Nome, Int32 Status, SqlCommand db)
        {
            SqlCommand scCommand = new SqlCommand("STP_AtletaSelecionarTodos", db.Connection);
            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@IdModalidade", SqlDbType.Int).Value = Utils.ToIntNull(IdModalidade); //Passa os Parâmetros de busca
            scCommand.Parameters.Add("@IdPais", SqlDbType.Int).Value = Utils.ToIntNull(IdPais); //Por exemplo, caso o usuário não queira buscar por país através do método ToIntNull ele retorna o valor como nulo
            scCommand.Parameters.Add("@Nome", SqlDbType.VarChar).Value = Nome;
            scCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = Utils.ToBooleanNull(Status);

            SqlDataReader reader = scCommand.ExecuteReader();

            if (reader.FieldCount > 0) //Caso retorne algo entra no IF
            {
                return this.Popular(reader); //Chama o método responsável por montar a lista de objetos Atleta
            }
            else
            {
                return new List<EntAtleta>();
            }
        }

        private List<EntAtleta> Popular(SqlDataReader dtrDados)
        {
            List<EntAtleta> listEntReturn = new List<EntAtleta>();
            EntAtleta entReturn;

            try
            {
                while (dtrDados.Read()) //Faz o while com a quantidade de linhas retornadas do banco
                {
                    entReturn = new EntAtleta();

                    entReturn.IdAtleta = (int) dtrDados["PK_ATLETA"]; //Alimenta os dados
                    entReturn.Modalidade.IdModalidade = (int) dtrDados["FK_MODALIDADE"];
                    entReturn.Modalidade.Modalidade = dtrDados["TX_MODALIDADE"].ToString();
                    entReturn.Nome = dtrDados["TX_NOME"].ToString();
                    entReturn.Pais.IdPais = (int) dtrDados["FK_PAIS"];
                    entReturn.Pais.Nome = dtrDados["TX_NOME_PAIS"].ToString();
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

        public EntAtleta ObterPorId(Int32 IdAtleta, SqlCommand db)
        {
            SqlCommand scCommand = new SqlCommand("STP_AtletaSelecionarPorId", db.Connection);
            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@IdAtleta", SqlDbType.Int).Value = IdAtleta;

            SqlDataReader reader = scCommand.ExecuteReader();

            if (reader.FieldCount > 0)
            {
                return this.Popular(reader)[0]; //Pega somente a primeira linha de retorno
            }
            else
            {
                return new EntAtleta();
            }
        }
    }
}
