using Microsoft.EntityFrameworkCore.Migrations;

namespace CVASP.NETLECT.Migrations
{
    public partial class firstmigrationupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobOffers_JobOfferID",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_JobOfferID",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "JobOfferID",
                table: "JobApplications");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "JobApplications",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "JobApplications",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "JobApplications",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationDescription",
                table: "JobApplications",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationDescription",
                table: "JobApplications");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "JobApplications",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "JobApplications",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "JobApplications",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "JobOfferID",
                table: "JobApplications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobOfferID",
                table: "JobApplications",
                column: "JobOfferID");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobOffers_JobOfferID",
                table: "JobApplications",
                column: "JobOfferID",
                principalTable: "JobOffers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
