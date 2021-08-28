using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MedalhasOlimpiadas.Models
{
    public class BllTipoMedalha : BllBase
    {
        private DalTipoMedalha dalTipoMedalha = new DalTipoMedalha();

        public List<EntTipoMedalha> ObterTodos(Int32 Status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;

            List<EntTipoMedalha> lstTipoMedalha = new List<EntTipoMedalha>();

            try
            {
                lstTipoMedalha = dalTipoMedalha.ObterTodos(Status, cmd);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                db.Close();
            }

            return lstTipoMedalha;
        }

        public EntTipoMedalha ObterPorId(Int32 IdTipoMedalha)
        {
            EntTipoMedalha objTipoMedalha = new EntTipoMedalha();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;

            try
            {
                objTipoMedalha = dalTipoMedalha.ObterPorId(IdTipoMedalha, cmd);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                db.Close();
            }

            return objTipoMedalha;
        }

    }
}
