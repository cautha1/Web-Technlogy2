using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AOWebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    addressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    addressLine = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    suburb = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    postcode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    region = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    country = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addressID", x => x.addressID);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategories",
                columns: table => new
                {
                    categoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parentCategoryID = table.Column<int>(type: "int", nullable: true),
                    categoryName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_itemCategories", x => x.categoryID);
                    table.ForeignKey(
                        name: "fk_parentCategory",
                        column: x => x.parentCategoryID,
                        principalTable: "ItemCategories",
                        principalColumn: "categoryID");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    customerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    lastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    mainPhoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    secondaryPhoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    addressID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customers", x => x.customerID);
                    table.ForeignKey(
                        name: "fk_address",
                        column: x => x.addressID,
                        principalTable: "Addresses",
                        principalColumn: "addressID");
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    itemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itemName = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    itemDescription = table.Column<string>(type: "varchar(5000)", unicode: false, maxLength: 5000, nullable: false),
                    itemCost = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    itemImage = table.Column<string>(type: "varchar(5000)", unicode: false, maxLength: 5000, nullable: false),
                    categoryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_itemID", x => x.itemID);
                    table.ForeignKey(
                        name: "fk_itemCategory",
                        column: x => x.categoryID,
                        principalTable: "ItemCategories",
                        principalColumn: "categoryID");
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrders",
                columns: table => new
                {
                    orderNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    orderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    datePaid = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orderNumber", x => x.orderNumber);
                    table.ForeignKey(
                        name: "fk_customerID",
                        column: x => x.customerID,
                        principalTable: "Customers",
                        principalColumn: "customerID");
                });

            migrationBuilder.CreateTable(
                name: "ItemMarkupHistory",
                columns: table => new
                {
                    itemID = table.Column<int>(type: "int", nullable: false),
                    startDate = table.Column<DateOnly>(type: "date", nullable: false),
                    endDate = table.Column<DateOnly>(type: "date", nullable: true),
                    markup = table.Column<decimal>(type: "decimal(4,1)", nullable: false, defaultValue: 1.3m),
                    sale = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMarkupHistory", x => new { x.itemID, x.startDate });
                    table.ForeignKey(
                        name: "FK_ItemMarkupHistory_Items",
                        column: x => x.itemID,
                        principalTable: "Items",
                        principalColumn: "itemID");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    reviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    reviewDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    itemID = table.Column<int>(type: "int", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    reviewDescription = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.reviewID);
                    table.ForeignKey(
                        name: "fk_customer_review",
                        column: x => x.customerID,
                        principalTable: "Customers",
                        principalColumn: "customerID");
                    table.ForeignKey(
                        name: "fk_item_review",
                        column: x => x.itemID,
                        principalTable: "Items",
                        principalColumn: "itemID");
                });

            migrationBuilder.CreateTable(
                name: "ItemsInOrder",
                columns: table => new
                {
                    orderNumber = table.Column<int>(type: "int", nullable: false),
                    itemID = table.Column<int>(type: "int", nullable: false),
                    numberOf = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    totalItemCost = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_itemsInOrder", x => new { x.orderNumber, x.itemID });
                    table.ForeignKey(
                        name: "fk_items",
                        column: x => x.itemID,
                        principalTable: "Items",
                        principalColumn: "itemID");
                    table.ForeignKey(
                        name: "fk_orderNumber",
                        column: x => x.orderNumber,
                        principalTable: "CustomerOrders",
                        principalColumn: "orderNumber");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_customerID",
                table: "CustomerOrders",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_addressID",
                table: "Customers",
                column: "addressID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategories_parentCategoryID",
                table: "ItemCategories",
                column: "parentCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_categoryID",
                table: "Items",
                column: "categoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsInOrder_itemID",
                table: "ItemsInOrder",
                column: "itemID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_customerID",
                table: "Reviews",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_itemID",
                table: "Reviews",
                column: "itemID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemMarkupHistory");

            migrationBuilder.DropTable(
                name: "ItemsInOrder");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "CustomerOrders");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "ItemCategories");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
