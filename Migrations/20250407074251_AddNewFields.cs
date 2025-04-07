using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPortalEtico.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "QuienesOcultan",
                table: "IrregularityReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QuienesExternos",
                table: "IrregularityReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QuienesConocen",
                table: "IrregularityReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ComoOcultado",
                table: "IrregularityReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ComoConocen",
                table: "IrregularityReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AceptaTerminos",
                table: "IrregularityReports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "IrregularityReports",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AreaOtro",
                table: "IrregularityReports",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cargo",
                table: "IrregularityReports",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NombreCompleto",
                table: "IrregularityReports",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OtroContacto",
                table: "IrregularityReports",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "IrregularityReports",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AceptaTerminos",
                table: "IrregularityReports");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "IrregularityReports");

            migrationBuilder.DropColumn(
                name: "AreaOtro",
                table: "IrregularityReports");

            migrationBuilder.DropColumn(
                name: "Cargo",
                table: "IrregularityReports");

            migrationBuilder.DropColumn(
                name: "NombreCompleto",
                table: "IrregularityReports");

            migrationBuilder.DropColumn(
                name: "OtroContacto",
                table: "IrregularityReports");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "IrregularityReports");

            migrationBuilder.AlterColumn<string>(
                name: "QuienesOcultan",
                table: "IrregularityReports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "QuienesExternos",
                table: "IrregularityReports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "QuienesConocen",
                table: "IrregularityReports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ComoOcultado",
                table: "IrregularityReports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ComoConocen",
                table: "IrregularityReports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
