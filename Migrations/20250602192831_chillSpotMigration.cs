using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChillSpot.Migrations
{
    /// <inheritdoc />
    public partial class chillSpotMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Estado__3213E83F710AAB31", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RedPago",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Estado_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RedPago__3213E83F54AD491F", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    FechaRegistro = table.Column<DateOnly>(type: "date", nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rol__3213E83FB4449602", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Estado_Id = table.Column<long>(type: "bigint", nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Genero__3213E83FA292C541", x => x.id);
                    table.ForeignKey(
                        name: "FK_Genero_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Idioma",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Estado_Id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Idioma__3213E83FE876D2EF", x => x.id);
                    table.ForeignKey(
                        name: "FK_Idioma_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Mensajes",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Estado_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Mensajes__3213E83F7DF2BB37", x => x.id);
                    table.ForeignKey(
                        name: "FK_Mensajes_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "MetodoPago",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Estado_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MetodoPa__3213E83F54BC56F9", x => x.id);
                    table.ForeignKey(
                        name: "FK_MetodoPago_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Penalizacion",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Tipo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Estado_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Penaliza__3213E83F7D8DECEB", x => x.id);
                    table.ForeignKey(
                        name: "FK_Penalizacion_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "RolProfesional",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Estado_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RolProfe__3213E83F80198EB5", x => x.id);
                    table.ForeignKey(
                        name: "FK_RolProfesional_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "TipoArticulo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Estado_Id = table.Column<long>(type: "bigint", nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipoArti__3213E83FF9924040", x => x.id);
                    table.ForeignKey(
                        name: "FK_TipoArticulo_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Correo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Clave = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Rol_Id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__3213E83F46B5B3AC", x => x.id);
                    table.ForeignKey(
                        name: "FK_Usuario_Rol",
                        column: x => x.Rol_Id,
                        principalTable: "Rol",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Elenco",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Apellido = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    RolProfesional_Id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Elenco__3213E83F0A6716A8", x => x.id);
                    table.ForeignKey(
                        name: "FK_Elenco_RolProfesional",
                        column: x => x.RolProfesional_Id,
                        principalTable: "RolProfesional",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Articulo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TipoArticulo_Id = table.Column<long>(type: "bigint", nullable: true),
                    Idioma_Id = table.Column<long>(type: "bigint", nullable: true),
                    RentaXDia = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    MontoEntregaTardia = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    Estado_Id = table.Column<long>(type: "bigint", nullable: true),
                    Genero_Id = table.Column<long>(type: "bigint", nullable: true),
                    Sinopsis = table.Column<string>(type: "text", nullable: true),
                    AnioEstreno = table.Column<int>(type: "int", nullable: true),
                    PortadaURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RutaURL = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Articulo__3213E83F60C1242E", x => x.id);
                    table.ForeignKey(
                        name: "FK_Articulo_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Articulo_Genero",
                        column: x => x.Genero_Id,
                        principalTable: "Genero",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Articulo_Idioma",
                        column: x => x.Idioma_Id,
                        principalTable: "Idioma",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Articulo_TipoArticulo",
                        column: x => x.TipoArticulo_Id,
                        principalTable: "TipoArticulo",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Cedula = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    TarjetaCr = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    limiteCredito = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    TipoPersona = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    Estado_Id = table.Column<long>(type: "bigint", nullable: true),
                    Usuario_Id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cliente__3213E83F90DB1ECF", x => x.id);
                    table.ForeignKey(
                        name: "FK_Cliente_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Cliente_Usuario",
                        column: x => x.Usuario_Id,
                        principalTable: "Usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Cedula = table.Column<long>(type: "bigint", nullable: false),
                    TandaLabor = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    PorcientoComision = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    FechaIngreso = table.Column<DateOnly>(type: "date", nullable: false),
                    Estado_Id = table.Column<long>(type: "bigint", nullable: false),
                    Usuario_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Empleado__3213E83F9B3A782B", x => x.id);
                    table.ForeignKey(
                        name: "FK_Empleado_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Empleado_Usuario",
                        column: x => x.Usuario_Id,
                        principalTable: "Usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ArticuloElenco",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Articulo_Id = table.Column<long>(type: "bigint", nullable: true),
                    Elenco_Id = table.Column<long>(type: "bigint", nullable: true),
                    RolEn_Articulo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Articulo__3213E83F1689B72A", x => x.id);
                    table.ForeignKey(
                        name: "FK_ArticuloElenco_Articulo",
                        column: x => x.Articulo_Id,
                        principalTable: "Articulo",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ArticuloElenco_Elenco",
                        column: x => x.Elenco_Id,
                        principalTable: "Elenco",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Descuento",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    MontoCobertura = table.Column<int>(type: "int", nullable: false),
                    EmpleadoCreador_Id = table.Column<long>(type: "bigint", nullable: true),
                    Estado_Id = table.Column<long>(type: "bigint", nullable: false),
                    FechaInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaVencimiento = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Descuent__3213E83F50491C77", x => x.id);
                    table.ForeignKey(
                        name: "FK_Descuento_Empleado",
                        column: x => x.EmpleadoCreador_Id,
                        principalTable: "Empleado",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Descuento_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MetodoPago_Id = table.Column<long>(type: "bigint", nullable: true),
                    MontoAPagar = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Estado_Id = table.Column<long>(type: "bigint", nullable: false),
                    NumTarjeta = table.Column<long>(type: "bigint", nullable: false),
                    FechaVencimientoTarjeta = table.Column<long>(type: "bigint", nullable: false),
                    CVV = table.Column<int>(type: "int", nullable: false),
                    RedPago_Id = table.Column<long>(type: "bigint", nullable: true),
                    NombreDuenioTarjeta = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Descuento_Id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pago__3213E83FD69EEE69", x => x.id);
                    table.ForeignKey(
                        name: "FK_Pago_Descuento",
                        column: x => x.Descuento_Id,
                        principalTable: "Descuento",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Pago_Estado",
                        column: x => x.MetodoPago_Id,
                        principalTable: "Estado",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Pago_MetodoPago",
                        column: x => x.MetodoPago_Id,
                        principalTable: "MetodoPago",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Pago_RedPago",
                        column: x => x.RedPago_Id,
                        principalTable: "RedPago",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Articulo_Id = table.Column<long>(type: "bigint", nullable: true),
                    Estado_Id = table.Column<long>(type: "bigint", nullable: true),
                    Cliente_Id = table.Column<long>(type: "bigint", nullable: true),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: true),
                    Id_Descuento = table.Column<long>(type: "bigint", nullable: true),
                    DuracionReserva = table.Column<int>(type: "int", nullable: true),
                    Empleado_Id = table.Column<long>(type: "bigint", nullable: true),
                    MontoTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Penalizacion_Id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reserva__3213E83FB3CE72F4", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reserva_Articulo",
                        column: x => x.Articulo_Id,
                        principalTable: "Articulo",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Reserva_Cliente",
                        column: x => x.Cliente_Id,
                        principalTable: "Cliente",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Reserva_Descuento",
                        column: x => x.Id_Descuento,
                        principalTable: "Descuento",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Reserva_Empleado",
                        column: x => x.Empleado_Id,
                        principalTable: "Empleado",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Reserva_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Reserva_Penalizacion",
                        column: x => x.Penalizacion_Id,
                        principalTable: "Penalizacion",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Devolucion",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    FechaDevolucion = table.Column<DateOnly>(type: "date", nullable: false),
                    Estado_Id = table.Column<long>(type: "bigint", nullable: false),
                    Reserva_Id = table.Column<long>(type: "bigint", nullable: false),
                    Penalizacion_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Devoluci__3213E83FA31BA99E", x => x.id);
                    table.ForeignKey(
                        name: "FK_Devolucion_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Devolucion_Penalizacion",
                        column: x => x.Penalizacion_Id,
                        principalTable: "Penalizacion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Devolucion_Reserva",
                        column: x => x.Reserva_Id,
                        principalTable: "Reserva",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Renta",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    FechaInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaVencimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    Estado_Id = table.Column<long>(type: "bigint", nullable: false),
                    Reserva_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Renta__3213E83FDC2286B9", x => x.id);
                    table.ForeignKey(
                        name: "FK_Renta_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Renta_Reserva",
                        column: x => x.Reserva_Id,
                        principalTable: "Reserva",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ReservaArticulo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reserva_Id = table.Column<long>(type: "bigint", nullable: true),
                    Articulo_Id = table.Column<long>(type: "bigint", nullable: true),
                    FechaReserva = table.Column<DateOnly>(type: "date", nullable: true),
                    FechaDevolucion = table.Column<DateOnly>(type: "date", nullable: true),
                    Estado_Id = table.Column<long>(type: "bigint", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ReservaA__3213E83F14413BC6", x => x.id);
                    table.ForeignKey(
                        name: "FK_ReservaArticulo_Articulo",
                        column: x => x.Articulo_Id,
                        principalTable: "Articulo",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ReservaArticulo_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ReservaArticulo_Reserva",
                        column: x => x.Reserva_Id,
                        principalTable: "Reserva",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulo_Estado_Id",
                table: "Articulo",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Articulo_Genero_Id",
                table: "Articulo",
                column: "Genero_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Articulo_Idioma_Id",
                table: "Articulo",
                column: "Idioma_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Articulo_TipoArticulo_Id",
                table: "Articulo",
                column: "TipoArticulo_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ArticuloElenco_Articulo_Id",
                table: "ArticuloElenco",
                column: "Articulo_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ArticuloElenco_Elenco_Id",
                table: "ArticuloElenco",
                column: "Elenco_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Estado_Id",
                table: "Cliente",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Usuario_Id",
                table: "Cliente",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Descuento_EmpleadoCreador_Id",
                table: "Descuento",
                column: "EmpleadoCreador_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Descuento_Estado_Id",
                table: "Descuento",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Devolucion_Estado_Id",
                table: "Devolucion",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Devolucion_Penalizacion_Id",
                table: "Devolucion",
                column: "Penalizacion_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Devolucion_Reserva_Id",
                table: "Devolucion",
                column: "Reserva_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Elenco_RolProfesional_Id",
                table: "Elenco",
                column: "RolProfesional_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_Estado_Id",
                table: "Empleado",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_Usuario_Id",
                table: "Empleado",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Genero_Estado_Id",
                table: "Genero",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Idioma_Estado_Id",
                table: "Idioma",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_Estado_Id",
                table: "Mensajes",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MetodoPago_Estado_Id",
                table: "MetodoPago",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_Descuento_Id",
                table: "Pago",
                column: "Descuento_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_MetodoPago_Id",
                table: "Pago",
                column: "MetodoPago_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_RedPago_Id",
                table: "Pago",
                column: "RedPago_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Penalizacion_Estado_Id",
                table: "Penalizacion",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Renta_Estado_Id",
                table: "Renta",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Renta_Reserva_Id",
                table: "Renta",
                column: "Reserva_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_Articulo_Id",
                table: "Reserva",
                column: "Articulo_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_Cliente_Id",
                table: "Reserva",
                column: "Cliente_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_Empleado_Id",
                table: "Reserva",
                column: "Empleado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_Estado_Id",
                table: "Reserva",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_Id_Descuento",
                table: "Reserva",
                column: "Id_Descuento");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_Penalizacion_Id",
                table: "Reserva",
                column: "Penalizacion_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaArticulo_Articulo_Id",
                table: "ReservaArticulo",
                column: "Articulo_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaArticulo_Estado_Id",
                table: "ReservaArticulo",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaArticulo_Reserva_Id",
                table: "ReservaArticulo",
                column: "Reserva_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RolProfesional_Estado_Id",
                table: "RolProfesional",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TipoArticulo_Estado_Id",
                table: "TipoArticulo",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Rol_Id",
                table: "Usuario",
                column: "Rol_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticuloElenco");

            migrationBuilder.DropTable(
                name: "Devolucion");

            migrationBuilder.DropTable(
                name: "Mensajes");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "Renta");

            migrationBuilder.DropTable(
                name: "ReservaArticulo");

            migrationBuilder.DropTable(
                name: "Elenco");

            migrationBuilder.DropTable(
                name: "MetodoPago");

            migrationBuilder.DropTable(
                name: "RedPago");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "RolProfesional");

            migrationBuilder.DropTable(
                name: "Articulo");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Descuento");

            migrationBuilder.DropTable(
                name: "Penalizacion");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Idioma");

            migrationBuilder.DropTable(
                name: "TipoArticulo");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Rol");
        }
    }
}
