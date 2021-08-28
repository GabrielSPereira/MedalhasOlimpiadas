using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MedalhasOlimpiadas.Models
{
    public class BllAtleta : BllBase
    {
        private DalAtleta dalAtleta = new DalAtleta();

        public EntAtleta Inserir(EntAtleta objAtleta)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;

            try
            {
                dalAtleta.Inserir(objAtleta, cmd);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                db.Close();
            }

            return objAtleta;
        }

        public void Alterar(EntAtleta objAtleta)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;

            try
            {
                dalAtleta.Alterar(objAtleta, cmd);
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

        public EntAtleta Remover(EntAtleta objAtleta)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;

            try
            {
                objAtleta.Ativo = !objAtleta.Ativo;
                dalAtleta.Remover(objAtleta, cmd);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                db.Close();
            }

            return objAtleta;
        }

        public List<EntAtleta> ObterTodos(Int32 IdModalidade, Int32 IdPais, String Nome, Int32 Status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;

            List<EntAtleta> lstAtleta = new List<EntAtleta>();

            try
            {
                lstAtleta = dalAtleta.ObterTodos(IdModalidade, IdPais, Nome, Status, cmd);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                db.Close();
            }

            return lstAtleta;
        }

        public EntAtleta ObterPorId(Int32 IdAtleta)
        {
            EntAtleta objAtleta = new EntAtleta();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;

            try
            {
                objAtleta = dalAtleta.ObterPorId(IdAtleta, cmd);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                db.Close();
            }

            return objAtleta;
        }

    }
}
