using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MedalhasOlimpiadas.Models
{
    public class BllMedalha : BllBase
    {
        private DalMedalha dalMedalha = new DalMedalha();

        public EntMedalha Inserir(EntMedalha objMedalha)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;

            try
            {
                dalMedalha.Inserir(objMedalha, cmd);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                db.Close();
            }

            return objMedalha;
        }

        public void Alterar(EntMedalha objMedalha)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;

            try
            {
                dalMedalha.Alterar(objMedalha, cmd);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        public EntMedalha Remover(EntMedalha objMedalha)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;

            try
            {
                objMedalha.Ativo = !objMedalha.Ativo;
                dalMedalha.Remover(objMedalha, cmd);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                db.Close();
            }

            return objMedalha;
        }

        public List<EntMedalha> ObterTodos(Int32 IdTipoMedalha, Int32 IdAtleta, Int32 Status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;

            List<EntMedalha> lstMedalha = new List<EntMedalha>();

            try
            {
                lstMedalha = dalMedalha.ObterTodos(IdTipoMedalha, IdAtleta, Status, cmd);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                db.Close();
            }

            return lstMedalha;
        }

        public List<EntMedalha> ObterTodosPorMedalha(Int32 IdModalidade, Int32 IsMulher)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;

            List<EntMedalha> lstMedalha = new List<EntMedalha>();

            try
            {
                lstMedalha = dalMedalha.ObterTodosPorMedalha(IdModalidade, IsMulher, cmd);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                db.Close();
            }

            return lstMedalha;
        }

        public List<EntMedalha> ObterTodosPorPais(Int32 IdPais, Int32 Status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;

            List<EntMedalha> lstMedalha = new List<EntMedalha>();

            try
            {
                lstMedalha = dalMedalha.ObterTodosPorPais(IdPais, Status, cmd);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                db.Close();
            }

            return lstMedalha;
        }


        public EntMedalha ObterPorId(Int32 IdMedalha)
        {
            EntMedalha objMedalha = new EntMedalha();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;

            try
            {
                objMedalha = dalMedalha.ObterPorId(IdMedalha, cmd);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                db.Close();
            }

            return objMedalha;
        }

    }
}
