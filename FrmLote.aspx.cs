using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Despachos
{
    public partial class FrmLote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                Dictionary<string, object> _par = new Dictionary<string, object>() 
                { 
                    { "@RUC", txtlote.Text },
                    { "@Filtro", "Pendientes"}

                };


                DataTable dt_trazabilidad = AccesoLogica.SP_SQL("GMI_TRACKING_LOTES", "SBO_RumiImport", _par);
                
                

                if (dt_trazabilidad.Rows.Count > 0)
                {
                    gv_trazabilidad.DataSource = dt_trazabilidad;
                    gv_trazabilidad.DataBind();
                    AlertaInf.Visible = false;

                }
                else
                {
                    dt_trazabilidad.Rows.Clear();
                    dt_trazabilidad.Columns.Clear();
                    gv_trazabilidad.DataBind();
                    AlertaInf.Visible = true;
                    return;
                }

                //string sql = "SELECT U_GMI_Lote FROM[SBO_INDUZINC].dbo.[@GMI_MASTER]  where U_GMI_Lote =" + "'" + txtlote.Text + "'";
                //DataTable dt = AccesoLogica.TEXT_SQL("DESPACHO", sql);



                //if (dt.Rows.Count == 0)
                //{
                //    Alerta.Visible = true;
                //   return;

                //}
                //else
                //{
                //    Response.Redirect("http://192.168.1.88:8105/FrmProgamacion.aspx?Lote=" + txtlote.Text + "&fechaalerta=" + DateTime.Now.ToString("dd/MM/yyyy"));

                //}
            }


        }

        protected void gv_trazabilidad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}