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
    public class DalMedalha
    {
        public void Inserir(EntMedalha objMedalha, SqlCommand db)
        {
            SqlCommand scCommand = new SqlCommand("STP_MedalhaInserir", db.Connection);
            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@PK_MEDALHA", SqlDbType.Int).Value = objMedalha.IdMedalha;
            scCommand.Parameters.Add("@FK_TIPO_MEDALHA", SqlDbType.Int).Value = objMedalha.TipoMedalha.IdTipoMedalha;
            scCommand.Parameters.Add("@FK_ATLETA", SqlDbType.Int).Value = objMedalha.Atleta.IdAtleta;
            scCommand.Parameters.Add("@FL_ATIVO", SqlDbType.Bit).Value = objMedalha.Ativo;

            scCommand.ExecuteNonQuery();
        }

        public void Alterar(EntMedalha objMedalha, SqlCommand db)
        {
            SqlCommand scCommand = new SqlCommand("STP_MedalhaAlterar", db.Connection);
            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@PK_MEDALHA", SqlDbType.Int).Value = objMedalha.IdMedalha;
            scCommand.Parameters.Add("@FK_TIPO_MEDALHA", SqlDbType.Int).Value = objMedalha.TipoMedalha.IdTipoMedalha;
            scCommand.Parameters.Add("@FK_ATLETA", SqlDbType.Int).Value = objMedalha.Atleta.IdAtleta;
            scCommand.Parameters.Add("@FL_ATIVO", SqlDbType.Bit).Value = objMedalha.Ativo;

            scCommand.ExecuteNonQuery();
        }

        public void Remover(EntMedalha objMedalha, SqlCommand db)
        {
            SqlCommand scCommand = new SqlCommand("STP_MedalhaRemover", db.Connection);
            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@PK_MEDALHA", SqlDbType.Int).Value = objMedalha.IdMedalha;
            scCommand.Parameters.Add("@FL_ATIVO", SqlDbType.Bit).Value = objMedalha.Ativo;

            scCommand.ExecuteNonQuery();
        }

        public List<EntMedalha> ObterTodos(Int32 IdTipoMedalha, Int32 IdAtleta, Int32 Status, SqlCommand db)
        {
            SqlCommand scCommand = new SqlCommand("STP_MedalhaSelecionarTodos", db.Connection);
            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@IdTipoMedalha", SqlDbType.Int).Value = Utils.ToIntNull(IdTipoMedalha);
            scCommand.Parameters.Add("@IdAtleta", SqlDbType.Int).Value = Utils.ToIntNull(IdAtleta);
            scCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = Utils.ToBooleanNull(Status);

            SqlDataReader reader = scCommand.ExecuteReader();

            if (reader.FieldCount > 0)
            {
                return this.Popular(reader);
            }
            else
            {
                return new List<EntMedalha>();
            }
        }

        public List<EntMedalha> ObterTodosPorMedalha(Int32 IdModalidade, Int32 IsMulher, SqlCommand db)
        {
            SqlCommand scCommand = new SqlCommand("STP_MedalhaSelecionarTodosPorMedalha", db.Connection);
            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@IdModalidade", SqlDbType.Int).Value = Utils.ToIntNull(IdModalidade);
            scCommand.Parameters.Add("@IsMulher", SqlDbType.Bit).Value = Utils.ToBooleanNull(IsMulher);

            SqlDataReader reader = scCommand.ExecuteReader();

            if (reader.FieldCount > 0)
            {
                return this.PopularTipoMedalhaPorMedalha(reader);
            }
            else
            {
                return new List<EntMedalha>();
            }
        }

        public List<EntMedalha> ObterTodosPorPais(Int32 IdPais, Int32 Status, SqlCommand db)
        {
            SqlCommand scCommand = new SqlCommand("STP_MedalhaSelecionarTodosPorPais", db.Connection);
            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@IdPais", SqlDbType.Int).Value = IdPais;
            scCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = Utils.ToBooleanNull(Status);

            SqlDataReader reader = scCommand.ExecuteReader();

            if (reader.FieldCount > 0)
            {
                return this.PopularTipoMedalhaPorPais(reader);
            }
            else
            {
                return new List<EntMedalha>();
            }
        }

        private List<EntMedalha> Popular(DbDataReader dtrDados)
        {
            List<EntMedalha> listEntReturn = new List<EntMedalha>();
            EntMedalha entReturn;

            try
            {
                while (dtrDados.Read())
                {
                    entReturn = new EntMedalha();

                    entReturn.IdMedalha = (int) dtrDados["PK_MEDALHA"];
                    entReturn.TipoMedalha.IdTipoMedalha = (int) dtrDados["FK_TIPO_MEDALHA"];
                    entReturn.TipoMedalha.TipoMedalha = dtrDados["TX_TIPO_MEDALHA"].ToString();
                    entReturn.Atleta.IdAtleta = (int) dtrDados["FK_ATLETA"];
                    entReturn.Atleta.Nome = dtrDados["TX_NOME"].ToString();
                    entReturn.Atleta.Pais.IdPais = (int) dtrDados["FK_PAIS"];
                    entReturn.Atleta.Pais.Nome = dtrDados["TX_NOME_PAIS"].ToString();
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

        private List<EntMedalha> PopularTipoMedalhaPorMedalha(DbDataReader dtrDados)
        {
            List<EntMedalha> listEntReturn = new List<EntMedalha>();
            EntMedalha entReturn;

            Int32 i = 1;
            try
            {
                while (dtrDados.Read())
                {
                    entReturn = new EntMedalha();

                    entReturn.Atleta.Pais.IdPais = (int)dtrDados["PK_PAIS"];
                    entReturn.Atleta.Pais.Nome = dtrDados["TX_NOME_PAIS"].ToString();
                    entReturn.quantidadeOuro = (int)dtrDados["QUANTIDADE_OURO"];
                    entReturn.quantidadePrata = (int)dtrDados["QUANTIDADE_PRATA"];
                    entReturn.quantidadeBronze = (int)dtrDados["QUANTIDADE_BRONZE"];
                    entReturn.posicaoQuadro = i;
                        
                    listEntReturn.Add(entReturn);

                    i++;
                }

                dtrDados.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listEntReturn;
        }

        private List<EntMedalha> PopularTipoMedalhaPorPais(DbDataReader dtrDados)
        {
            List<EntMedalha> listEntReturn = new List<EntMedalha>();
            EntMedalha entReturn;

            Int32 i = 1;
            try
            {
                while (dtrDados.Read())
                {
                    entReturn = new EntMedalha();

                    entReturn.Atleta.Modalidade.IdModalidade = (int)dtrDados["PK_MODALIDADE"];
                    entReturn.Atleta.Modalidade.Modalidade = dtrDados["TX_MODALIDADE"].ToString();
                    entReturn.quantidadeOuro = (int)dtrDados["QUANTIDADE_OURO"];
                    entReturn.quantidadePrata = (int)dtrDados["QUANTIDADE_PRATA"];
                    entReturn.quantidadeBronze = (int)dtrDados["QUANTIDADE_BRONZE"];
                    entReturn.posicaoQuadro = i;

                    listEntReturn.Add(entReturn);

                    i++;
                }

                dtrDados.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listEntReturn;
        }


        public EntMedalha ObterPorId(Int32 IdMedalha, SqlCommand db)
        {
            SqlCommand scCommand = new SqlCommand("STP_MedalhaSelecionarPorId", db.Connection);
            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@IdMedalha", SqlDbType.Int).Value = IdMedalha;

            SqlDataReader reader = scCommand.ExecuteReader();

            if (reader.FieldCount > 0)
            {
                return this.Popular(reader)[0];
            }
            else
            {
                return new EntMedalha();
            }

        }
    }
}
