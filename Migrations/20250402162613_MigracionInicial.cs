using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPortalEtico.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IrregularityReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIrregularidad = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Detalles = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Beneficios = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Testigos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Conocimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvolucraExternos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuienesExternos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ocultado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComoOcultado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuienesOcultan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConocimientoPrevio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuienesConocen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComoConocen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Relacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoContacto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelacionGrupo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Anonimo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IrregularityReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Evidencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DondeObtener = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EntregaFisica = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IrregularityReportId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evidencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evidencias_IrregularityReports_IrregularityReportId",
                        column: x => x.IrregularityReportId,
                        principalTable: "IrregularityReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Involucrados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Relacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Otro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IrregularityReportId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Involucrados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Involucrados_IrregularityReports_IrregularityReportId",
                        column: x => x.IrregularityReportId,
                        principalTable: "IrregularityReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ubicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pais = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Provincia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sede = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IrregularityReportId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ubicaciones_IrregularityReports_IrregularityReportId",
                        column: x => x.IrregularityReportId,
                        principalTable: "IrregularityReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evidencias_IrregularityReportId",
                table: "Evidencias",
                column: "IrregularityReportId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Involucrados_IrregularityReportId",
                table: "Involucrados",
                column: "IrregularityReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Ubicaciones_IrregularityReportId",
                table: "Ubicaciones",
                column: "IrregularityReportId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evidencias");

            migrationBuilder.DropTable(
                name: "Involucrados");

            migrationBuilder.DropTable(
                name: "Ubicaciones");

            migrationBuilder.DropTable(
                name: "IrregularityReports");
        }
    }
}
