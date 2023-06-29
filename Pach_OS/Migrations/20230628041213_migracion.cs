using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pach_OS.Migrations
{
    public partial class migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            //migrationBuilder.CreateTable(
            //    name: "categorias",
            //    columns: table => new
            //    {
            //        id_categoria = table.Column<int>(type: "int", nullable: false),
            //        nomCategoria = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__categori__CD54BC5A68905BA1", x => x.id_categoria);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "insumos",
            //    columns: table => new
            //    {
            //        id_insumos = table.Column<int>(type: "int", nullable: false),
            //        nom_insumo = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
            //        cant_insumo = table.Column<int>(type: "int", unicode: false, maxLength: 30, nullable: true),
            //        proveedores_id = table.Column<int>(type: "int", nullable: true),
            //        tiempo_llegado = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__insumos__B76055AED687714B", x => x.id_insumos);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "roles",
            //    columns: table => new
            //    {
            //        id_rol = table.Column<byte>(type: "tinyint", nullable: false),
            //        nom_rol = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__roles__6ABCB5E0C26F1690", x => x.id_rol);
            //    });

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

            //migrationBuilder.CreateTable(
            //    name: "productos",
            //    columns: table => new
            //    {
            //        id_productos = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        nom_producto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        precio_venta = table.Column<int>(type: "int", nullable: true),
            //        estado = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        categoria_id = table.Column<int>(type: "int", nullable: true),
            //        tamano_pizza = table.Column<byte>(type: "tinyint", nullable: true),
            //        maximo_sabores = table.Column<byte>(type: "tinyint", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__producto__3804F4FB38FC97EE", x => x.id_productos);
            //        table.ForeignKey(
            //            name: "FK__productos__categ__4F7CD00D",
            //            column: x => x.categoria_id,
            //            principalTable: "categorias",
            //            principalColumn: "id_categoria");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "proveedores",
            //    columns: table => new
            //    {
            //        id_proveedor = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        nit = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
            //        nom_local = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
            //        insumos_id = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__proveedo__8D3DFE283F237B5C", x => x.id_proveedor);
            //        table.ForeignKey(
            //            name: "FK__proveedor__insum__52593CB8",
            //            column: x => x.insumos_id,
            //            principalTable: "insumos",
            //            principalColumn: "id_insumos");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "usuarios",
            //    columns: table => new
            //    {
            //        id_usuario = table.Column<int>(type: "int", nullable: false),
            //        tipo_documento = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
            //        num_documento = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
            //        nombre = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
            //        apellido = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
            //        contrasena = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
            //        celular = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
            //        correo = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
            //        rol_id = table.Column<byte>(type: "tinyint", nullable: true),
            //        estado = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
            //        fecha_entrada = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__usuarios__4E3E04ADDB368B9D", x => x.id_usuario);
            //        table.ForeignKey(
            //            name: "FK__usuarios__rol_id__534D60F1",
            //            column: x => x.rol_id,
            //            principalTable: "roles",
            //            principalColumn: "id_rol");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "productos_insumos",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        productosId = table.Column<int>(type: "int", nullable: true),
            //        cant_insumo = table.Column<int>(type: "int", nullable: true),
            //        insumosID = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__producto__3214EC27EBD61552", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__productos__insum__60A75C0F",
            //            column: x => x.insumosID,
            //            principalTable: "insumos",
            //            principalColumn: "id_insumos");
            //        table.ForeignKey(
            //            name: "FK__productos__produ__5FB337D6",
            //            column: x => x.productosId,
            //            principalTable: "productos",
            //            principalColumn: "id_productos");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "compras",
            //    columns: table => new
            //    {
            //        id_compras = table.Column<int>(type: "int", nullable: false),
            //        id_usuario = table.Column<int>(type: "int", nullable: true),
            //        fecha_compra = table.Column<DateTime>(type: "datetime", nullable: true),
            //        total = table.Column<int>(type: "int", nullable: true),
            //        id_proveedor = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__compras__59093490D7BBFFB7", x => x.id_compras);
            //        table.ForeignKey(
            //            name: "FK__compras__id_usua__49C3F6B7",
            //            column: x => x.id_usuario,
            //            principalTable: "usuarios",
            //            principalColumn: "id_usuario");
            //        table.ForeignKey(
            //            name: "FK_compras_proveedores",
            //            column: x => x.id_proveedor,
            //            principalTable: "proveedores",
            //            principalColumn: "id_proveedor");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ventas",
            //    columns: table => new
            //    {
            //        id_ventas = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        usuario_id = table.Column<int>(type: "int", nullable: true),
            //        fecha_venta = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
            //        total_venta = table.Column<int>(type: "int", nullable: true),
            //        tipo_pago = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
            //        pago = table.Column<int>(type: "int", nullable: true),
            //        pago_domicilio = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__ventas__283C869DAE168DF8", x => x.id_ventas);
            //        table.ForeignKey(
            //            name: "FK__ventas__usuario___7E37BEF6",
            //            column: x => x.usuario_id,
            //            principalTable: "usuarios",
            //            principalColumn: "id_usuario");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Detalles_Compras",
            //    columns: table => new
            //    {
            //        id_detalles_compras = table.Column<int>(type: "int", nullable: false),
            //        compras_id = table.Column<int>(type: "int", nullable: true),
            //        insumos_id = table.Column<int>(type: "int", nullable: true),
            //        precio_insumo = table.Column<int>(type: "int", nullable: true),
            //        cantidad = table.Column<byte>(type: "tinyint", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Detalles__8D095AC36C3F477E", x => x.id_detalles_compras);
            //        table.ForeignKey(
            //            name: "FK__Detalles___compr__4BAC3F29",
            //            column: x => x.compras_id,
            //            principalTable: "compras",
            //            principalColumn: "id_compras");
            //        table.ForeignKey(
            //            name: "FK__Detalles___insum__4CA06362",
            //            column: x => x.insumos_id,
            //            principalTable: "insumos",
            //            principalColumn: "id_insumos");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "detalleVentas",
            //    columns: table => new
            //    {
            //        id_detalleVenta = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        venta_id = table.Column<int>(type: "int", nullable: true),
            //        cant_vendida = table.Column<int>(type: "int", nullable: true),
            //        productos_id = table.Column<int>(type: "int", nullable: true),
            //        precio = table.Column<int>(type: "int", nullable: true),
            //        totalPrecio = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__detalleV__3C2E445CA143D433", x => x.id_detalleVenta);
            //        table.ForeignKey(
            //            name: "FK__detalleVe__venta__778AC167",
            //            column: x => x.venta_id,
            //            principalTable: "ventas",
            //            principalColumn: "id_ventas");
            //        table.ForeignKey(
            //            name: "FK_detalleVentas_productos",
            //            column: x => x.productos_id,
            //            principalTable: "productos",
            //            principalColumn: "id_productos");
            //    });

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

            //migrationBuilder.CreateIndex(
            //    name: "IX_compras_id_proveedor",
            //    table: "compras",
            //    column: "id_proveedor");

            //migrationBuilder.CreateIndex(
            //    name: "IX_compras_id_usuario",
            //    table: "compras",
            //    column: "id_usuario");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Detalles_Compras_compras_id",
            //    table: "Detalles_Compras",
            //    column: "compras_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Detalles_Compras_insumos_id",
            //    table: "Detalles_Compras",
            //    column: "insumos_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_detalleVentas_productos_id",
            //    table: "detalleVentas",
            //    column: "productos_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_detalleVentas_venta_id",
            //    table: "detalleVentas",
            //    column: "venta_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_productos_categoria_id",
            //    table: "productos",
            //    column: "categoria_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_productos_insumos_insumosID",
            //    table: "productos_insumos",
            //    column: "insumosID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_productos_insumos_productosId",
            //    table: "productos_insumos",
            //    column: "productosId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_proveedores_insumos_id",
            //    table: "proveedores",
            //    column: "insumos_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_usuarios_rol_id",
            //    table: "usuarios",
            //    column: "rol_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ventas_usuario_id",
            //    table: "ventas",
            //    column: "usuario_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            //migrationBuilder.DropTable(
            //    name: "Detalles_Compras");

            //migrationBuilder.DropTable(
            //    name: "detalleVentas");

            //migrationBuilder.DropTable(
            //    name: "productos_insumos");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            //migrationBuilder.DropTable(
            //    name: "compras");

            //migrationBuilder.DropTable(
            //    name: "ventas");

            //migrationBuilder.DropTable(
            //    name: "productos");

            //migrationBuilder.DropTable(
            //    name: "proveedores");

            //migrationBuilder.DropTable(
            //    name: "usuarios");

            //migrationBuilder.DropTable(
            //    name: "categorias");

            //migrationBuilder.DropTable(
            //    name: "insumos");

            //migrationBuilder.DropTable(
            //    name: "roles");
        }
    }
}
