using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MedalhasOlimpiadas.Models
{
    public class BllBase
    {
        public static SqlConnection db;

        public BllBase()
        {
            try
            {
                string strConn = "Server=tcp:gabriel.database.windows.net,1433;Initial Catalog=medalhas;Persist Security Info=False;User ID=gapereira;Password=!medalhas123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                db = new SqlConnection(strConn);
                db.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
