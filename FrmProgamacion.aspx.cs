using Negocio;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Despachos
{
    public partial class FrmProgamacion : System.Web.UI.Page
    {
        public enum MessageType { Success, Error, Info, Warning };
        DateTime FechaInicio;
        DateTime FechaFin;
        DateTime FechaAlerta;
        string tipo;
        int contadorMsgWsp = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string lote = Request.QueryString["Lote"];
            string fechaalerta = Request.QueryString["fechaalerta"];

            AlertError.Visible = false;
            AlertErrorFecha.Visible = false;
            DivMsg.Visible = false;
            lblFA.Text = fechaalerta;
            DivFecHr.Visible = false;

            if (!Page.IsPostBack)
            {
                ClientScript.RegisterStartupScript(GetType(), "key", "launchModal();", true);
                Dictionary<string, object> _par2 = new Dictionary<string, object>() { };

                DataTable dtRango = AccesoLogica.SP_SQL("SP_Obtener_Cab_Horario", "DESPACHORumi", _par2);
                if (dtRango.Rows.Count > 0)
                {
                    lblFI.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    lblFF.Text = dtRango.Rows[0]["FF"].ToString();
                }
                else
                {
                    AlertError.Visible = true;
                    LblError.Text = "Horario no disponible.";
                    return;
                }


                Dictionary<string, object> _par3 = new Dictionary<string, object>() { { "@lote", lote }, { "@fecha", "" }, { "@Estado", "" } };
                DataTable dtProgramacion = AccesoLogica.SP_SQL("SP_Consultar_Programacion", "DESPACHORumi", _par3);

                if (dtProgramacion.Rows.Count > 0)
                {
                    lblLote.Text = lote;
                    lblPeso.Text = dtProgramacion.Rows[0]["Peso"].ToString();
                    lblTiempo.Text = dtProgramacion.Rows[0]["MinEstimados"].ToString();
                    lblFecha.Text = dtProgramacion.Rows[0]["Fecha"].ToString();
                    lblHora.Text = dtProgramacion.Rows[0]["HI"].ToString();
                    txtConductor.Text = dtProgramacion.Rows[0]["Conductor"].ToString();
                    txtLicencia.Text = dtProgramacion.Rows[0]["Licencia"].ToString();
                    txtObservaciones.Text = dtProgramacion.Rows[0]["Observaciones"].ToString();
                    txtPlaca.Text = dtProgramacion.Rows[0]["Placa"].ToString();
                    lblIdProgramacion.Text = dtProgramacion.Rows[0]["IdProgramacion"].ToString();
                    string estado = dtProgramacion.Rows[0]["Estado"].ToString();

                    string fechaActual = DateTime.Now.ToString("dd/MM/yyyy");
                    datepicker.SelectedDate = Convert.ToDateTime(fechaActual);

                    DivAlerta.Visible = true;
                    datepicker.Enabled = false;
                    BtnConfirmar.Visible = false;
                    SetDisponibilidad(fechaActual);

                    if (estado == "P")
                    {
                        lblAlertHeader.Text = "Despacho Programado";
                        lblSuccess.Text = "Estimado cliente su despacho fue programado para el día " + lblFecha.Text + " a las " + lblHora.Text + " hrs.";

                        BtnCancelar.Visible = true;
                        BtnConfirmar.Visible = true;
                        BtnConfirmar.Text = "Reprogramar";

                        datepicker.Enabled = true;
                        lblHora.Text = "";
                    }

                    if (estado == "E")
                    {
                        lblAlertHeader.Text = "Despacho Atendido";
                        lblSuccess.Text = "Estimado cliente su despacho fue atendido.";
                    }

                    if (estado == "C")
                    {
                        lblAlertHeader.Text = "Despacho Cancelado";
                        lblSuccess.Text = "Estimado cliente su despacho ha sido Cancelado.";
                    }

                    lblHeaderFoot.Text = "Cualquier duda, consulta solicitar soporte al área de Sistemas: procesosti@metalindustrias.com.pe";//contáctese con Administración de ventas.
                }
                else
                {
                    Inicializar(lote);
                    lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    SetDisponibilidad(lblFecha.Text);
                }
            }

            SetHora();

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            lblFecha.Text = datepicker.SelectedDate.ToShortDateString();
            SetDisponibilidad(lblFecha.Text);
            SetHora();
        }

        protected void Inicializar(string lote)
        {
            Dictionary<string, object> _par1 = new Dictionary<string, object>() { };
            string disponible = AccesoLogica.SP_SQL("SP_Validar_Horario_Atencion", "DESPACHORumi", _par1).Rows[0][0].ToString();

            if (disponible == "0")
            {
                DivAlerta.Visible = true;
                datepicker.Enabled = false;
                BtnConfirmar.Visible = false;

                lblAlertHeader.Text = "Programación No Disponible";
                lblSuccess.Text = "Estimado cliente en estos momentos no podemos programar su Despacho. Recuerde que la programación esta disponible de Lunes a Viernes de 09:00-17:00 y Sábados 09:00-12:00";
                return;
            }
            else
            {
                Dictionary<string, object> _par = new Dictionary<string, object>() { { "@lote", lote } };
                DataTable dtProgramacion = AccesoLogica.SP_SQL("SP_Calcular_Tiempo_Despacho", "DESPACHORumi", _par);

                lblLote.Text = lote;
                lblIdProgramacion.Text = "0";
                lblPeso.Text = dtProgramacion.Rows[0][0].ToString();
                lblTiempo.Text = dtProgramacion.Rows[0][1].ToString();
                DivAlerta.Visible = false;
            }
        }

        protected void SetDisponibilidad(string fecha)
        {
            DataTable dt_Disponiblidad;

            List<Tuple<RadioButton, HtmlGenericControl>> listRadio = new List<Tuple<RadioButton, HtmlGenericControl>>();
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio1, lblradio1));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio2, lblradio2));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio3, lblradio3));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio4, lblradio4));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio5, lblradio5));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio6, lblradio6));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio7, lblradio7));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio8, lblradio8));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio9, lblradio9));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio10, lblradio10));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio11, lblradio11));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio12, lblradio12));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio13, lblradio13));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio14, lblradio14));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio15, lblradio15));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio16, lblradio16));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio17, lblradio17));

            Dictionary<string, object> _par = new Dictionary<string, object>()
                {
                         { "@fecha", fecha },
                        { "@lote", lblLote.Text },
                        { "@Estado", "O" }
                };

            dt_Disponiblidad = AccesoLogica.SP_SQL("SP_Obtener_Disp_horas", "DESPACHORumi", _par);

            if (dt_Disponiblidad.Rows.Count > 0)
            {
                foreach (var objPair in listRadio)
                {
                    objPair.Item1.Attributes.Remove("disabled");
                    objPair.Item2.Attributes.Remove("style");
                }

                foreach (DataRow row in dt_Disponiblidad.Rows)
                {
                    string hora = row["Hora"].ToString();
                    foreach (var objPair in listRadio)
                    {
                        if (hora == objPair.Item2.InnerText.ToString())
                        {
                            objPair.Item1.Attributes["disabled"] = "disabled";
                            objPair.Item2.Attributes["style"] = "background-color:#CCCCCC";
                        }
                    }
                }
            }
            else
            {
                foreach (var objPair in listRadio)
                {
                    objPair.Item1.Attributes.Remove("disabled");
                    objPair.Item2.Attributes.Remove("style");
                }
            }

        }

        protected void SetHora()
        {
            List<Tuple<RadioButton, HtmlGenericControl>> listRadio = new List<Tuple<RadioButton, HtmlGenericControl>>();
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio1, lblradio1));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio2, lblradio2));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio3, lblradio3));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio4, lblradio4));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio5, lblradio5));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio6, lblradio6));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio7, lblradio7));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio8, lblradio8));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio9, lblradio9));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio10, lblradio10));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio11, lblradio11));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio12, lblradio12));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio13, lblradio13));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio14, lblradio14));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio15, lblradio15));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio16, lblradio16));
            listRadio.Add(new Tuple<RadioButton, HtmlGenericControl>(btnradio17, lblradio17));

            foreach (Tuple<RadioButton, HtmlGenericControl> objPair in listRadio)
            {
                objPair.Item2.Attributes["class"] = "btn btn-outline-secondary m-lg-1" + (objPair.Item1.Checked ? " active" : "");
                objPair.Item2.Attributes["onclick"] = "javascript:setTimeout('__doPostBack(\\'" + objPair.Item1.ClientID + "\\',\\'\\')', 0);";
            }

            if (Page.IsPostBack)
            {
                if (btnradio1.Checked) lblHora.Text = lblradio1.InnerText.ToString();
                if (btnradio2.Checked) lblHora.Text = lblradio2.InnerText.ToString();
                if (btnradio3.Checked) lblHora.Text = lblradio3.InnerText.ToString();
                if (btnradio4.Checked) lblHora.Text = lblradio4.InnerText.ToString();
                if (btnradio5.Checked) lblHora.Text = lblradio5.InnerText.ToString();
                if (btnradio6.Checked) lblHora.Text = lblradio6.InnerText.ToString();
                if (btnradio7.Checked) lblHora.Text = lblradio7.InnerText.ToString();
                if (btnradio8.Checked) lblHora.Text = lblradio8.InnerText.ToString();
                if (btnradio9.Checked) lblHora.Text = lblradio9.InnerText.ToString();
                if (btnradio10.Checked) lblHora.Text = lblradio10.InnerText.ToString();
                if (btnradio11.Checked) lblHora.Text = lblradio11.InnerText.ToString();
                if (btnradio12.Checked) lblHora.Text = lblradio12.InnerText.ToString();
                if (btnradio13.Checked) lblHora.Text = lblradio13.InnerText.ToString();
                if (btnradio14.Checked) lblHora.Text = lblradio14.InnerText.ToString();
                if (btnradio15.Checked) lblHora.Text = lblradio15.InnerText.ToString();
                if (btnradio16.Checked) lblHora.Text = lblradio16.InnerText.ToString();
                if (btnradio17.Checked) lblHora.Text = lblradio17.InnerText.ToString();

                DivFecHr.Visible = true;
                lblFecHr.Text = "Fecha y hora seleccionada: " + lblFecha.Text + " a las " + lblHora.Text + " hrs.";
            }

        }

        protected void datepicker_SelectionChanged(object sender, EventArgs e)
        {
            lblFecha.Text = datepicker.SelectedDate.ToShortDateString();
            DivFecHr.Visible = true;
            lblFecHr.Text = "Fecha y hora seleccionada: " + lblFecha.Text + " a las " + lblHora.Text + " hrs.";
            SetDisponibilidad(lblFecha.Text);
        }

        protected void Cancelar_Despacho_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> _par3 = new Dictionary<string, object>()
                {
                    { "@lote",  lblLote.Text},
                    { "@fecha", lblFecha.Text },
                    { "@hi", lblHora.Text},
                    { "@Observaciones", txtObservaciones.Text},
                    { "@PlacaVehiculo", txtPlaca.Text},
                    { "@LicenciaConducir", txtLicencia.Text},
                    { "@Conductor", txtConductor.Text},
                    { "@IdReprogramacion", Convert.ToInt32(lblIdProgramacion.Text)},
                    { "@Accion", "Cancelar"}

                };

            try
            {
                int res = AccesoLogica.SP_A_U_SQL("SP_Programar_Despacho", "DESPACHORumi", _par3);
                if (res == 0)
                {
                    throw new Exception(res.ToString());
                }

                if (lblIdProgramacion.Text == "0")
                {
                    BtnConfirmar.Visible = false;
                    DivAlerta.Visible = true;
                    lblAlertHeader.Text = "Tu CANCELACIÓN de despacho se realizó con Éxito.";
                    tipo = "Cancelado";
                }
                else
                {
                    DivAlerta.Visible = true;
                    lblAlertHeader.Text = "Tu CANCELACIÓN de despacho se realizó con Éxito.";
                    tipo = "Cancelado";
                }

            }
            catch (Exception ex)
            {
                AlertError.Visible = true;
                LblError.Text = ex.Message.ToString();
                BtnConfirmar.Enabled = true;
                datepicker.Enabled = true;
                return;
            }
        }

        protected void Programar_Despacho_Click(object sender, EventArgs e)
        {
            try
            {
                String fechaTexto = lblFecha.Text;
                DateTime fechaLabel = DateTime.ParseExact(fechaTexto, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime fechaActual = DateTime.Today;

                if (fechaLabel < fechaActual)
                {
                    AlertError.Visible = true;

                    LblError.Text = "La fecha seleccionada para la cita no puede ser menor a la fecha actual. Fecha seleccionada: " + fechaLabel;
                    return;
                }

                Dictionary<string, object> _par3 = new Dictionary<string, object>()
                {
                    { "@lote",  lblLote.Text},
                    { "@fecha", lblFecha.Text },
                    { "@hi", lblHora.Text},
                    { "@Accion", "VerificarDisponibilidadHorario"}

                };

                DataTable dt_disponibilidad_horario = AccesoLogica.SP_SQL("SP_Programar_Despacho", "DESPACHORumi", _par3);
                if (dt_disponibilidad_horario.Rows.Count > 0)
                {
                    AlertError.Visible = true;
                    LblError.Text = "El horario en la fecha seleccionada no esta disponible, ya cuenta con dos programaciones!";
                    return;
                }


                Dictionary<string, object> _par2 = new Dictionary<string, object>()
                {
                    { "@lote",  lblLote.Text},
                    { "@fecha", lblFecha.Text },
                    { "@hi", lblHora.Text},
                    { "@Observaciones", txtObservaciones.Text},
                    { "@PlacaVehiculo", txtPlaca.Text},
                    { "@LicenciaConducir", txtLicencia.Text},
                    { "@Conductor", txtConductor.Text},
                    { "@IdReprogramacion", Convert.ToInt32(lblIdProgramacion.Text)},
                    { "@Accion", "ExisteProgramacion"}

                };

                DataTable dt_existe = AccesoLogica.SP_SQL("SP_Programar_Despacho", "DESPACHORumi", _par2);
                if (dt_existe.Rows.Count > 0)
                {
                    string sRes = dt_existe.Rows[0][0].ToString();
                    if (!string.IsNullOrEmpty(lblFecha.Text))
                    {
                        AlertError.Visible = true;

                        LblError.Text = sRes;
                        return;

                    }
                }


                if (string.IsNullOrEmpty(lblFecha.Text))
                {
                    AlertErrorFecha.Visible = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showContent();", true);
                    LblErrorFecha.Text = "Seleccione una fecha por favor.";
                    return;
                }

                if (string.IsNullOrEmpty(lblHora.Text))
                {
                    AlertError.Visible = true;
                    LblError.Text = "Seleccione una hora por favor.";
                    return;
                }


                BtnConfirmar.Enabled = false;
                datepicker.Enabled = false;

                Dictionary<string, object> _par = new Dictionary<string, object>()
                {
                    { "@lote",  lblLote.Text},
                    { "@fecha", lblFecha.Text },
                    { "@hi", lblHora.Text},
                    { "@Observaciones", txtObservaciones.Text},
                    { "@PlacaVehiculo", txtPlaca.Text},
                    { "@LicenciaConducir", txtLicencia.Text},
                    { "@Conductor", txtConductor.Text},
                    { "@IdReprogramacion", Convert.ToInt32(lblIdProgramacion.Text)},
                    { "@Accion", "Programar"}
                };


                int res = AccesoLogica.SP_A_U_SQL("SP_Programar_Despacho", "DESPACHORumi", _par);
                if (res == 0)
                {
                    throw new Exception(res.ToString());
                }


                if (lblIdProgramacion.Text == "0")
                {
                    Dictionary<string, object> wsp = new Dictionary<string, object>()
                    {
                        { "@Docnum", lblLote.Text }
                    };

                    DataSet dt_trazabilidad = AccesoLogica.SP_SQL_WSP("GMI_TRACKING_INFO_WSP", "SBO_RumiImport", wsp);
                    string ov = dt_trazabilidad.Tables[0].Rows[0]["OV"].ToString();
                    string cliente = dt_trazabilidad.Tables[0].Rows[0]["CLIENTE"].ToString();
                    string datosProgramacion = dt_trazabilidad.Tables[0].Rows[0]["FECHA"].ToString();

                    string numVendedor = dt_trazabilidad.Tables[0].Rows[0]["VENDEDOR_NUM"].ToString();
                    string numCliente1 = dt_trazabilidad.Tables[0].Rows[0]["CLIENTE_NUM1"].ToString();
                    string numCliente2 = dt_trazabilidad.Tables[0].Rows[0]["CLIENTE_NUM2"].ToString();
                    string numCliente3 = dt_trazabilidad.Tables[0].Rows[0]["CLIENTE_NUM3"].ToString();

                    numCliente1 = Regex.Replace(numCliente1, @"[^\d]", "");
                    numCliente2 = Regex.Replace(numCliente2, @"[^\d]", "");
                    numCliente3 = Regex.Replace(numCliente3, @"[^\d]", "");
                    string resultNum = NumeroValido(numCliente1, numCliente2, numCliente3);

                    int envio2 = Enviar_WSP(ov, cliente, datosProgramacion, resultNum, 1);
                    int envio1 = Enviar_WSP(ov, cliente, datosProgramacion, numVendedor, 2);

                    BtnConfirmar.Visible = false;
                    DivAlerta.Visible = true;
                    lblAlertHeader.Text = "Tu programación de despacho se realizó con Éxito.";
                    lblSuccess.Text = "Estimado cliente su despacho ha sido programado para el día " + lblFecha.Text + " a las " + lblHora.Text + " hrs. Acérquese a Rumi Import con el Nº de Orden " + lblLote.Text + " para la entrega.";
                    lblHeaderFoot.Text = "Cualquier duda, consulta, reprogramación contáctese con Administración de ventas.";
                    tipo = "Programado";
                }
                else
                {
                    DivAlerta.Visible = true;
                    lblAlertHeader.Text = "Tu Reprogramación de despacho se realizó con Éxito.";
                    lblSuccess.Text = "Estimado cliente su despacho ha sido reprogramado para el día " + lblFecha.Text + " a las " + lblHora.Text + " hrs. Acérquese a Rumi Import con el Nº de Orden " + lblLote.Text + " para la entrega.";
                    lblHeaderFoot.Text = "Cualquier duda, consulta, reprogramación contáctese con Administración de ventas.";
                    tipo = "Reprogramado";
                }

            }
            catch (Exception ex)
            {
                AlertError.Visible = true;
                LblError.Text = ex.Message.ToString();
                BtnConfirmar.Enabled = true;
                datepicker.Enabled = true;
                return;
            }
        }

        protected void datepicker_DayRender(object sender, DayRenderEventArgs e)
        {
            try
            {
                FechaInicio = Convert.ToDateTime(lblFI.Text);
                FechaFin = Convert.ToDateTime(lblFF.Text);

                if (lblFA.Text == lblFI.Text)
                {
                    if (e.Day.Date < FechaInicio.AddDays(1) || e.Day.Date > FechaFin)
                        e.Day.IsSelectable = false;
                }
                else
                {
                    if (e.Day.Date < FechaInicio || e.Day.Date > FechaFin)
                        e.Day.IsSelectable = false;
                }
            }
            catch
            {
                datepicker.Enabled = false;
                AlertError.Visible = true;
                LblError.Text = "Horario no disponible";
            }
        }

        private void EnviarMail(string tipo)
        {
            string enlace = HttpContext.Current.Request.Url.AbsoluteUri;
            //string textbody;
            //textbody =
            //"<br/>" + "Mediante el presente, INDUZINC S.A. confirma el registro de su despacho, generado de acuerdo al siguiene detalle:" + "\r\n" +
            ////"<br/>" + "Cliente: " + "" + "\r\n" +
            //"<br/>" + "Lote: " + lblLote.Text + "\r\n" +
            //"<br/>" + "Fecha: " + lblFecha.Text + "\r\n" +
            //"<br/>" + "Hora:  " + lblHora.Text + "\r\n" +
            //"<br/>" + "Conductor:  " + txtConductor.Text + "\r\n" +
            //"<br/>" + "Licencia:  " + txtLicencia.Text+ "\r\n" +
            //"<br/>" + "Placa:  " + txtPlaca.Text + "\r\n" +
            //"<br/>" + "Observaciones:  " + txtObservaciones.Text + "\r\n" + 
            //"\r\n" +
            //"\r\n" +
            //"\r\n" +
            //"<br/>" + "Reprogramacion:  " + "<a href=" + enlace + ">Ver enlace</a>" + "\r\n";


            string linkReprogramacion = "<br/>" + "Reprogramacion:  " + "<a href=" + enlace + ">Clic Aquí</a>";

            DataTable dt_body = AccesoLogica.TEXT_SQL("DESPACHORumi", "SELECT body FROM Cab_Lista_Distribucion WHERE Code = 1");
            string textbody = dt_body.Rows[0]["body"].ToString();

            string[] paramts = new string[8] { lblLote.Text, lblFecha.Text, lblHora.Text, txtConductor.Text, txtLicencia.Text, txtPlaca.Text, txtObservaciones.Text, linkReprogramacion };
            string str4 = "";

            str4 = str4 + textbody;
            if (paramts != null)
            {
                for (int index = 0; index < paramts.Length; ++index)
                    str4 = str4.Replace("${param" + (index + 1).ToString() + "}", paramts[index]);
            }


            Utilidades.sendMail("ENVIO AUTOMATICO - Despacho " + tipo + " -  Lote: " + lblLote.Text, str4, 1);
        }

        protected void btn_retornar_Click(object sender, EventArgs e)
        {
            //Response.Redirect("http://localhost:18360/FrmLote.aspx");
            Response.Redirect("http://192.168.1.88:8105/frmlote.aspx");
        }

        public string NumeroValido(string phone1, string phone2, string phone3)
        {
            if (phone1.Length == 9 && phone1.StartsWith("9"))
            {
                return phone1;
            }
            else if (phone2.Length == 9 && phone2.StartsWith("9"))
            {
                return phone2;
            }
            else if (phone3.Length == 9 && phone3.StartsWith("9"))
            {
                return phone3;
            }
            return "0";
        }

        public int Enviar_WSP(string ov, string cliente, string datosProgramacion, string num, int tipo)
        {
            try
            {

                if (num != "0")
                {
                    if (tipo == 1)
                    {
                        byte[] AsBytes = File.ReadAllBytes(@"\\SRV_FE_02\ChatBot\Rumi.jpg");
                        String AsBase64String = Convert.ToBase64String(AsBytes);
                        var url = "https://api.ultramsg.com/instance68166/messages/image";
                        ///////////
                        var client =
                            new RestClient(url);
                        var request = new RestRequest(url, Method.Post);
                        request.AddHeader("content-type", "application/x-www-form-urlencoded");
                        request.AddParameter("token", "8jti991f4jf3uvmn", ParameterType.GetOrPost);
                        request.AddParameter("to", "51" + num, ParameterType.GetOrPost);
                        request.AddParameter("image", AsBase64String, ParameterType.GetOrPost);
                        request.AddParameter("caption", "Estimado Cliente: *" + cliente + "*, " +
                            "\n\n¡Hemos agendado exitosamente tu cita para el recojo del pedido *N° " + ov + "*! " +
                            "\n*Fecha y hora*: " + datosProgramacion +
                            "\n*Dirección*: Ca. Omicron 128 – Parque Industrial – Callao " +
                            "\n\nRecuerda que para iniciar tu atención debes identificarte con el número de pedido en nuestra recepción, te recomendamos llegar con 10 minutos de anticipación. " +
                            "\n\nAgradecemos su preferencia y quedamos a su disposición para cualquier consulta adicional. " +
                            "\n\nAtentamente, *RUMI IMPORT SA*", ParameterType.GetOrPost);
                        RestResponse response = client.Execute(request);
                    }
                    else if (tipo == 2)
                    {
                        byte[] AsBytes = File.ReadAllBytes(@"\\SRV_FE_02\ChatBot\Rumi.jpg");
                        String AsBase64String = Convert.ToBase64String(AsBytes);
                        var url = "https://api.ultramsg.com/instance68166/messages/image";
                        ///////////
                        var client =
                            new RestClient(url);
                        var request = new RestRequest(url, Method.Post);
                        request.AddHeader("content-type", "application/x-www-form-urlencoded");
                        request.AddParameter("token", "8jti991f4jf3uvmn", ParameterType.GetOrPost);
                        request.AddParameter("to", "51" + num, ParameterType.GetOrPost);
                        request.AddParameter("image", AsBase64String, ParameterType.GetOrPost);
                        request.AddParameter("caption", "Estimado Vendedor del cliente: *" + cliente + "*, " +
                            "\n\n¡Hemos agendado exitosamente la cita para el recojo del pedido *N° " + ov + "*! " +
                            "\n*Fecha y hora*: " + datosProgramacion +
                            "\n*Dirección*: Ca. Omicron 128 – Parque Industrial – Callao " +
                            "\n\nRecuerda indicarle al cliente que para iniciar su atención debes identificarte con el número de pedido en nuestra recepción, así mismo recomendamos llegar con 10 minutos de anticipación. " +
                            "\n\nAgradecemos su preferencia y quedamos a su disposición para cualquier consulta adicional. " +
                            "\n\nAtentamente, *RUMI IMPORT SA*", ParameterType.GetOrPost);
                        RestResponse response = client.Execute(request);
                    }

                }
                contadorMsgWsp = 0;
                return 1;
            }
            catch (Exception)
            {
                //Console.WriteLine($"Error: {ex.Message}");
                if (contadorMsgWsp < 3)
                {
                    contadorMsgWsp = contadorMsgWsp + 1;
                    return Enviar_WSP(ov, cliente, datosProgramacion, num, tipo);
                }
                else
                {
                    return 0;
                }

            }
        }
    }
}