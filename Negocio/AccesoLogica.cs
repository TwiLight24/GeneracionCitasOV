using System.Collections.Generic;
using System.Data;
using Datos;


namespace Negocio
{
    public class AccesoLogica
    {
        #region MSSQL

        public static DataTable TEXT_SQL(string CompanyDB, string query)
        {
            return AccesoDatos.TEXT_SQL(CompanyDB, query);
        }

        public static DataTable SP_SQL(string sp, string CompanyDB, Dictionary<string, object> @params = null)
        {
            return AccesoDatos.SP_SQL(sp, CompanyDB, @params);
        }

        public static DataSet SP_SQL_WSP(string sp, string CompanyDB, Dictionary<string, object> @params = null)
        {
            return AccesoDatos.SP_SQL_WSP(sp, CompanyDB, @params);
        }

        public static int SP_A_U_SQL(string sp, string CompanyDB, Dictionary<string, object> @params = null)
        {
            AccesoDatos acceso = new AccesoDatos();
            return acceso.SP_A_U_SQL(sp, CompanyDB, @params);
        }

        #endregion


    }
}
