using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFcoreApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ogrencild1",
                table: "KursKayitlari",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_KursKayitlari_KursId",
                table: "KursKayitlari",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_KursKayitlari_Ogrencild1",
                table: "KursKayitlari",
                column: "Ogrencild1");

            migrationBuilder.AddForeignKey(
                name: "FK_KursKayitlari_Kurslar_KursId",
                table: "KursKayitlari",
                column: "KursId",
                principalTable: "Kurslar",
                principalColumn: "KursId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KursKayitlari_Ogrenciler_Ogrencild1",
                table: "KursKayitlari",
                column: "Ogrencild1",
                principalTable: "Ogrenciler",
                principalColumn: "Ogrencild",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KursKayitlari_Kurslar_KursId",
                table: "KursKayitlari");

            migrationBuilder.DropForeignKey(
                name: "FK_KursKayitlari_Ogrenciler_Ogrencild1",
                table: "KursKayitlari");

            migrationBuilder.DropIndex(
                name: "IX_KursKayitlari_KursId",
                table: "KursKayitlari");

            migrationBuilder.DropIndex(
                name: "IX_KursKayitlari_Ogrencild1",
                table: "KursKayitlari");

            migrationBuilder.DropColumn(
                name: "Ogrencild1",
                table: "KursKayitlari");
        }
    }
}
