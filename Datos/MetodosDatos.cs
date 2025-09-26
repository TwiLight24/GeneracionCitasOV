using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class MetodosDatos
    {

        #region   CONEXION SQL SERVER

        public static SqlCommand CrearComando_SQL(string CompanyDB)
        {
            string _cadenaConexion = Configuracion.CadenaConexion_sql(CompanyDB);
            SqlConnection _conexion = new SqlConnection();
            _conexion.ConnectionString = _cadenaConexion;
            SqlCommand _comando = new SqlCommand();
            _comando = _conexion.CreateCommand();
            _comando.CommandType = CommandType.Text;
            return _comando;

        }
        public static SqlCommand CrearComandoProc_SQL(string procedimiento, string CompanyDB, Dictionary<string, object> @params = null)
        {
            string _cadenaConexion = Configuracion.CadenaConexion_sql(CompanyDB);
            SqlConnection _conexion = new SqlConnection(_cadenaConexion);
            SqlCommand _comando = new SqlCommand(procedimiento, _conexion);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.CommandTimeout = 120;

            foreach (var p in @params)
            {
                System.Data.IDbDataParameter par = _comando.CreateParameter();
                par.ParameterName = p.Key;
                par.Value = p.Value;
                _comando.Parameters.Add(par);
            }


            return _comando;

        }
        public static int EjecutarComandoInsert_SQL(SqlCommand comando)
        {
            try
            {
                comando.Connection.Open();
                return comando.ExecuteNonQuery();
            }
            catch { throw; }
            finally
            {
                comando.Connection.Dispose();
                comando.Connection.Close();
            }
        }
        public static int EjecutarComandoInsert_SQL_r(SqlCommand comando)
        {
            try
            {
                comando.Connection.Open();
                return Convert.ToInt32(comando.ExecuteReader());

                //return comando.ExecuteNonQuery();
            }
            catch { throw; }
            finally
            {
                comando.Connection.Dispose();
                comando.Connection.Close();
            }
        }
        public static DataTable EjecutarComandoSelect_SQL(SqlCommand comando)
        {


            DataTable _tabla = new DataTable();
            try
            {
                comando.Connection.Open();
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                adaptador.Fill(_tabla);


                // SqlDataReader reader = comando.ExecuteReader();
                //_tabla.Load(reader);

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                comando.Connection.Close();
            }
            return _tabla;


        }


        public static DataSet EjecutarComandoSelect_SQL_WSP(SqlCommand comando)
        {


            DataSet _tabla = new DataSet();
            try
            {
                comando.Connection.Open();
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                adaptador.Fill(_tabla);


                // SqlDataReader reader = comando.ExecuteReader();
                //_tabla.Load(reader);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                comando.Connection.Close();
            }
            return _tabla;


        }

        #endregion 


    }
}
