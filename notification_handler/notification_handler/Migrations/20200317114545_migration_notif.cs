using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace notification_handler.Migrations
{
    public partial class migration_notif : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "notif",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: true),
                    message = table.Column<string>(nullable: true),
                    created_at = table.Column<double>(nullable: false),
                    updated_at = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notif", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notiflog",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    notification_id = table.Column<int>(nullable: false),
                    type = table.Column<string>(nullable: true),
                    from = table.Column<int>(nullable: false),
                    target = table.Column<int>(nullable: false),
                    email_destination = table.Column<string>(nullable: true),
                    read_at = table.Column<double>(nullable: false),
                    created_at = table.Column<double>(nullable: false),
                    updated_at = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notiflog", x => x.id);
                    table.ForeignKey(
                        name: "FK_notiflog_notif_notification_id",
                        column: x => x.notification_id,
                        principalTable: "notif",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_notiflog_notification_id",
                table: "notiflog",
                column: "notification_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notiflog");

            migrationBuilder.DropTable(
                name: "notif");
        }
    }
}
