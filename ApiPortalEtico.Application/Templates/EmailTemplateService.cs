using System;
using System.Collections.Generic;
using System.Text;
using ApiPortalEtico.Domain.Entities;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ApiPortalEtico.Application.Emails.Templates
{
    public static class EmailTemplateService
    {
        private static IWebHostEnvironment _environment;

        public static void Initialize(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public static string GenerateIrregularityReportEmailTemplate(ApiPortalEtico.Domain.Entities.IrregularityReport report, string additionalMessage)
        {
            // Función auxiliar para manejar valores nulos o vacíos
            string GetValueOrDash(string value)
            {
                return string.IsNullOrWhiteSpace(value) ? "-" : value;
            }

            // Colores corporativos - definidos como variables para facilitar su uso
            string darkGreen = "#115740";
            string lightGreen = "#7a9a01";
            string lightGray = "#f8f9fa";
            string borderColor = "#e9ecef";
            string textColor = "#343a40";

            // Preparar los valores para evitar problemas de interpolación
            string tipoIrregularidad = GetValueOrDash(report.TipoIrregularidad);
            string fechaIncidente = report.Fecha.ToString("dd/MM/yyyy");
            string detalles = GetValueOrDash(report.Detalles);
            string fechaCreacion = report.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss");

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

            // Determinar qué correo mostrar según si es anónimo o no
            string correoMostrar = report.Anonimo?.ToLower() == "si" || report.Anonimo?.ToLower() == "sí"
                ? GetValueOrDash(report.CorreoContacto)
                : GetValueOrDash(report.Correo);

            string relacionGrupo = GetValueOrDash(report.RelacionGrupo);
            string anonimo = GetValueOrDash(report.Anonimo);

            // Nuevos campos
            string nombreCompleto = GetValueOrDash(report.NombreCompleto);
            string telefono = GetValueOrDash(report.Telefono);
            string otroContacto = GetValueOrDash(report.OtroContacto);
            string cargo = GetValueOrDash(report.Cargo);
            string area = GetValueOrDash(report.Area);
            string areaOtro = GetValueOrDash(report.AreaOtro);

            // Obtener la URL base para el logo
            string logoPath = "cid:logo"; // Usamos Content-ID para imágenes embebidas

            StringBuilder sb = new StringBuilder();

            sb.Append(@"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <title>Reporte de Irregularidad - Grupo Silvestre Ético</title>
</head>
<body style=""margin: 0; padding: 0; font-family: Arial, Helvetica, sans-serif; color: #343a40; background-color: #f1f3f5;"">
    <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
        <tr>
            <td align=""center"" style=""padding: 20px 0;"">
                <!-- CONTAINER TABLE -->
                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"" style=""background-color: #ffffff; border-radius: 8px; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);"">
                    <!-- HEADER -->
                    <tr>
                        <td align=""center"" bgcolor=""" + darkGreen + @""" style=""padding: 30px; color: #ffffff;"">
                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                                <tr>
                                    <td align=""center"" style=""padding-bottom: 15px;"">
                                        <img src="""""" + logoPath + @"""""" alt=""""Grupo Silvestre Ético"""" style=""""max-height: 70px; display: block;"""" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align=""center"" style=""font-size: 28px; font-weight: 600;"">
                                        Grupo Silvestre Ético
                                    </td>
                                </tr>
                                <tr>
                                    <td align=""center"" style=""font-size: 16px; padding-top: 8px;"">
                                        Sistema de reporte de irregularidades éticas
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    
                    <!-- CONTENT -->
                    <tr>
                        <td style=""padding: 30px;"">
                            <!-- Message Box -->
                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""margin-bottom: 30px;"">
                                <tr>
                                    <td style=""background-color: #f8f9fa; border-left: 4px solid " + lightGreen + @"; padding: 20px; border-radius: 6px;"">
                                        <p style=""margin-top: 0;"">Estimado(a) usuario,</p>
                                        <p style=""margin-bottom: 0;"">" + additionalMessage + @"</p>
                                    </td>
                                </tr>
                            </table>
                            
                            <!-- Reference Number -->
                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""margin-bottom: 25px;"">
                                <tr>
                                    <td align=""center"" bgcolor=""" + lightGreen + @""" style=""padding: 20px; color: #ffffff; font-size: 18px; font-weight: 600; border-radius: 6px;"">
                                        Número de Referencia: #" + report.Id + @"
                                    </td>
                                </tr>
                            </table>
                            
                            <!-- Información del Reportante -->
                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""margin-bottom: 25px; border: 1px solid #e9ecef; border-radius: 8px;"">
                                <tr>
                                    <td bgcolor=""#f8f9fa"" style=""padding: 15px 20px; border-bottom: 1px solid #e9ecef;"">
                                        <h2 style=""color: #115740; font-size: 18px; margin: 0; font-weight: 600;"">
                                            Información del Reportante
                                        </h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""padding: 20px;"">
                                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                                            <tr>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-right: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">Nombre Completo</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + nombreCompleto + @"</div>
                                                </td>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-left: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">Teléfono</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + telefono + @"</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-right: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">Correo Electrónico</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + correoMostrar + @"</div>
                                                </td>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-left: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">Otro Contacto</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + otroContacto + @"</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-right: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">Cargo</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + cargo + @"</div>
                                                </td>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-left: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">Área</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + area + (string.IsNullOrEmpty(areaOtro) ? "" : " - " + areaOtro) + @"</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-right: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">Reporte Anónimo</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + anonimo + @"</div>
                                                </td>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-left: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">Relación con el Grupo</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + relacionGrupo + @"</div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            
                            <!-- General Information Card -->
                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""margin-bottom: 25px; border: 1px solid #e9ecef; border-radius: 8px;"">
                                <tr>
                                    <td bgcolor=""#f8f9fa"" style=""padding: 15px 20px; border-bottom: 1px solid #e9ecef;"">
                                        <h2 style=""color: #115740; font-size: 18px; margin: 0; font-weight: 600;"">
                                            Información General
                                        </h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""padding: 20px;"">
                                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                                            <tr>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-right: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">Seleccione el tipo de irregularidad que desea reportar</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + tipoIrregularidad + @"</div>
                                                </td>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-left: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">Fecha del Incidente</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + fechaIncidente + @"</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-right: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">Fecha de Creación</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + fechaCreacion + @"</div>
                                                </td>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-left: 10px; vertical-align: top;"">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan=""2"" style=""padding-bottom: 15px;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">Describa con el mayor detalle posible los hechos irregulares</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + detalles + @"</div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            
                            <!-- Location Card -->
                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""margin-bottom: 25px; border: 1px solid #e9ecef; border-radius: 8px;"">
                                <tr>
                                    <td bgcolor=""#f8f9fa"" style=""padding: 15px 20px; border-bottom: 1px solid #e9ecef;"">
                                        <h2 style=""color: #115740; font-size: 18px; margin: 0; font-weight: 600;"">
                                            Ubicación
                                        </h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""padding: 20px;"">
                                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                                            <tr>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-right: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">País</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + pais + @"</div>
                                                </td>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-left: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">Provincia</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + provincia + @"</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-right: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">Ciudad</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + ciudad + @"</div>
                                                </td>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-left: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">Sede</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + sede + @"</div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            
                            <!-- People Involved Card -->
                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""margin-bottom: 25px; border: 1px solid #e9ecef; border-radius: 8px;"">
                                <tr>
                                    <td bgcolor=""#f8f9fa"" style=""padding: 15px 20px; border-bottom: 1px solid #e9ecef;"">
                                        <h2 style=""color: #115740; font-size: 18px; margin: 0; font-weight: 600;"">
                                            Personas Involucradas
                                        </h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""padding: 20px;"">");

            if (report.Involucrados != null && report.Involucrados.Count > 0)
            {
                sb.Append(@"
                                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""border: 1px solid #e9ecef; border-collapse: collapse;"">
                                            <tr>
                                                <th style=""background-color: " + lightGreen + @"; color: white; text-align: left; padding: 12px 15px; font-weight: 500; font-size: 14px;"">Nombre</th>
                                                <th style=""background-color: " + lightGreen + @"; color: white; text-align: left; padding: 12px 15px; font-weight: 500; font-size: 14px;"">Apellido</th>
                                                <th style=""background-color: " + lightGreen + @"; color: white; text-align: left; padding: 12px 15px; font-weight: 500; font-size: 14px;"">Relación</th>
                                                <th style=""background-color: " + lightGreen + @"; color: white; text-align: left; padding: 12px 15px; font-weight: 500; font-size: 14px;"">Otro</th>
                                            </tr>");

                int rowIndex = 0;
                foreach (var involucrado in report.Involucrados)
                {
                    string rowStyle = rowIndex % 2 == 0 ? "" : "background-color: #f8f9fa;";
                    sb.Append(@"
                                            <tr style=""" + rowStyle + @""">
                                                <td style=""padding: 12px 15px; border-bottom: 1px solid #e9ecef; font-size: 14px;"">" + GetValueOrDash(involucrado.Nombre) + @"</td>
                                                <td style=""padding: 12px 15px; border-bottom: 1px solid #e9ecef; font-size: 14px;"">" + GetValueOrDash(involucrado.Apellido) + @"</td>
                                                <td style=""padding: 12px 15px; border-bottom: 1px solid #e9ecef; font-size: 14px;"">" + GetValueOrDash(involucrado.Relacion) + @"</td>
                                                <td style=""padding: 12px 15px; border-bottom: 1px solid #e9ecef; font-size: 14px;"">" + GetValueOrDash(involucrado.Otro) + @"</td>
                                            </tr>");
                    rowIndex++;
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
                                    </td>
                                </tr>
                            </table>
                            
                            <!-- Evidence Card -->
                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""margin-bottom: 25px; border: 1px solid #e9ecef; border-radius: 8px;"">
                                <tr>
                                    <td bgcolor=""#f8f9fa"" style=""padding: 15px 20px; border-bottom: 1px solid #e9ecef;"">
                                        <h2 style=""color: #115740; font-size: 18px; margin: 0; font-weight: 600;"">
                                            Evidencia
                                        </h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""padding: 20px;"">
                                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                                            <tr>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-right: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">Tipo</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + evidenciaTipo + @"</div>
                                                </td>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-left: 10px; vertical-align: top;"">");

            if (!string.IsNullOrEmpty(report.Evidencia?.EntregaFisica))
            {
                sb.Append(@"
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">Entrega Física</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + evidenciaEntregaFisica + @"</div>");
            }

            sb.Append(@"
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan=""2"" style=""padding-bottom: 15px;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">¿Dónde se puede obtener la evidencia?</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + evidenciaDondeObtener + @"</div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            
                            <!-- Additional Information Card -->
                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""margin-bottom: 25px; border: 1px solid #e9ecef; border-radius: 8px;"">
                                <tr>
                                    <td bgcolor=""#f8f9fa"" style=""padding: 15px 20px; border-bottom: 1px solid #e9ecef;"">
                                        <h2 style=""color: #115740; font-size: 18px; margin: 0; font-weight: 600;"">
                                            Información Adicional
                                        </h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""padding: 20px;"">
                                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                                            <tr>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-right: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">¿Qué tipos de beneficios reciben las personas involucradas en la irregularidad?</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + beneficios + @"</div>
                                                </td>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-left: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">¿Qué testigos considera que podrían contribuir con mayor evidencia de esta irregularidad?</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + testigos + @"</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-right: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">Conocimiento</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + conocimiento + @"</div>
                                                </td>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-left: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">¿Este incidente involucra a personas externas al grupo?</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + involucraExternos + @"</div>
                                                </td>
                                            </tr>");

            if (report.InvolucraExternos == "si" && !string.IsNullOrEmpty(report.QuienesExternos))
            {
                sb.Append(@"
                                            <tr>
                                                <td colspan=""2"" style=""padding-bottom: 15px;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">¿Qué personas externas al grupo están involucradas?</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + quienesExternos + @"</div>
                                                </td>
                                            </tr>");
            }

            sb.Append(@"
                                            <tr>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-right: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">¿Cree usted que estos hechos están siendo ocultados de alguna manera?</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + ocultado + @"</div>
                                                </td>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-left: 10px; vertical-align: top;"">
                                                </td>
                                            </tr>");

            if (report.Ocultado == "si")
            {
                if (!string.IsNullOrEmpty(report.ComoOcultado))
                {
                    sb.Append(@"
                                            <tr>
                                                <td colspan=""2"" style=""padding-bottom: 15px;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">¿De qué manera se están ocultando estos hechos?</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + comoOcultado + @"</div>
                                                </td>
                                            </tr>");
                }

                if (!string.IsNullOrEmpty(report.QuienesOcultan))
                {
                    sb.Append(@"
                                            <tr>
                                                <td colspan=""2"" style=""padding-bottom: 15px;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">¿Qué personas están ocultando estos hechos?</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + quienesOcultan + @"</div>
                                                </td>
                                            </tr>");
                }
            }

            sb.Append(@"
                                            <tr>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-right: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">¿Alguna dirección, gerencia, subgerencia o jefatura conoce de estos hechos?</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + conocimientoPrevio + @"</div>
                                                </td>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-left: 10px; vertical-align: top;"">
                                                </td>
                                            </tr>");

            if (report.ConocimientoPrevio == "si")
            {
                if (!string.IsNullOrEmpty(report.QuienesConocen))
                {
                    sb.Append(@"
                                            <tr>
                                                <td colspan=""2"" style=""padding-bottom: 15px;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">¿Qué direcciones, gerencias, subgerencias o jefaturas conocen estos hechos?</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + quienesConocen + @"</div>
                                                </td>
                                            </tr>");
                }

                if (!string.IsNullOrEmpty(report.ComoConocen))
                {
                    sb.Append(@"
                                            <tr>
                                                <td colspan=""2"" style=""padding-bottom: 15px;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">¿Cómo se enteraron de estos hechos?</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + comoConocen + @"</div>
                                                </td>
                                            </tr>");
                }
            }

            sb.Append(@"
                                            <tr>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-right: 10px; vertical-align: top;"">
                                                    <div style=""font-weight: 500; color: #115740; font-size: 14px; margin-bottom: 5px;"">¿Qué tipo de relación existe entre las personas involucradas?</div>
                                                    <div style=""font-size: 15px; padding: 8px 12px; background-color: #f8f9fa; border-radius: 4px; border: 1px solid #e9ecef;"">" + relacion + @"</div>
                                                </td>
                                                <td width=""50%"" style=""padding-bottom: 15px; padding-left: 10px; vertical-align: top;"">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    
                    <!-- FOOTER -->
                    <tr>
                        <td bgcolor=""" + darkGreen + @""" style=""padding: 25px 30px; color: #ffffff; text-align: center; font-size: 14px;"">
                            <p style=""margin: 8px 0;"">© " + DateTime.Now.Year + @" Grupo Silvestre Ético. Todos los derechos reservados.</p>
                            <p style=""margin: 8px 0;"">Este correo es confidencial y contiene información sensible. Por favor no lo reenvíe.</p>
                            <p style=""margin: 8px 0;"">Para más información, contacte a: <a href=""mailto:reportes@gruposilvestretico.com"" style=""color: #ffffff; text-decoration: underline;"">reportes@gruposilvestretico.com</a></p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>");

            return sb.ToString();
        }
    }
}

