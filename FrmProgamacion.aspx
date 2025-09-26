<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmProgamacion.aspx.cs" Inherits="Despachos.FrmProgamacion" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>SistemadeCitas - Programacion de despachos</title>
    
    <link  href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-+0n0xVW2eSR5OomGNYDnhzAbDsOXxcvSN1TPprVMTNDbiYZCxYbOOl7+AMvyTG2x" crossorigin="anonymous">
   
    <script src = "https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"> </script>

    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.5.0/font/bootstrap-icons.css">
      <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>  
 <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/js/bootstrap.min.js" integrity="sha384-Atwg2Pkwv9vp0ygtn1JAojH0nYbwNJLPhwyoVbhoPwBhjQPR5VtM2+xf0Uwh9KtT" crossorigin="anonymous"></script>
    <script>

        function launchModal() {
            $(window).load(function () {
                $('#exampleModal').modal('show');
            });

        }


        function showContent() {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "progressBar": true,
                "preventDuplicates": false,
                "positionClass": "toast-top-right",
                "showDuration": "400",
                "hideDuration": "1000",
                "timeOut": "7000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
            toastr["success"]("This is a message");

        }
    </script>

    
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

            <!-- Modal -->
<div runat="server" class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header bg-dark">
          <h1 class="d-flex align-items-center fs-4 text-white mb-0">
                <img src="imagenes/rum.png" width="160" class="rounded float-start" alt="Web de citas Rumi">
                 
            </h1>

       <h5 class="modal-title text-white" id="exampleModalLabel">Términos y condiciones</h5>
       
      </div>
      <div class="modal-body">
          <p>  El horario para la programación de despacho es el siguiente: </p>  
          <p>  Lunes a viernes de 09:00 hrs a 17:00 hrs</p> 
          <p>  Sábados de 09:00 hrs a 12:00 hrs  </p> 
          <p>  Sólo está permitido realizar la programación del despacho dentro del horario establecido y la semana en curso.</p>
      </div>
      <div class="modal-footer">
        <button type="button" class="alert-danger" data-bs-dismiss="modal">Aceptar</button>
      
      </div>
    </div>
  </div>
