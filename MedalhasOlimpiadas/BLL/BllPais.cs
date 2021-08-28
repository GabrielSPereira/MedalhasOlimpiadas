using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MedalhasOlimpiadas.Models
{
    public class BllPais : BllBase
    {
        private DalPais dalPais = new DalPais();

        public List<EntPais> ObterTodos(String Nome, Int32 Status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;

            List<EntPais> lstPais = new List<EntPais>();

            try
            {
                lstPais = dalPais.ObterTodos(Nome, Status, cmd);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                db.Close();
            }

            return lstPais;
        }

        public EntPais ObterPorId(Int32 IdPais)
        {
            EntPais objPais = new EntPais();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;

            try
            {
                objPais = dalPais.ObterPorId(IdPais, cmd);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                db.Close();
            }

            return objPais;
        }

    }
}
