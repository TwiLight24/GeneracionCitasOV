namespace Datos.MailServices
{
    public class SystemSupportMail : MasterMailServer
    {
        public SystemSupportMail()
        {
            senderMail = "sistemas@metalindustrias.com.pe";
            password = "WKNEV1e2";
            host = "mail.metalindustrias.com.pe";
            port = 25;
            ssl = false;
            initializeSmtpClient();
        }
    }
}
