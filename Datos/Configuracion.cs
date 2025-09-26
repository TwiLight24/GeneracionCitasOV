namespace Datos
{
    public class Configuracion
    {

        public static string CadenaConexion_sql(string CompanyDB)
        {


            string cadenaConexion_sql = @"Data Source= 192.168.1.2" +
                    " ;Initial Catalog= " + CompanyDB +
                    " ;User ID= sa" +
                    " ;Password= B1Admin ;Connect Timeout= 200 ";
            return cadenaConexion_sql;

            
            //    //string cadenaConexion_sql = @"Data Source= 192.168.1.2"  +
            //    string cadenaConexion_sql = @"Data Source= 192.168.1.2" +
            //        " ;Initial Catalog= " + CompanyDB +
            //        " ;User ID= sa" +
            //        " ;Password= B1Admin ;Connect Timeout= 200 ";
            //    return cadenaConexion_sql;






        }

    }
}
