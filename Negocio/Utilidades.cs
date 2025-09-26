using System.Collections.Generic;
using System;
using System.Data;
using System.IO;

namespace Negocio
{
    public class Utilidades
    {

        public static bool sendMail(string subject, string body, int lista)
        {
            try
            {
              


                Dictionary<string, object> par = new Dictionary<string, object>() { { "Code", lista } };
                
                DataTable dt = AccesoLogica.SP_SQL("SP_Obtener_Lista_Distribucion", "DESPACHO", par);
                List<string> listMail = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    listMail.Add(Convert.ToString(row["E_Mail"]));
                }


                var mailService = new Datos.MailServices.SystemSupportMail();
                mailService.sendMail(subject: subject, body: body, recipientMail: listMail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }

}
