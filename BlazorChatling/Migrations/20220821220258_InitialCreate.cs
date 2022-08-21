using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorChatling.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    tag = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Chat_User",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "int", nullable: false),
                    id_chat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__Chat_User__id_ch__3A81B327",
                        column: x => x.id_chat,
                        principalTable: "Chats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Chat_User__id_us__398D8EEE",
                        column: x => x.id_user,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_chat = table.Column<int>(type: "int", nullable: false),
                    id_user = table.Column<int>(type: "int", nullable: false),
                    id_reply_message = table.Column<int>(type: "int", nullable: true),
                    id_reply_user = table.Column<int>(type: "int", nullable: true),
                    msg_text = table.Column<string>(type: "text", nullable: false),
                    msg_time = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    is_edited = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "('FALSE')"),
                    is_visible_to_creator = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "('TRUE')"),
                    is_reply_visible_to_group = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "('TRUE')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.id);
                    table.ForeignKey(
                        name: "FK__Messages__id_cha__5812160E",
                        column: x => x.id_chat,
                        principalTable: "Chats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Messages__id_rep__571DF1D5",
                        column: x => x.id_reply_message,
                        principalTable: "Messages",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Messages__id_use__5629CD9C",
                        column: x => x.id_user,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chat_User_id_chat",
                table: "Chat_User",
                column: "id_chat");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_User_id_user",
                table: "Chat_User",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_id_chat",
                table: "Messages",
                column: "id_chat");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_id_reply_message",
                table: "Messages",
                column: "id_reply_message");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_id_user",
                table: "Messages",
                column: "id_user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chat_User");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