</div>


    <form id="FrmProgramacion" runat="server">

           <div class="container py-3">

                <div class="col-md-12">
                       <div id="DivAlerta" runat="server" class="alert alert-secondary d-flex align-items-center mt-2" role="alert">
                           <svg xmlns="http://www.w3.org/2000/svg" style="display: none;">
                               <symbol id="check-circle-fill" fill="currentColor" viewBox="0 0 16 16">
                                   <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zm-3.97-3.03a.75.75 0 0 0-1.08.022L7.477 9.417 5.384 7.323a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-.01-1.05z" />
                               </symbol>
                           </svg>
                           <div>

                               <h4 id="alertHeader" runat="server" class="alert-heading">
                                   <svg class="bi flex-shrink-0 me-1" width="24" height="24" role="img" aria-label="Success:">
                                       <use xlink:href="#check-circle-fill" />
                                   </svg>
                                   <asp:Label ID="lblAlertHeader" runat="server" Text=""></asp:Label></h4>
                               <asp:Label ID="lblSuccess" runat="server" Text="" Visible="true"></asp:Label>
                               <hr>
                               <p class="mb-0">
                                   <asp:Label ID="lblHeaderFoot" runat="server" Text="" Visible="true"></asp:Label></p>
                           </div>
                       </div>
                   </div>


               <div class="col-md-12">
                       <div id="DivMsg" runat="server" class="alert alert-secondary d-flex align-items-center mt-2" role="alert">
                           <svg xmlns="http://www.w3.org/2000/svg" style="display: none;">
                               <symbol id="check-circle-fill" fill="currentColor" viewBox="0 0 16 16">
                                   <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zm-3.97-3.03a.75.75 0 0 0-1.08.022L7.477 9.417 5.384 7.323a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-.01-1.05z" />
                               </symbol>
                           </svg>
                           <div>

                               <h4 id="H1" runat="server" class="alert-heading">
                                   <svg class="bi flex-shrink-0 me-1" width="24" height="24" role="img" aria-label="Success:">
                                       <use xlink:href="#check-circle-fill" />
                                   </svg>
                                   <asp:Label ID="LblMsgHeader" runat="server" Text=""></asp:Label></h4>
                               <asp:Label ID="LblMsgBody" runat="server" Text="" Visible="true"></asp:Label>
                               <hr>
                               <p class="mb-0">
                                   <asp:Label ID="LblMsgFooter" runat="server" Text="" Visible="true"></asp:Label></p>
                           </div>
                       </div>
                   </div>

               <div class="container-fluid py-6">

                   <div class="row row-cols-1 row-cols-md-3 mb-6 text-center">

                       <div class="col">
                           <div class="card mb-4 rounded-6 shadow-sm border-secondary">
                               <div class="alert-danger">
                                   <h4 class="my-0 fw-normal">Nº de Orden</h4>
                               </div>
                               <div class="card-body">
                                   <h4 class="card-title pricing-card-title">
                                       <asp:Label ID="lblLote" runat="server" Text=""></asp:Label>
                                       <asp:Label ID="lblIdProgramacion" runat="server" Visible="False"></asp:Label>
                                   </h4>
                               </div>
                           </div>
                       </div>

                       <div class="col">
                           <div class="card mb-4 rounded-6 shadow-sm border-secondary">
                               <div class="alert-danger">
                                   <h4 class="my-0 fw-normal">Peso (Kg)</h4>
                               </div>
                               <div class="card-body">
                                   <h4 class="card-title pricing-card-title">
                                       <asp:Label ID="lblPeso" runat="server" Text=""></asp:Label>

                                   </h4>
                               </div>
                           </div>
                       </div>

                       <div class="col">
                           <div class="card mb-4 rounded-6 shadow-sm border-secondary">
                               <div class="alert-danger">
                                   <h4 class="my-0 fw-normal">Tiempo estimado</h4>
                               </div>
                               <div class="card-body">
                                   <h4 class="card-title pricing-card-title">
                                       <asp:Label ID="lblTiempo" runat="server" Text=""></asp:Label>
                                       <small class="text-muted fw-light">min</small></h4>
                               </div>
                           </div>
                       </div>

                   </div>

               </div>


               <div class="row align-items-md-stretch">
                   <div class="col-md-4">
                       <div class="h-100 p-4 text-black bg-light border rounded-3">
                           <h4 runat="server" id="HeaderFecha">Seleccione la fecha

                            <asp:Label ID="lblFecha" runat="server" Visible="False"></asp:Label>
                               <asp:Label ID="lblFI" runat="server" Visible="False"></asp:Label>
                               <asp:Label ID="lblFF" runat="server" Visible="False"></asp:Label>
                           </h4>
                           <div runat="server" id="AlertErrorFecha" class="alert alert-danger alert-dismissible fade show" role="alert">
                               <strong>Error!</strong>
                               <asp:Label ID="LblErrorFecha" runat="server" Text=""></asp:Label>
                               <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                           </div>
                           <div>

                               <asp:Calendar ID="datepicker" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" FirstDayOfWeek="Monday" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnDayRender="datepicker_DayRender" OnSelectionChanged="datepicker_SelectionChanged" Width="300px">
                                   <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                   <NextPrevStyle VerticalAlign="Bottom" />
                                   <OtherMonthDayStyle ForeColor="#808080" />
                                   <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                   <SelectorStyle BackColor="#CC3300" />
                                   <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                   <TodayDayStyle BackColor="White" ForeColor="Black" />
                                   <WeekendDayStyle BackColor="White" />
                               </asp:Calendar>

                               <asp:Label ID="lblFA" runat="server" Visible="False"></asp:Label>
                           </div>
                       </div>
                   </div>
                   <div class="col-md-4">
                       <div class="h-100 p-4 bg-light border rounded-3">
                           <h4>Seleccione la hora<asp:Label ID="lblHora" runat="server" Visible="False"></asp:Label>
                           </h4>
                           <div runat="server" id="AlertError" class="alert alert-danger alert-dismissible fade show" role="alert">
                               <strong>Error!</strong>
                               <asp:Label ID="LblError" runat="server" Text=""></asp:Label>
                               <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                           </div>

                           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                           <!-- UpdatePanel que contiene el Timer y los RadioButton -->
                           <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <ContentTemplate>

                           <asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick"></asp:Timer>
                               <div runat="server" id="BGroup" class="btn-group flex-wrap" role="group" aria-label="Basic radio toggle button group">

                                   <asp:RadioButton runat="server" type="radio" class="btn-check" GroupName="btnradio" ID="btnradio1" AutoPostBack="true" autocomplete="off" />
                                   <label runat="server" class="btn btn-outline-secondary m-lg-1" for="btnradio1" id="lblradio1">09:00</label>

                                   <asp:RadioButton runat="server" type="radio" class="btn-check" GroupName="btnradio" ID="btnradio2" AutoPostBack="true" autocomplete="off" />
                                   <label runat="server" class="btn btn-outline-secondary m-lg-1" for="btnradio2" id="lblradio2">09:30</label>

                                   <asp:RadioButton runat="server" type="radio" class="btn-check" GroupName="btnradio" ID="btnradio3" AutoPostBack="true" autocomplete="off" />
                                   <label runat="server" class="btn btn-outline-secondary  m-lg-1" for="btnradio3" id="lblradio3">10:00</label>

                                   <asp:RadioButton runat="server" type="radio" class="btn-check" GroupName="btnradio" ID="btnradio4" AutoPostBack="true" autocomplete="off" />
                                   <label runat="server" class="btn btn-outline-secondary m-lg-1" for="btnradio4" id="lblradio4">10:30</label>

                                   <asp:RadioButton runat="server" type="radio" class="btn-check" GroupName="btnradio" ID="btnradio5" AutoPostBack="true" autocomplete="off" />
                                   <label runat="server" class="btn btn-outline-secondary m-lg-1" for="btnradio5" id="lblradio5">11:00</label>

                                   <asp:RadioButton runat="server" type="radio" class="btn-check" GroupName="btnradio" ID="btnradio6" AutoPostBack="true" autocomplete="off" />
                                   <label runat="server" class="btn btn-outline-secondary m-lg-1" for="btnradio6" id="lblradio6">11:30</label>

                                   <asp:RadioButton runat="server" type="radio" class="btn-check" GroupName="btnradio" ID="btnradio7" AutoPostBack="true" autocomplete="off" />
                                   <label runat="server" class="btn btn-outline-secondary m-lg-1" for="btnradio7" id="lblradio7">12:00</label>

                                   <asp:RadioButton runat="server" type="radio" class="btn-check" GroupName="btnradio" ID="btnradio8" AutoPostBack="true" autocomplete="off" />
                                   <label runat="server" class="btn btn-outline-secondary m-lg-1" for="btnradio8" id="lblradio8">12:30</label>

                                   <asp:RadioButton runat="server" type="radio" class="btn-check" GroupName="btnradio" ID="btnradio9" AutoPostBack="true" autocomplete="off" />
                                   <label runat="server" class="btn btn-outline-secondary m-lg-1" for="btnradio9" id="lblradio9">13:00</label>

                                   <asp:RadioButton runat="server" type="radio" class="btn-check" GroupName="btnradio" ID="btnradio10" AutoPostBack="true" autocomplete="off" />
                                   <label runat="server" class="btn btn-outline-secondary m-lg-1" for="btnradio10" id="lblradio10">13:30</label>

                                   <asp:RadioButton runat="server" type="radio" class="btn-check" GroupName="btnradio" ID="btnradio11" AutoPostBack="true" autocomplete="off" />
                                   <label runat="server" class="btn btn-outline-secondary m-lg-1" for="btnradio11" id="lblradio11">14:00</label>

                                   <asp:RadioButton runat="server" type="radio" class="btn-check" GroupName="btnradio" ID="btnradio12" AutoPostBack="true" autocomplete="off" />
                                   <label runat="server" class="btn btn-outline-secondary m-lg-1" for="btnradio12" id="lblradio12">14:30</label>

                                   <asp:RadioButton runat="server" type="radio" class="btn-check" GroupName="btnradio" ID="btnradio13" AutoPostBack="true" autocomplete="off" />
                                   <label runat="server" class="btn btn-outline-secondary m-lg-1" for="btnradio13" id="lblradio13">15:00</label>

                                   <asp:RadioButton runat="server" type="radio" class="btn-check" GroupName="btnradio" ID="btnradio14" AutoPostBack="true" autocomplete="off" />
                                   <label runat="server" class="btn btn-outline-secondary m-lg-1" for="btnradio14" id="lblradio14">15:30</label>

                                   <asp:RadioButton runat="server" type="radio" class="btn-check" GroupName="btnradio" ID="btnradio15" AutoPostBack="true" autocomplete="off" />
                                   <label runat="server" class="btn btn-outline-secondary m-lg-1" for="btnradio15" id="lblradio15">16:00</label>

                                   <asp:RadioButton runat="server" type="radio" class="btn-check" GroupName="btnradio" ID="btnradio16" AutoPostBack="true" autocomplete="off" />
                                   <label runat="server" class="btn btn-outline-secondary m-lg-1" for="btnradio16" id="lblradio16">16:30</label>

                                   <asp:RadioButton runat="server" type="radio" class="btn-check" GroupName="btnradio" ID="btnradio17" AutoPostBack="true" autocomplete="off" />
                                   <label runat="server" class="btn btn-outline-secondary m-lg-1" for="btnradio17" id="lblradio17">17:00</label>
                               </div>
                              
                               <hr>
                               <h5>Leyenda</h5>
                               <div class="col-md-12">
                                    <asp:RadioButton ID="LeyDis" disabled="disabled" runat="server" type="radio" class="btn-check" AutoPostBack="true" autocomplete="off" />
                                    <label runat="server" style="background-color: #FFFFFF" class="btn btn-outline-secondary m-lg-1" for="LeyDis" id="Label2">Disponible</label>
                                    <asp:Label ID="Label3" runat="server" Text="Hora Disponible"></asp:Label>
                               </div>

                               <div class="col-md-12">
                                    <asp:RadioButton ID="LeyRes" disabled="disabled" runat="server" type="radio" class="btn-check" AutoPostBack="true" autocomplete="off" />
                                    <label runat="server" style="background-color: #CCCCCC" class="btn btn-outline-secondary m-lg-1" for="LeyRes" id="Label1">Reservado</label>
                                    <asp:Label ID="Label4" runat="server" Text="Hora Reservada"></asp:Label>
                               </div>

                                <div ID="DivFecHr" class="alert alert-dark" role="alert" runat ="server">
                                   <asp:Label ID="lblFecHr" runat="server" Text=""></asp:Label>
                               </div>
                            </ContentTemplate>
                           </asp:UpdatePanel>
                       </div>
                      
                   </div>
                   <div class="col-md-4">
                       <div class="h-100 p-4 bg-light border rounded-6">
                           <FONT SIZE=4>Información del conductor</FONT>
                           <p style="color: #5e9ca0;"><span style="color: #000000;"><em><strong>(Opcional)</strong></em></span></p>
                           <div class="row g-2">
                               <asp:TextBox class="form-control form-control-sm" type="text" placeholder="Nombre del conductor" aria-label=".form-control-sm example" ID="txtConductor" runat="server"></asp:TextBox>
                               <asp:TextBox class="form-control form-control-sm" type="text" placeholder="Licencia de conducir" aria-label=".form-control-sm example" ID="txtLicencia" runat="server"></asp:TextBox>
                               <asp:TextBox class="form-control form-control-sm" type="text" placeholder="Placa de vehiculo" aria-label=".form-control-sm example" ID="txtPlaca" runat="server"></asp:TextBox>
                           </div>
                           <hr />
                           <FONT SIZE=4>Observaciones</FONT>
                           <p style="color: #5e9ca0;"><span style="color: #000000;"><em><strong>(Opcional)</strong></em></span></p>
                           <div class="row g-2">
                               <asp:TextBox class="form-control form-control-sm" Rows="2" type="text" placeholder="Ingrese aquí información adicional para su despacho." aria-label=".form-control-sm example" ID="txtObservaciones" runat="server" TextMode="MultiLine"></asp:TextBox>
                           </div>
                           <hr>

                           <div class="d-grid">
                               <asp:Button ID="BtnCancelar" Class="active" runat="server" Text="Cancelar Programación" OnClick="Cancelar_Despacho_Click" /> 
                               <asp:Button ID="BtnConfirmar" Class="active" runat="server" Text="Confirmar" OnClick="Programar_Despacho_Click" />
                                <asp:Button ID="btn_retornar" CssClass="btn rounded-pill" runat="server" OnClick="btn_retornar_Click" Text="Retornar a la página Principal" style="text-decoration: underline; color: darkred;" />
                            </div>


                       </div>
                   </div>
               </div>
                
               
               <footer class="pt-2 mt-3 text-muted" style="text-align: center;">
                   <span class="fw-light">RUMI IMPORT S.A Copyright &copy; 2022</span>
               </footer>
        </div>


        
    </form>
    <a href="Web.config">Web.config</a>
</body>
</html>
