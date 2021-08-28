using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MedalhasOlimpiadas.Models
{
    public class BllModalidade : BllBase
    {
        private DalModalidade dalModalidade = new DalModalidade();

        public List<EntModalidade> ObterTodos(String Modalidade, Int32 IsMulher, Int32 Status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;

            List<EntModalidade> lstModalidade = new List<EntModalidade>();

            try
            {
                lstModalidade = dalModalidade.ObterTodos(Modalidade, IsMulher, Status, cmd);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                db.Close();
            }

            return lstModalidade;
        }

        public EntModalidade ObterPorId(Int32 IdModalidade)
        {
            EntModalidade objModalidade = new EntModalidade();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;

            try
            {
                objModalidade = dalModalidade.ObterPorId(IdModalidade, cmd);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                db.Close();
            }

            return objModalidade;
        }

    }
}
