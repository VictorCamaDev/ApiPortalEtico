using System;
using System.Collections.Generic;
using System.Text;
using ApiPortalEtico.Domain.Entities;

namespace ApiPortalEtico.Application.Emails.Templates
{
    public static class EmailTemplateService
    {
        public static string GenerateIrregularityReportEmailTemplate(ApiPortalEtico.Domain.Entities.IrregularityReport report, string additionalMessage)
        {
            // Función auxiliar para manejar valores nulos o vacíos
            string GetValueOrDash(string value)
            {
                return string.IsNullOrWhiteSpace(value) ? "-" : value;
            }

            // Colores corporativos
            string darkGreen = "#115740";
            string lightGreen = "#7a9a01";
            string lightGray = "#f5f5f5";
            string textColor = "#333333";

            // Preparar los valores para evitar problemas de interpolación
            string tipoIrregularidad = GetValueOrDash(report.TipoIrregularidad);
            string fechaIncidente = report.Fecha.ToString("dd/MM/yyyy");
            string detalles = GetValueOrDash(report.Detalles);

            string pais = GetValueOrDash(report.Ubicacion?.Pais);
            string provincia = GetValueOrDash(report.Ubicacion?.Provincia);
            string ciudad = GetValueOrDash(report.Ubicacion?.Ciudad);
            string sede = GetValueOrDash(report.Ubicacion?.Sede);

            string evidenciaTipo = GetValueOrDash(report.Evidencia?.Tipo);
            string evidenciaDondeObtener = GetValueOrDash(report.Evidencia?.DondeObtener);
            string evidenciaEntregaFisica = GetValueOrDash(report.Evidencia?.EntregaFisica);

            string beneficios = GetValueOrDash(report.Beneficios);
            string testigos = GetValueOrDash(report.Testigos);
            string conocimiento = GetValueOrDash(report.Conocimiento);
            string involucraExternos = GetValueOrDash(report.InvolucraExternos);
            string quienesExternos = GetValueOrDash(report.QuienesExternos);
            string ocultado = GetValueOrDash(report.Ocultado);
            string comoOcultado = GetValueOrDash(report.ComoOcultado);
            string quienesOcultan = GetValueOrDash(report.QuienesOcultan);
            string conocimientoPrevio = GetValueOrDash(report.ConocimientoPrevio);
            string quienesConocen = GetValueOrDash(report.QuienesConocen);
            string comoConocen = GetValueOrDash(report.ComoConocen);
            string relacion = GetValueOrDash(report.Relacion);
            string relacionGrupo = GetValueOrDash(report.RelacionGrupo);
            string anonimo = GetValueOrDash(report.Anonimo);

            StringBuilder sb = new StringBuilder();

            sb.Append(@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1'>
    <title>Reporte de Irregularidad - Grupo Silvestre Ético</title>
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;500;700&display=swap');
        
        body, html {
            margin: 0;
            padding: 0;
            font-family: 'Roboto', Arial, sans-serif;
            color: " + textColor + @";
            line-height: 1.6;
        }
        
        .container {
            max-width: 650px;
            margin: 0 auto;
            padding: 0;
            background-color: #ffffff;
        }
        
        .header {
            background-color: " + darkGreen + @";
            padding: 20px 30px;
            text-align: left;
            color: white;
            border-top-left-radius: 4px;
            border-top-right-radius: 4px;
        }
        
        .header h1 {
            margin: 0;
            font-size: 24px;
            font-weight: 500;
        }
        
        .header p {
            margin: 5px 0 0 0;
            font-size: 16px;
            opacity: 0.9;
        }
        
        .content {
            padding: 30px;
        }
        
        .message-box {
            background-color: " + lightGray + @";
            border-left: 4px solid " + lightGreen + @";
            padding: 15px;
            margin-bottom: 25px;
            border-radius: 4px;
        }
        
        .section {
            margin-bottom: 25px;
        }
        
        .section h2 {
            color: " + darkGreen + @";
            font-size: 18px;
            margin-top: 0;
            margin-bottom: 15px;
            padding-bottom: 8px;
            border-bottom: 1px solid #e0e0e0;
        }
        
        .data-row {
            margin-bottom: 10px;
        }
        
        .data-label {
            font-weight: 500;
            color: " + darkGreen + @";
        }
        
        .data-value {
            margin-top: 3px;
        }
        
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
        }
        
        table th {
            background-color: " + lightGreen + @";
            color: white;
            text-align: left;
            padding: 10px;
        }
        
        table td {
            padding: 10px;
            border-bottom: 1px solid #e0e0e0;
        }
        
        table tr:nth-child(even) {
            background-color: " + lightGray + @";
        }
        
        .footer {
            background-color: " + darkGreen + @";
            color: white;
            padding: 20px 30px;
            text-align: center;
            font-size: 14px;
            border-bottom-left-radius: 4px;
            border-bottom-right-radius: 4px;
        }
        
        .reference-number {
            background-color: " + lightGreen + @";
            color: white;
            padding: 15px;
            text-align: center;
            font-size: 18px;
            font-weight: 500;
            margin: 25px 0;
            border-radius: 4px;
        }
        
        .logo {
            max-height: 60px;
            margin-bottom: 10px;
        }
        
        @media only screen and (max-width: 600px) {
            .container {
                width: 100% !important;
            }
            
            .header, .content, .footer {
                padding: 15px !important;
            }
        }
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <img src='https://hebbkx1anhila5yf.public.blob.vercel-storage.com/image-UqP4MjTluHXd8ORrnjCOGAihqAaAfp.png' alt='Grupo Silvestre Ético' class='logo'>
            <h1>Grupo Silvestre Ético</h1>
            <p>Sistema de reporte de irregularidades éticas</p>
        </div>
        
        <div class='content'>
            <div class='message-box'>
                <p>Estimado(a) usuario,</p>
                <p>" + additionalMessage + @"</p>
            </div>
            
            <div class='reference-number'>
                Número de Referencia: #" + report.Id + @"
            </div>
            
            <div class='section'>
                <h2>Información General</h2>
                <div class='data-row'>
                    <div class='data-label'>Tipo de Irregularidad:</div>
                    <div class='data-value'>" + tipoIrregularidad + @"</div>
                </div>
                <div class='data-row'>
                    <div class='data-label'>Fecha del Incidente:</div>
                    <div class='data-value'>" + fechaIncidente + @"</div>
                </div>
                <div class='data-row'>
                    <div class='data-label'>Detalles:</div>
                    <div class='data-value'>" + detalles + @"</div>
                </div>
            </div>
            
            <div class='section'>
                <h2>Ubicación</h2>
                <div class='data-row'>
                    <div class='data-label'>País:</div>
                    <div class='data-value'>" + pais + @"</div>
                </div>
                <div class='data-row'>
                    <div class='data-label'>Provincia:</div>
                    <div class='data-value'>" + provincia + @"</div>
                </div>
                <div class='data-row'>
                    <div class='data-label'>Ciudad:</div>
                    <div class='data-value'>" + ciudad + @"</div>
                </div>
                <div class='data-row'>
                    <div class='data-label'>Sede:</div>
                    <div class='data-value'>" + sede + @"</div>
                </div>
            </div>
            
            <div class='section'>
                <h2>Personas Involucradas</h2>");

            if (report.Involucrados != null && report.Involucrados.Count > 0)
            {
                sb.Append(@"
                <table>
                    <tr>
                        <th>Nombre</th>
                        <th>Apellido</th>
                        <th>Relación</th>
                        <th>Otro</th>
                    </tr>");

                foreach (var involucrado in report.Involucrados)
                {
                    sb.Append(@"
                    <tr>
                        <td>" + GetValueOrDash(involucrado.Nombre) + @"</td>
                        <td>" + GetValueOrDash(involucrado.Apellido) + @"</td>
                        <td>" + GetValueOrDash(involucrado.Relacion) + @"</td>
                        <td>" + GetValueOrDash(involucrado.Otro) + @"</td>
                    </tr>");
                }

                sb.Append(@"
                </table>");
            }
            else
            {
                sb.Append(@"
                <p>No se han especificado personas involucradas.</p>");
            }

            sb.Append(@"
            </div>
            
            <div class='section'>
                <h2>Evidencia</h2>
                <div class='data-row'>
                    <div class='data-label'>Tipo:</div>
                    <div class='data-value'>" + evidenciaTipo + @"</div>
                </div>
                <div class='data-row'>
                    <div class='data-label'>Dónde Obtener:</div>
                    <div class='data-value'>" + evidenciaDondeObtener + @"</div>
                </div>");

            if (!string.IsNullOrEmpty(report.Evidencia?.EntregaFisica))
            {
                sb.Append(@"
                <div class='data-row'>
                    <div class='data-label'>Entrega Física:</div>
                    <div class='data-value'>" + evidenciaEntregaFisica + @"</div>
                </div>");
            }

            sb.Append(@"
            </div>
            
            <div class='section'>
                <h2>Información Adicional</h2>
                <div class='data-row'>
                    <div class='data-label'>Beneficios:</div>
                    <div class='data-value'>" + beneficios + @"</div>
                </div>
                <div class='data-row'>
                    <div class='data-label'>Testigos:</div>
                    <div class='data-value'>" + testigos + @"</div>
                </div>
                <div class='data-row'>
                    <div class='data-label'>Conocimiento:</div>
                    <div class='data-value'>" + conocimiento + @"</div>
                </div>
                <div class='data-row'>
                    <div class='data-label'>¿Involucra a Externos?:</div>
                    <div class='data-value'>" + involucraExternos + @"</div>
                </div>");

            if (report.InvolucraExternos == "si" && !string.IsNullOrEmpty(report.QuienesExternos))
            {
                sb.Append(@"
                <div class='data-row'>
                    <div class='data-label'>¿Quiénes Externos?:</div>
                    <div class='data-value'>" + quienesExternos + @"</div>
                </div>");
            }

            sb.Append(@"
                <div class='data-row'>
                    <div class='data-label'>¿Ha sido ocultado?:</div>
                    <div class='data-value'>" + ocultado + @"</div>
                </div>");

            if (report.Ocultado == "si")
            {
                if (!string.IsNullOrEmpty(report.ComoOcultado))
                {
                    sb.Append(@"
                    <div class='data-row'>
                        <div class='data-label'>¿Cómo ha sido ocultado?:</div>
                        <div class='data-value'>" + comoOcultado + @"</div>
                    </div>");
                }

                if (!string.IsNullOrEmpty(report.QuienesOcultan))
                {
                    sb.Append(@"
                    <div class='data-row'>
                        <div class='data-label'>¿Quiénes lo ocultan?:</div>
                        <div class='data-value'>" + quienesOcultan + @"</div>
                    </div>");
                }
            }

            sb.Append(@"
                <div class='data-row'>
                    <div class='data-label'>¿Conocimiento previo?:</div>
                    <div class='data-value'>" + conocimientoPrevio + @"</div>
                </div>");

            if (report.ConocimientoPrevio == "si")
            {
                if (!string.IsNullOrEmpty(report.QuienesConocen))
                {
                    sb.Append(@"
                    <div class='data-row'>
                        <div class='data-label'>¿Quiénes conocen?:</div>
                        <div class='data-value'>" + quienesConocen + @"</div>
                    </div>");
                }

                if (!string.IsNullOrEmpty(report.ComoConocen))
                {
                    sb.Append(@"
                    <div class='data-row'>
                        <div class='data-label'>¿Cómo conocen?:</div>
                        <div class='data-value'>" + comoConocen + @"</div>
                    </div>");
                }
            }

            sb.Append(@"
                <div class='data-row'>
                    <div class='data-label'>Relación:</div>
                    <div class='data-value'>" + relacion + @"</div>
                </div>
                <div class='data-row'>
                    <div class='data-label'>Relación con el Grupo:</div>
                    <div class='data-value'>" + relacionGrupo + @"</div>
                </div>
                <div class='data-row'>
                    <div class='data-label'>¿Reporte Anónimo?:</div>
                    <div class='data-value'>" + anonimo + @"</div>
                </div>
            </div>
        </div>
        
        <div class='footer'>
            <p>© " + DateTime.Now.Year + @" Grupo Silvestre Ético. Todos los derechos reservados.</p>
            <p>Este correo es confidencial y contiene información sensible. Por favor no lo reenvíe.</p>
            <p>Para más información, contacte a: <a href='mailto:reportes@gruposilvestretico.com' style='color: white;'>reportes@gruposilvestretico.com</a></p>
        </div>
    </div>
</body>
</html>");

            return sb.ToString();
        }
    }
}

