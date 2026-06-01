using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookBase.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bookbase");

            migrationBuilder.CreateTable(
                name: "authors",
                schema: "bookbase",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    pseudonym = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_authors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "book_covers",
                schema: "bookbase",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_book_covers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "book_types",
                schema: "bookbase",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_book_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "genres",
                schema: "bookbase",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_genres", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "publishers",
                schema: "bookbase",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publishers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "bookbase",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "bookbase",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: true),
                    password_hash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "books",
                schema: "bookbase",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    publication_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    publisher_id = table.Column<Guid>(type: "uuid", nullable: false),
                    book_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    book_cover_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_books", x => x.id);
                    table.ForeignKey(
                        name: "fk_books_authors_author_id",
                        column: x => x.author_id,
                        principalSchema: "bookbase",
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_books_book_covers_book_cover_id",
                        column: x => x.book_cover_id,
                        principalSchema: "bookbase",
                        principalTable: "book_covers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_books_book_types_book_type_id",
                        column: x => x.book_type_id,
                        principalSchema: "bookbase",
                        principalTable: "book_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_books_publishers_publisher_id",
                        column: x => x.publisher_id,
                        principalSchema: "bookbase",
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                schema: "bookbase",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "bookbase",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "bookbase",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "book_genres",
                schema: "bookbase",
                columns: table => new
                {
                    book_id = table.Column<Guid>(type: "uuid", nullable: false),
                    genre_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_book_genres", x => new { x.book_id, x.genre_id });
                    table.ForeignKey(
                        name: "fk_book_genres_books_book_id",
                        column: x => x.book_id,
                        principalSchema: "bookbase",
                        principalTable: "books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_book_genres_genres_genre_id",
                        column: x => x.genre_id,
                        principalSchema: "bookbase",
                        principalTable: "genres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_authors_pseudonym",
                schema: "bookbase",
                table: "authors",
                column: "pseudonym",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_book_covers_type",
                schema: "bookbase",
                table: "book_covers",
                column: "type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_book_genres_genre_id",
                schema: "bookbase",
                table: "book_genres",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "ix_book_types_name",
                schema: "bookbase",
                table: "book_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_books_author_id",
                schema: "bookbase",
                table: "books",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_books_book_cover_id",
                schema: "bookbase",
                table: "books",
                column: "book_cover_id");

            migrationBuilder.CreateIndex(
                name: "ix_books_book_type_id",
                schema: "bookbase",
                table: "books",
                column: "book_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_books_publisher_id",
                schema: "bookbase",
                table: "books",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "ix_books_title_author_id",
                schema: "bookbase",
                table: "books",
                columns: new[] { "title", "author_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_genres_name",
                schema: "bookbase",
                table: "genres",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_publishers_name",
                schema: "bookbase",
                table: "publishers",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_roles_name",
                schema: "bookbase",
                table: "roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_role_id",
                schema: "bookbase",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                schema: "bookbase",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_username",
                schema: "bookbase",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "book_genres",
                schema: "bookbase");

            migrationBuilder.DropTable(
                name: "user_roles",
                schema: "bookbase");

            migrationBuilder.DropTable(
                name: "books",
                schema: "bookbase");

            migrationBuilder.DropTable(
                name: "genres",
                schema: "bookbase");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "bookbase");

            migrationBuilder.DropTable(
                name: "users",
                schema: "bookbase");

            migrationBuilder.DropTable(
                name: "authors",
                schema: "bookbase");

            migrationBuilder.DropTable(
                name: "book_covers",
                schema: "bookbase");

            migrationBuilder.DropTable(
                name: "book_types",
                schema: "bookbase");

            migrationBuilder.DropTable(
                name: "publishers",
                schema: "bookbase");
        }
    }
}
