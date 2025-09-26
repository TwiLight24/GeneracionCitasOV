<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmLote.aspx.cs" Inherits="Despachos.FrmLote" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SistemadeCitas - Programacion de despachos</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <meta http-equiv="content-type" content="text/html; charset=UTF-8">
   
    <link  href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-+0n0xVW2eSR5OomGNYDnhzAbDsOXxcvSN1TPprVMTNDbiYZCxYbOOl7+AMvyTG2x" crossorigin="anonymous">
   
    <script src = "https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"> </script>

    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.5.0/font/bootstrap-icons.css">
      <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>  
 <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/js/bootstrap.min.js" integrity="sha384-Atwg2Pkwv9vp0ygtn1JAojH0nYbwNJLPhwyoVbhoPwBhjQPR5VtM2+xf0Uwh9KtT" crossorigin="anonymous"></script>
 
</head>
<body>
    <header class="card mb-4 rounded-6 shadow-sm border-secondary" style="display: flex;">
        <div style="background-color: black; display: flex; align-items: center; ">
            <img src="rumi-logosolo.png" style="width: 5%; border: none; height: auto;">
            <h2 class="bi-card-text" aria-atomic="False" aria-busy="False" style="color: #F4F7F5; margin-left: 10px;">
                Programación de despachos - Rumi Import
            </h2>
        </div>
    </header>



    <section id="cover">
        <div id="cover-caption">
            <div id="container" class="container">
                <div class="row">
                    <div class="col-sm-8 offset-sm-2 text-center">
                        <br>
                        <h4>Por favor digite el Nro de R.U.C. a Programar:</h4>
                        <div class="info-form">
                            <form id="FrmProgramacion" runat="server" class="form-inline justify-content-center">
                                <div class="form-group ">
                                    <asp:TextBox class="form-control form-control-sm" type="text" placeholder="R.U.C." ID="txtlote" runat="server"></asp:TextBox>

                                </div>
                                <br>
                                <div runat="server" id="Alerta" class="alert alert-danger" visible="false">Error al digitar el R.U.C., vuelva a intentarlo</div>
                                <div runat="server" id="AlertaInf" class="alert-dark" visible="false">No tiene pedidos pendientes para programar.</div>
                                  
                                
                                <div class="form-group ">
                                <asp:Button ID="Button1" class="active" runat="server" Text="Buscar" OnClick="Button1_Click" />
                                 </div>
                                    
                                    <asp:RadioButton ID="RadioButton1" runat="server" />
                                <asp:RadioButton ID="RadioButton2" runat="server" />
                                    
                                    <br>
                               
                                <div class="info-form">
                                <asp:GridView ID="gv_trazabilidad" AutoGenerateColumns="False" runat="server" Width="799px" CellPadding="4" Font-Names="Arial Narrow" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gv_trazabilidad_SelectedIndexChanged">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="Lote" HeaderText="Nro OV SAP">
                                        <HeaderStyle Width="100px" />
                                        </asp:BoundField>
										<asp:BoundField DataField="RazonSocial" HeaderText="Razón Social" >
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
                                        </asp:BoundField>
										<asp:BoundField DataField="fechacontabilizacion" HeaderText="Fecha contabilización">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:BoundField>
										<asp:BoundField DataField="Peso" HeaderText="Peso Total">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:BoundField>
										<asp:BoundField DataField="Estado" HeaderText="Estado Programación" >
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" />
                                        </asp:BoundField>
                                        <%--<asp:HyperLinkField DataTextField="Despacho" HeaderText="Despacho"  NavigateUrl="~/FrmProgamacion.aspx" />--%>
                                        <asp:HyperLinkField DataTextField="Despacho" HeaderText="Despacho" DataNavigateUrlFields="Lote,fechaalerta" DataNavigateUrlFormatString="http://192.168.1.88:8105//FrmProgamacion.aspx?Lote={0}&amp;fechaalerta={1}" NavigateUrl="~/FrmProgamacion.aspx" />
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor=dimgray Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                                </div>
                                <footer class="pt-2 mt-3 text-muted" style="text-align: center;">
                                    RUMI IMPORT S.A<span class="fw-light">. Copyright &copy; 2022</span></footer>
                            </form>
                        </div>
                        <br>
                    </div>
                </div>
            </div>
        </div>
    </section> 
</body>
</html>

