using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations;

public partial class ApplicationUser : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Categories_Users_UserId",
            table: "Categories");

        migrationBuilder.DropForeignKey(
            name: "FK_RoleUser_Roles_RolesId",
            table: "RoleUser");

        migrationBuilder.DropForeignKey(
            name: "FK_RoleUser_Users_UsersId",
            table: "RoleUser");

        migrationBuilder.DropForeignKey(
            name: "FK_Transactions_Users_UserId",
            table: "Transactions");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Users",
            table: "Users");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Roles",
            table: "Roles");

        migrationBuilder.RenameTable(
            name: "Users",
            newName: "User");

        migrationBuilder.RenameTable(
            name: "Roles",
            newName: "Role");

        migrationBuilder.AlterColumn<string>(
            name: "Username",
            table: "User",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(128)",
            oldMaxLength: 128);

        migrationBuilder.AlterColumn<string>(
            name: "PasswordSalt",
            table: "User",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(128)",
            oldMaxLength: 128);

        migrationBuilder.AlterColumn<string>(
            name: "PasswordHash",
            table: "User",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(128)",
            oldMaxLength: 128);

        migrationBuilder.AlterColumn<string>(
            name: "LastName",
            table: "User",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(128)",
            oldMaxLength: 128);

        migrationBuilder.AlterColumn<string>(
            name: "FirstName",
            table: "User",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(128)",
            oldMaxLength: 128);

        migrationBuilder.AlterColumn<string>(
            name: "Email",
            table: "User",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(128)",
            oldMaxLength: 128);

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Role",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(32)",
            oldMaxLength: 32);

        migrationBuilder.AddPrimaryKey(
            name: "PK_User",
            table: "User",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Role",
            table: "Role",
            column: "Id");

        migrationBuilder.CreateTable(
            name: "AspNetRoles",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUsers",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                AccessFailedCount = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "DeviceCodes",
            columns: table => new
            {
                UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
            });

        migrationBuilder.CreateTable(
            name: "PersistedGrants",
            columns: table => new
            {
                Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PersistedGrants", x => x.Key);
            });

        migrationBuilder.CreateTable(
            name: "AspNetRoleClaims",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "AspNetRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserClaims",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserLogins",
            columns: table => new
            {
                LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                table.ForeignKey(
                    name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserRoles",
            columns: table => new
            {
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "AspNetRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserTokens",
            columns: table => new
            {
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                table.ForeignKey(
                    name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_AspNetRoleClaims_RoleId",
            table: "AspNetRoleClaims",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            table: "AspNetRoles",
            column: "NormalizedName",
            unique: true,
            filter: "[NormalizedName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserClaims_UserId",
            table: "AspNetUserClaims",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserLogins_UserId",
            table: "AspNetUserLogins",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserRoles_RoleId",
            table: "AspNetUserRoles",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "EmailIndex",
            table: "AspNetUsers",
            column: "NormalizedEmail");

        migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            table: "AspNetUsers",
            column: "NormalizedUserName",
            unique: true,
            filter: "[NormalizedUserName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_DeviceCodes_DeviceCode",
            table: "DeviceCodes",
            column: "DeviceCode",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_DeviceCodes_Expiration",
            table: "DeviceCodes",
            column: "Expiration");

        migrationBuilder.CreateIndex(
            name: "IX_PersistedGrants_Expiration",
            table: "PersistedGrants",
            column: "Expiration");

        migrationBuilder.CreateIndex(
            name: "IX_PersistedGrants_SubjectId_ClientId_Type",
            table: "PersistedGrants",
            columns: new[] { "SubjectId", "ClientId", "Type" });

        migrationBuilder.CreateIndex(
            name: "IX_PersistedGrants_SubjectId_SessionId_Type",
            table: "PersistedGrants",
            columns: new[] { "SubjectId", "SessionId", "Type" });

        migrationBuilder.AddForeignKey(
            name: "FK_Categories_User_UserId",
            table: "Categories",
            column: "UserId",
            principalTable: "User",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_RoleUser_Role_RolesId",
            table: "RoleUser",
            column: "RolesId",
            principalTable: "Role",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_RoleUser_User_UsersId",
            table: "RoleUser",
            column: "UsersId",
            principalTable: "User",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Transactions_User_UserId",
            table: "Transactions",
            column: "UserId",
            principalTable: "User",
            principalColumn: "Id");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Categories_User_UserId",
            table: "Categories");

        migrationBuilder.DropForeignKey(
            name: "FK_RoleUser_Role_RolesId",
            table: "RoleUser");

        migrationBuilder.DropForeignKey(
            name: "FK_RoleUser_User_UsersId",
            table: "RoleUser");

        migrationBuilder.DropForeignKey(
            name: "FK_Transactions_User_UserId",
            table: "Transactions");

        migrationBuilder.DropTable(
            name: "AspNetRoleClaims");

        migrationBuilder.DropTable(
            name: "AspNetUserClaims");

        migrationBuilder.DropTable(
            name: "AspNetUserLogins");

        migrationBuilder.DropTable(
            name: "AspNetUserRoles");

        migrationBuilder.DropTable(
            name: "AspNetUserTokens");

        migrationBuilder.DropTable(
            name: "DeviceCodes");

        migrationBuilder.DropTable(
            name: "PersistedGrants");

        migrationBuilder.DropTable(
            name: "AspNetRoles");

        migrationBuilder.DropTable(
            name: "AspNetUsers");

        migrationBuilder.DropPrimaryKey(
            name: "PK_User",
            table: "User");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Role",
            table: "Role");

        migrationBuilder.RenameTable(
            name: "User",
            newName: "Users");

        migrationBuilder.RenameTable(
            name: "Role",
            newName: "Roles");

        migrationBuilder.AlterColumn<string>(
            name: "Username",
            table: "Users",
            type: "nvarchar(128)",
            maxLength: 128,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "PasswordSalt",
            table: "Users",
            type: "nvarchar(128)",
            maxLength: 128,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "PasswordHash",
            table: "Users",
            type: "nvarchar(128)",
            maxLength: 128,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "LastName",
            table: "Users",
            type: "nvarchar(128)",
            maxLength: 128,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "FirstName",
            table: "Users",
            type: "nvarchar(128)",
            maxLength: 128,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Email",
            table: "Users",
            type: "nvarchar(128)",
            maxLength: 128,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Roles",
            type: "nvarchar(32)",
            maxLength: 32,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AddPrimaryKey(
            name: "PK_Users",
            table: "Users",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Roles",
            table: "Roles",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Categories_Users_UserId",
            table: "Categories",
            column: "UserId",
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_RoleUser_Roles_RolesId",
            table: "RoleUser",
            column: "RolesId",
            principalTable: "Roles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_RoleUser_Users_UsersId",
            table: "RoleUser",
            column: "UsersId",
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Transactions_Users_UserId",
            table: "Transactions",
            column: "UserId",
            principalTable: "Users",
            principalColumn: "Id");
    }
}
