using Microsoft.EntityFrameworkCore.Migrations;

namespace Instagram.Data.Migrations.InstagramDb
{
    public partial class AddReportPost_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Posts_ReportPostId1",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_ReportPostId1",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ReportPostId1",
                table: "Reports");

            migrationBuilder.AlterColumn<int>(
                name: "ReportPostId",
                table: "Reports",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReportPostId",
                table: "Reports",
                column: "ReportPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Posts_ReportPostId",
                table: "Reports",
                column: "ReportPostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Posts_ReportPostId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_ReportPostId",
                table: "Reports");

            migrationBuilder.AlterColumn<string>(
                name: "ReportPostId",
                table: "Reports",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReportPostId1",
                table: "Reports",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReportPostId1",
                table: "Reports",
                column: "ReportPostId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Posts_ReportPostId1",
                table: "Reports",
                column: "ReportPostId1",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
