using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLySinhVienAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "giangViens",
                columns: table => new
                {
                    IdGiangVien = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameGiangVien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_giangViens", x => x.IdGiangVien);
                });

            migrationBuilder.CreateTable(
                name: "khoaHocs",
                columns: table => new
                {
                    IdKhoaHoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameKhoaHoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_khoaHocs", x => x.IdKhoaHoc);
                });

            migrationBuilder.CreateTable(
                name: "monHocs",
                columns: table => new
                {
                    IdMonHoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameMonHoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdGiangVien = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monHocs", x => x.IdMonHoc);
                    table.ForeignKey(
                        name: "FK_monHocs_giangViens_IdGiangVien",
                        column: x => x.IdGiangVien,
                        principalTable: "giangViens",
                        principalColumn: "IdGiangVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lopHocs",
                columns: table => new
                {
                    LopId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameLop = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdMonHoc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lopHocs", x => x.LopId);
                    table.ForeignKey(
                        name: "FK_lopHocs_monHocs_IdMonHoc",
                        column: x => x.IdMonHoc,
                        principalTable: "monHocs",
                        principalColumn: "IdMonHoc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sinhViens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LopId = table.Column<int>(type: "int", nullable: false),
                    IdKhoaHoc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sinhViens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sinhViens_khoaHocs_IdKhoaHoc",
                        column: x => x.IdKhoaHoc,
                        principalTable: "khoaHocs",
                        principalColumn: "IdKhoaHoc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sinhViens_lopHocs_LopId",
                        column: x => x.LopId,
                        principalTable: "lopHocs",
                        principalColumn: "LopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lopHocs_IdMonHoc",
                table: "lopHocs",
                column: "IdMonHoc");

            migrationBuilder.CreateIndex(
                name: "IX_monHocs_IdGiangVien",
                table: "monHocs",
                column: "IdGiangVien");

            migrationBuilder.CreateIndex(
                name: "IX_sinhViens_IdKhoaHoc",
                table: "sinhViens",
                column: "IdKhoaHoc");

            migrationBuilder.CreateIndex(
                name: "IX_sinhViens_LopId",
                table: "sinhViens",
                column: "LopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sinhViens");

            migrationBuilder.DropTable(
                name: "khoaHocs");

            migrationBuilder.DropTable(
                name: "lopHocs");

            migrationBuilder.DropTable(
                name: "monHocs");

            migrationBuilder.DropTable(
                name: "giangViens");
        }
    }
}
