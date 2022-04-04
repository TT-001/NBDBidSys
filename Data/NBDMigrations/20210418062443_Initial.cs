using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NBD_TractionFive.Data.NBDMigrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    cliID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cliName = table.Column<string>(maxLength: 50, nullable: false),
                    cliContactName = table.Column<string>(maxLength: 50, nullable: false),
                    clicontactType = table.Column<string>(maxLength: 50, nullable: true),
                    cliPhonenumber = table.Column<long>(nullable: false),
                    Street = table.Column<string>(maxLength: 100, nullable: false),
                    cliCity = table.Column<string>(maxLength: 50, nullable: false),
                    Province = table.Column<int>(nullable: false),
                    ZipCode = table.Column<string>(maxLength: 10, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.cliID);
                });


            migrationBuilder.Sql(
                @"
                CREATE TRIGGER SetClientTimestampOnUpdate
                AFTER UPDATE ON Clients
                BEGIN
                    UPDATE Clients
                    SET RowVersion = randomblob(8)
                    WHERE rowid = NEW.rowid;
                END
            ");
            migrationBuilder.Sql(
                @"
                CREATE TRIGGER SetClientTimestampOnInsert
                AFTER INSERT ON Clients
                BEGIN
                    UPDATE Clients
                    SET RowVersion = randomblob(8)
                    WHERE rowid = NEW.rowid;
                END
            ");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    empID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    empFirst = table.Column<string>(maxLength: 50, nullable: false),
                    empLast = table.Column<string>(maxLength: 50, nullable: false),
                    empPhoneNumber = table.Column<long>(nullable: false),
                    eMail = table.Column<string>(maxLength: 255, nullable: false),
                    empType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.empID);
                });

            migrationBuilder.CreateTable(
                name: "inventoryVMs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    quantityOnHands = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    price = table.Column<decimal>(nullable: false),
                    type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventoryVMs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InvTypes",
                columns: table => new
                {
                    itID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    itType = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvTypes", x => x.itID);
                });

            migrationBuilder.CreateTable(
                name: "Labours",
                columns: table => new
                {
                    labID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    labDescription = table.Column<string>(maxLength: 500, nullable: false),
                    labPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labours", x => x.labID);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    projID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    projBeginDate = table.Column<DateTime>(nullable: false),
                    projCompletionDate = table.Column<DateTime>(nullable: true),
                    projSite = table.Column<string>(maxLength: 200, nullable: false),
                    projCity = table.Column<string>(maxLength: 50, nullable: false),
                    Province = table.Column<int>(nullable: false),
                    ZipCode = table.Column<string>(maxLength: 10, nullable: false),
                    cliID = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.projID);
                    table.ForeignKey(
                        name: "FK_Projects_Clients_cliID",
                        column: x => x.cliID,
                        principalTable: "Clients",
                        principalColumn: "cliID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.Sql(
                @"
                CREATE TRIGGER SetProjectTimestampOnUpdate
                AFTER UPDATE ON Projects
                BEGIN
                    UPDATE Projects
                    SET RowVersion = randomblob(8)
                    WHERE rowid = NEW.rowid;
                END
            ");
            migrationBuilder.Sql(
                @"
                CREATE TRIGGER SetProjectTimestampOnInsert
                AFTER INSERT ON Projects
                BEGIN
                    UPDATE Projects
                    SET RowVersion = randomblob(8)
                    WHERE rowid = NEW.rowid;
                END
            ");

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    invID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    invName = table.Column<string>(maxLength: 50, nullable: false),
                    invSize = table.Column<string>(maxLength: 50, nullable: false),
                    invPrice = table.Column<double>(nullable: false),
                    itID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.invID);
                    table.ForeignKey(
                        name: "FK_Inventories_InvTypes_itID",
                        column: x => x.itID,
                        principalTable: "InvTypes",
                        principalColumn: "itID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    bidID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    bidDate = table.Column<DateTime>(nullable: false),
                    bidAmount = table.Column<decimal>(nullable: false),
                    projID = table.Column<int>(nullable: false),
                    SalesID = table.Column<int>(nullable: false),
                    DesignerID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    BidState = table.Column<int>(nullable: false),
                    employeeempID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.bidID);
                    table.ForeignKey(
                        name: "FK_Bids_Employees_DesignerID",
                        column: x => x.DesignerID,
                        principalTable: "Employees",
                        principalColumn: "empID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bids_Employees_SalesID",
                        column: x => x.SalesID,
                        principalTable: "Employees",
                        principalColumn: "empID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bids_Employees_employeeempID",
                        column: x => x.employeeempID,
                        principalTable: "Employees",
                        principalColumn: "empID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bids_Projects_projID",
                        column: x => x.projID,
                        principalTable: "Projects",
                        principalColumn: "projID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.Sql(
                @"
                CREATE TRIGGER SetBidTimestampOnUpdate
                AFTER UPDATE ON Bids
                BEGIN
                    UPDATE Bids
                    SET RowVersion = randomblob(8)
                    WHERE rowid = NEW.rowid;
                END
            ");
            migrationBuilder.Sql(
                @"
                CREATE TRIGGER SetBidTimestampOnInsert
                AFTER INSERT ON Bids
                BEGIN
                    UPDATE Bids
                    SET RowVersion = randomblob(8)
                    WHERE rowid = NEW.rowid;
                END
            ");

            migrationBuilder.CreateTable(
                name: "BidLabours",
                columns: table => new
                {
                    labID = table.Column<int>(nullable: false),
                    bidID = table.Column<int>(nullable: false),
                    blHours = table.Column<double>(nullable: false),
                    blCost = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidLabours", x => new { x.bidID, x.labID });
                    table.ForeignKey(
                        name: "FK_BidLabours_Bids_bidID",
                        column: x => x.bidID,
                        principalTable: "Bids",
                        principalColumn: "bidID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BidLabours_Labours_labID",
                        column: x => x.labID,
                        principalTable: "Labours",
                        principalColumn: "labID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BidMaterials",
                columns: table => new
                {
                    invID = table.Column<int>(nullable: false),
                    bidID = table.Column<int>(nullable: false),
                    bmQuantity = table.Column<int>(nullable: false),
                    bmCost = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidMaterials", x => new { x.bidID, x.invID });
                    table.ForeignKey(
                        name: "FK_BidMaterials_Bids_bidID",
                        column: x => x.bidID,
                        principalTable: "Bids",
                        principalColumn: "bidID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BidMaterials_Inventories_invID",
                        column: x => x.invID,
                        principalTable: "Inventories",
                        principalColumn: "invID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BidLabours_labID",
                table: "BidLabours",
                column: "labID");

            migrationBuilder.CreateIndex(
                name: "IX_BidMaterials_invID",
                table: "BidMaterials",
                column: "invID");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_DesignerID",
                table: "Bids",
                column: "DesignerID");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_SalesID",
                table: "Bids",
                column: "SalesID");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_employeeempID",
                table: "Bids",
                column: "employeeempID");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_projID",
                table: "Bids",
                column: "projID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_itID",
                table: "Inventories",
                column: "itID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_cliID",
                table: "Projects",
                column: "cliID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BidLabours");

            migrationBuilder.DropTable(
                name: "BidMaterials");

            migrationBuilder.DropTable(
                name: "inventoryVMs");

            migrationBuilder.DropTable(
                name: "Labours");

            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "InvTypes");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
