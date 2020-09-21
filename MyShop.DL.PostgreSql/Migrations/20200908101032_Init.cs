using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MyShop.DL.PostgreSql.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    SymbolLeft = table.Column<string>(nullable: true),
                    SymbolRight = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: false),
                    IsBase = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageGalleries",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageGalleries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Symbol = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupportVerifyTokens",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    TokenType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportVerifyTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    IsEmailConfirmed = table.Column<bool>(nullable: false),
                    IsPhoneConfirmed = table.Column<bool>(nullable: false),
                    UserProfileId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileEntities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true),
                    OriginalName = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    ImageGalleryId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileEntities_ImageGalleries_ImageGalleryId",
                        column: x => x.ImageGalleryId,
                        principalTable: "ImageGalleries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Alias = table.Column<string>(nullable: true),
                    ExtraTitle = table.Column<string>(nullable: true),
                    SmallDescription = table.Column<string>(nullable: true),
                    LongDescription = table.Column<string>(nullable: true),
                    ParentCategoryId = table.Column<int>(nullable: true),
                    ParentCategoryLanguageId = table.Column<int>(nullable: true),
                    ImageGalleryId = table.Column<string>(nullable: true),
                    SeoTitle = table.Column<string>(nullable: true),
                    SeoKeywords = table.Column<string>(nullable: true),
                    SeoDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => new { x.Id, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_Categories_ImageGalleries_ImageGalleryId",
                        column: x => x.ImageGalleryId,
                        principalTable: "ImageGalleries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Categories_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId_ParentCategoryLangua~",
                        columns: x => new { x.ParentCategoryId, x.ParentCategoryLanguageId },
                        principalTable: "Categories",
                        principalColumns: new[] { "Id", "LanguageId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Symbol = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => new { x.Id, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_Genders_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailExtra = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberExtra = table.Column<string>(nullable: true),
                    Manager = table.Column<string>(nullable: true),
                    ManagerExtra = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    AddressExtra = table.Column<string>(nullable: true),
                    SomeInfo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => new { x.Id, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_Suppliers_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Token = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => new { x.Id, x.Token });
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.Id, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Alias = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SeoTitle = table.Column<string>(nullable: true),
                    SeoKeywords = table.Column<string>(nullable: true),
                    SeoDescription = table.Column<string>(nullable: true),
                    FileEntityId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brands_FileEntities_FileEntityId",
                        column: x => x.FileEntityId,
                        principalTable: "FileEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    FileEntityId = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    GenderLanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_FileEntities_FileEntityId",
                        column: x => x.FileEntityId,
                        principalTable: "FileEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Genders_GenderId_GenderLanguageId",
                        columns: x => new { x.GenderId, x.GenderLanguageId },
                        principalTable: "Genders",
                        principalColumns: new[] { "Id", "LanguageId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Alias = table.Column<string>(nullable: true),
                    CurrentPrice = table.Column<decimal>(nullable: false),
                    OldPrice = table.Column<decimal>(nullable: true),
                    ItemNumber = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Specifications = table.Column<string>(nullable: true),
                    Raiting = table.Column<double>(nullable: true),
                    AmountInStock = table.Column<int>(nullable: false),
                    SeoKeywords = table.Column<string>(nullable: true),
                    SeoDescription = table.Column<string>(nullable: true),
                    SuperPrice = table.Column<int>(nullable: true),
                    TopOfSale = table.Column<int>(nullable: true),
                    New = table.Column<int>(nullable: true),
                    Share = table.Column<int>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    ImageGalleryId = table.Column<string>(nullable: true),
                    BrandId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => new { x.Id, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ImageGalleries_ImageGalleryId",
                        column: x => x.ImageGalleryId,
                        principalTable: "ImageGalleries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CategoryLanguageId = table.Column<int>(nullable: false),
                    ProductId = table.Column<string>(nullable: false),
                    ProductLanguageId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProducts", x => new { x.Id, x.CategoryLanguageId, x.ProductId, x.ProductLanguageId });
                    table.ForeignKey(
                        name: "FK_CategoryProducts_Categories_Id_CategoryLanguageId",
                        columns: x => new { x.Id, x.CategoryLanguageId },
                        principalTable: "Categories",
                        principalColumns: new[] { "Id", "LanguageId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProducts_Products_ProductId_ProductLanguageId",
                        columns: x => new { x.ProductId, x.ProductLanguageId },
                        principalTable: "Products",
                        principalColumns: new[] { "Id", "LanguageId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modifications",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    ProductLanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modifications_Products_ProductId_ProductLanguageId",
                        columns: x => new { x.ProductId, x.ProductLanguageId },
                        principalTable: "Products",
                        principalColumns: new[] { "Id", "LanguageId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ProductId = table.Column<string>(nullable: false),
                    ProductLanguageId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => new { x.Id, x.ProductId, x.ProductLanguageId });
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_Id",
                        column: x => x.Id,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId_ProductLanguageId",
                        columns: x => new { x.ProductId, x.ProductLanguageId },
                        principalTable: "Products",
                        principalColumns: new[] { "Id", "LanguageId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierProduct",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    SupplierLanguageId = table.Column<int>(nullable: false),
                    ProductId = table.Column<string>(nullable: false),
                    ProductLanguageId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ActivityStatus = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierProduct", x => new { x.Id, x.SupplierLanguageId, x.ProductId, x.ProductLanguageId });
                    table.ForeignKey(
                        name: "FK_SupplierProduct_Suppliers_Id_SupplierLanguageId",
                        columns: x => new { x.Id, x.SupplierLanguageId },
                        principalTable: "Suppliers",
                        principalColumns: new[] { "Id", "LanguageId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierProduct_Products_ProductId_ProductLanguageId",
                        columns: x => new { x.ProductId, x.ProductLanguageId },
                        principalTable: "Products",
                        principalColumns: new[] { "Id", "LanguageId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brands_FileEntityId",
                table: "Brands",
                column: "FileEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ImageGalleryId",
                table: "Categories",
                column: "ImageGalleryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_LanguageId",
                table: "Categories",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId_ParentCategoryLanguageId",
                table: "Categories",
                columns: new[] { "ParentCategoryId", "ParentCategoryLanguageId" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProducts_ProductId_ProductLanguageId",
                table: "CategoryProducts",
                columns: new[] { "ProductId", "ProductLanguageId" });

            migrationBuilder.CreateIndex(
                name: "IX_FileEntities_ImageGalleryId",
                table: "FileEntities",
                column: "ImageGalleryId");

            migrationBuilder.CreateIndex(
                name: "IX_Genders_LanguageId",
                table: "Genders",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Modifications_ProductId_ProductLanguageId",
                table: "Modifications",
                columns: new[] { "ProductId", "ProductLanguageId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId_ProductLanguageId",
                table: "OrderProducts",
                columns: new[] { "ProductId", "ProductLanguageId" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CurrencyId",
                table: "Products",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ImageGalleryId",
                table: "Products",
                column: "ImageGalleryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_LanguageId",
                table: "Products",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierProduct_ProductId_ProductLanguageId",
                table: "SupplierProduct",
                columns: new[] { "ProductId", "ProductLanguageId" });

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_LanguageId",
                table: "Suppliers",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_FileEntityId",
                table: "UserProfiles",
                column: "FileEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_LanguageId",
                table: "UserProfiles",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_GenderId_GenderLanguageId",
                table: "UserProfiles",
                columns: new[] { "GenderId", "GenderLanguageId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProducts");

            migrationBuilder.DropTable(
                name: "Modifications");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "SupplierProduct");

            migrationBuilder.DropTable(
                name: "SupportVerifyTokens");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "FileEntities");

            migrationBuilder.DropTable(
                name: "ImageGalleries");
        }
    }
}
