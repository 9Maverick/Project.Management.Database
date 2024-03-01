using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagement.Database.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class initial : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Projects",
				columns: table => new
				{
					Id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Sequence = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Projects", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Teams",
				columns: table => new
				{
					Id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ParentId = table.Column<long>(type: "bigint", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Teams", x => x.Id);
					table.ForeignKey(
						name: "FK_Teams_Teams_ParentId",
						column: x => x.ParentId,
						principalTable: "Teams",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new
				{
					Id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "TeamUser",
				columns: table => new
				{
					TeamsId = table.Column<long>(type: "bigint", nullable: false),
					UsersId = table.Column<long>(type: "bigint", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TeamUser", x => new { x.TeamsId, x.UsersId });
					table.ForeignKey(
						name: "FK_TeamUser_Teams_TeamsId",
						column: x => x.TeamsId,
						principalTable: "Teams",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_TeamUser_Users_UsersId",
						column: x => x.UsersId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Tickets",
				columns: table => new
				{
					Id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ProjectId = table.Column<long>(type: "bigint", nullable: false),
					CreatorId = table.Column<long>(type: "bigint", nullable: false),
					AssigneeId = table.Column<long>(type: "bigint", nullable: true),
					ParentId = table.Column<long>(type: "bigint", nullable: true),
					Type = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Story"),
					Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Priority = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "None"),
					StoryPoints = table.Column<int>(type: "int", nullable: false),
					Status = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Open"),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Tickets", x => x.Id);
					table.ForeignKey(
						name: "FK_Tickets_Projects_ProjectId",
						column: x => x.ProjectId,
						principalTable: "Projects",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Tickets_Tickets_ParentId",
						column: x => x.ParentId,
						principalTable: "Tickets",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_Tickets_Users_AssigneeId",
						column: x => x.AssigneeId,
						principalTable: "Users",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_Tickets_Users_CreatorId",
						column: x => x.CreatorId,
						principalTable: "Users",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "Comments",
				columns: table => new
				{
					Id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<long>(type: "bigint", nullable: false),
					TicketId = table.Column<long>(type: "bigint", nullable: false),
					Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Comments", x => x.Id);
					table.ForeignKey(
						name: "FK_Comments_Tickets_TicketId",
						column: x => x.TicketId,
						principalTable: "Tickets",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Comments_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "TicketTicket",
				columns: table => new
				{
					LinkedFromId = table.Column<long>(type: "bigint", nullable: false),
					LinkedToId = table.Column<long>(type: "bigint", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TicketTicket", x => new { x.LinkedFromId, x.LinkedToId });
					table.ForeignKey(
						name: "FK_TicketTicket_Tickets_LinkedFromId",
						column: x => x.LinkedFromId,
						principalTable: "Tickets",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_TicketTicket_Tickets_LinkedToId",
						column: x => x.LinkedToId,
						principalTable: "Tickets",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "TicketUser",
				columns: table => new
				{
					TrackingTasksId = table.Column<long>(type: "bigint", nullable: false),
					TrackingUsersId = table.Column<long>(type: "bigint", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TicketUser", x => new { x.TrackingTasksId, x.TrackingUsersId });
					table.ForeignKey(
						name: "FK_TicketUser_Tickets_TrackingTasksId",
						column: x => x.TrackingTasksId,
						principalTable: "Tickets",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_TicketUser_Users_TrackingUsersId",
						column: x => x.TrackingUsersId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Comments_TicketId",
				table: "Comments",
				column: "TicketId");

			migrationBuilder.CreateIndex(
				name: "IX_Comments_UserId",
				table: "Comments",
				column: "UserId",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Teams_ParentId",
				table: "Teams",
				column: "ParentId");

			migrationBuilder.CreateIndex(
				name: "IX_TeamUser_UsersId",
				table: "TeamUser",
				column: "UsersId");

			migrationBuilder.CreateIndex(
				name: "IX_Tickets_AssigneeId",
				table: "Tickets",
				column: "AssigneeId");

			migrationBuilder.CreateIndex(
				name: "IX_Tickets_CreatorId",
				table: "Tickets",
				column: "CreatorId");

			migrationBuilder.CreateIndex(
				name: "IX_Tickets_ParentId",
				table: "Tickets",
				column: "ParentId");

			migrationBuilder.CreateIndex(
				name: "IX_Tickets_ProjectId",
				table: "Tickets",
				column: "ProjectId");

			migrationBuilder.CreateIndex(
				name: "IX_TicketTicket_LinkedToId",
				table: "TicketTicket",
				column: "LinkedToId");

			migrationBuilder.CreateIndex(
				name: "IX_TicketUser_TrackingUsersId",
				table: "TicketUser",
				column: "TrackingUsersId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Comments");

			migrationBuilder.DropTable(
				name: "TeamUser");

			migrationBuilder.DropTable(
				name: "TicketTicket");

			migrationBuilder.DropTable(
				name: "TicketUser");

			migrationBuilder.DropTable(
				name: "Teams");

			migrationBuilder.DropTable(
				name: "Tickets");

			migrationBuilder.DropTable(
				name: "Projects");

			migrationBuilder.DropTable(
				name: "Users");
		}
	}
}
