using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WeiHeMuStore.Model.Migrations
{
    public partial class initMySql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductCates",
                columns: table => new
                {
                    ProductCateId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductCateName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCates", x => x.ProductCateId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductCatesProductCateId = table.Column<int>(nullable: true),
                    ProductImage = table.Column<string>(maxLength: 150, nullable: true),
                    ProductIntroduce = table.Column<string>(maxLength: 150, nullable: true),
                    ProductName = table.Column<string>(maxLength: 100, nullable: true),
                    ProductPrice = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_ProductCates_ProductCatesProductCateId",
                        column: x => x.ProductCatesProductCateId,
                        principalTable: "ProductCates",
                        principalColumn: "ProductCateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCatesProductCateId",
                table: "Products",
                column: "ProductCatesProductCateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductCates");
        }
    }
}
