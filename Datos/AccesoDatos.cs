using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Datos
{
    public class AccesoDatos
    {
        #region SQL

        public static DataTable TEXT_SQL(string CompanyDB, string query)
        {

            SqlCommand _comando = MetodosDatos.CrearComando_SQL(CompanyDB);
            _comando.CommandText = query;
            return MetodosDatos.EjecutarComandoSelect_SQL(_comando);
        }

        public static DataTable SP_SQL(string sp, string CompanyDB, Dictionary<string, object> @params = null)
        {
            SqlCommand _comando = MetodosDatos.CrearComandoProc_SQL(sp, CompanyDB, @params);
            return MetodosDatos.EjecutarComandoSelect_SQL(_comando);
        }

        public static DataSet SP_SQL_WSP(string sp, string CompanyDB, Dictionary<string, object> @params = null)
        {
            SqlCommand _comando = MetodosDatos.CrearComandoProc_SQL(sp, CompanyDB, @params);
            return MetodosDatos.EjecutarComandoSelect_SQL_WSP(_comando);
        }

        public int SP_A_U_SQL(string sp, string CompanyDB, Dictionary<string, object> @params = null)
        {
            SqlCommand _comando = MetodosDatos.CrearComandoProc_SQL(sp, CompanyDB, @params);
            return MetodosDatos.EjecutarComandoInsert_SQL(_comando);
        }

        #endregion




    }


}
