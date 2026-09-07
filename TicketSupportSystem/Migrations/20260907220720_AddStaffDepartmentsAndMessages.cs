using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TicketSupportSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddStaffDepartmentsAndMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainSignedIn",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "AssignedTo",
                table: "Tickets",
                newName: "AssignedToStaffID");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Tickets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DisplayName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: true),
                    Signature = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.StaffID);
                    table.CheckConstraint("CK_Staff_LevelOnlyForAdvisors", "\"Level\" IS NULL OR \"Role\" = 0");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Response = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    AttachmentFilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    TicketID = table.Column<int>(type: "integer", nullable: false),
                    ResponseByUserID = table.Column<int>(type: "integer", nullable: true),
                    ResponseByStaffID = table.Column<int>(type: "integer", nullable: true),
                    PostedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsInternal = table.Column<bool>(type: "boolean", nullable: false),
                    IPAddress = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageID);
                    table.CheckConstraint("CK_Message_ExactlyOneAuthor", "(\"ResponseByUserID\" IS NULL) <> (\"ResponseByStaffID\" IS NULL)");
                    table.ForeignKey(
                        name: "FK_Messages_Staff_ResponseByStaffID",
                        column: x => x.ResponseByStaffID,
                        principalTable: "Staff",
                        principalColumn: "StaffID");
                    table.ForeignKey(
                        name: "FK_Messages_Tickets_TicketID",
                        column: x => x.TicketID,
                        principalTable: "Tickets",
                        principalColumn: "TicketID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_ResponseByUserID",
                        column: x => x.ResponseByUserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssignedToStaffID",
                table: "Tickets",
                column: "AssignedToStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DepartmentID",
                table: "Tickets",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ResponseByStaffID",
                table: "Messages",
                column: "ResponseByStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ResponseByUserID",
                table: "Messages",
                column: "ResponseByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_TicketID",
                table: "Messages",
                column: "TicketID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Departments_DepartmentID",
                table: "Tickets",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "DepartmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Staff_AssignedToStaffID",
                table: "Tickets",
                column: "AssignedToStaffID",
                principalTable: "Staff",
                principalColumn: "StaffID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Departments_DepartmentID",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Staff_AssignedToStaffID",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_AssignedToStaffID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_DepartmentID",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "AssignedToStaffID",
                table: "Tickets",
                newName: "AssignedTo");

            migrationBuilder.AddColumn<bool>(
                name: "RemainSignedIn",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
