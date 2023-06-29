using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pach_OS.Models
{
    public partial class Pach_OSContext : IdentityDbContext
    {
        public Pach_OSContext()
        {
        }

        public Pach_OSContext(DbContextOptions<Pach_OSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<DetalleVenta> DetalleVentas { get; set; } = null!;
        public virtual DbSet<DetallesCompra> DetallesCompras { get; set; } = null!;
        public virtual DbSet<Insumo> Insumos { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<ProductosInsumo> ProductosInsumos { get; set; } = null!;
        public virtual DbSet<Proveedore> Proveedores { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Venta> Ventas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=Pach_OS;integrated security=True; TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)

                    .HasName("PK__categori__CD54BC5A68905BA1");


                entity.ToTable("categorias");

                entity.Property(e => e.IdCategoria)
                    .ValueGeneratedNever()
                    .HasColumnName("id_categoria");

                entity.Property(e => e.NomCategoria)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nomCategoria");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.IdCompras)

                    .HasName("PK__compras__59093490D7BBFFB7");


                entity.ToTable("compras");

                entity.Property(e => e.IdCompras)
                    .ValueGeneratedNever()
                    .HasColumnName("id_compras");

                entity.Property(e => e.FechaCompra)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_compra");

                entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("FK_compras_proveedores");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdUsuario)

                    .HasConstraintName("FK__compras__id_usua__49C3F6B7");

            });

            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.HasKey(e => e.IdDetalleVenta)

                    .HasName("PK__detalleV__3C2E445CA143D433");


                entity.ToTable("detalleVentas");

                entity.Property(e => e.IdDetalleVenta).HasColumnName("id_detalleVenta");

                entity.Property(e => e.CantVendida).HasColumnName("cant_vendida");

                entity.Property(e => e.Precio).HasColumnName("precio");

                entity.Property(e => e.ProductosId).HasColumnName("productos_id");

                entity.Property(e => e.TotalPrecio).HasColumnName("totalPrecio");

                entity.Property(e => e.VentaId).HasColumnName("venta_id");

                entity.HasOne(d => d.Productos)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.ProductosId)
                    .HasConstraintName("FK_detalleVentas_productos");

                entity.HasOne(d => d.Venta)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.VentaId)

                    .HasConstraintName("FK__detalleVe__venta__778AC167");

            });

            modelBuilder.Entity<DetallesCompra>(entity =>
            {
                entity.HasKey(e => e.IdDetallesCompras)

                    .HasName("PK__Detalles__8D095AC36C3F477E");


                entity.ToTable("Detalles_Compras");

                entity.Property(e => e.IdDetallesCompras)
                    .ValueGeneratedNever()
                    .HasColumnName("id_detalles_compras");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.ComprasId).HasColumnName("compras_id");

                entity.Property(e => e.InsumosId).HasColumnName("insumos_id");

                entity.Property(e => e.PrecioInsumo).HasColumnName("precio_insumo");

                entity.HasOne(d => d.Compras)
                    .WithMany(p => p.DetallesCompras)
                    .HasForeignKey(d => d.ComprasId)

                    .HasConstraintName("FK__Detalles___compr__4BAC3F29");


                entity.HasOne(d => d.Insumos)
                    .WithMany(p => p.DetallesCompras)
                    .HasForeignKey(d => d.InsumosId)

                    .HasConstraintName("FK__Detalles___insum__4CA06362");

            });

            modelBuilder.Entity<Insumo>(entity =>
            {
                entity.HasKey(e => e.IdInsumos)

                    .HasName("PK__insumos__B76055AED687714B");

                entity.ToTable("insumos");

                entity.Property(e => e.IdInsumos)
                    .ValueGeneratedNever()
                    .HasColumnName("id_insumos");

                entity.Property(e => e.CantInsumo)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cant_insumo");

                entity.Property(e => e.NomInsumo)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nom_insumo");

                entity.Property(e => e.ProveedoresId).HasColumnName("proveedores_id");

                entity.Property(e => e.TiempoLlegado)
                    .HasMaxLength(255)
                    .HasColumnName("tiempo_llegado");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProductos)

                    .HasName("PK__producto__3804F4FB38FC97EE");


                entity.ToTable("productos");

                entity.Property(e => e.IdProductos).HasColumnName("id_productos");

                entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");

                entity.Property(e => e.Estado)
                    .HasMaxLength(255)
                    .HasColumnName("estado");

                entity.Property(e => e.MaximoSabores).HasColumnName("maximo_sabores");

                entity.Property(e => e.NomProducto)
                    .HasMaxLength(255)
                    .HasColumnName("nom_producto");

                entity.Property(e => e.PrecioVenta).HasColumnName("precio_venta");

                entity.Property(e => e.TamanoPizza).HasColumnName("tamano_pizza");

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.CategoriaId)

                    .HasConstraintName("FK__productos__categ__4F7CD00D");

            });

            modelBuilder.Entity<ProductosInsumo>(entity =>
            {
                entity.ToTable("productos_insumos");


                entity.HasKey(e => e.Id).HasName("PK__producto__3214EC27EBD61552");


                entity.Property(e => e.CantInsumo).HasColumnName("cant_insumo");

                entity.Property(e => e.InsumosId).HasColumnName("insumosID");

                entity.Property(e => e.ProductosId).HasColumnName("productosId");

                entity.HasOne(d => d.Insumos)
                    .WithMany(p => p.ProductosInsumos)
                    .HasForeignKey(d => d.InsumosId)

                    .HasConstraintName("FK__productos__insum__60A75C0F");


                entity.HasOne(d => d.Productos)
                    .WithMany(p => p.ProductosInsumos)
                    .HasForeignKey(d => d.ProductosId)

                    .HasConstraintName("FK__productos__produ__5FB337D6");

            });

            modelBuilder.Entity<Proveedore>(entity =>
            {
                entity.HasKey(e => e.IdProveedor)

                    .HasName("PK__proveedo__8D3DFE283F237B5C");

                entity.ToTable("proveedores");

                entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");

                entity.Property(e => e.InsumosId).HasColumnName("insumos_id");

                entity.Property(e => e.Nit)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("nit");

                entity.Property(e => e.NomLocal)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nom_local");

                entity.HasOne(d => d.Insumos)
                    .WithMany(p => p.Proveedores)
                    .HasForeignKey(d => d.InsumosId)

                    .HasConstraintName("FK__proveedor__insum__52593CB8");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRol)

                    .HasName("PK__roles__6ABCB5E0C26F1690");


                entity.ToTable("roles");

                entity.Property(e => e.IdRol).HasColumnName("id_rol");

                entity.Property(e => e.NomRol)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nom_rol");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)

                    .HasName("PK__usuarios__4E3E04ADDB368B9D");


                entity.ToTable("usuarios");

                entity.Property(e => e.IdUsuario)
                    .ValueGeneratedNever()
                    .HasColumnName("id_usuario");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Celular)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("celular");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contrasena");

                entity.Property(e => e.Correo)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaEntrada)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_entrada")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NumDocumento)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("num_documento");

                entity.Property(e => e.RolId).HasColumnName("rol_id");

                entity.Property(e => e.TipoDocumento)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("tipo_documento");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.RolId)

                    .HasConstraintName("FK__usuarios__rol_id__534D60F1");

            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.IdVentas)

                    .HasName("PK__ventas__283C869DAE168DF8");

                entity.ToTable("ventas");

                entity.Property(e => e.IdVentas).HasColumnName("id_ventas");

                entity.Property(e => e.FechaVenta)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_venta")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Pago).HasColumnName("pago");

                entity.Property(e => e.PagoDomicilio).HasColumnName("pago_domicilio");

                entity.Property(e => e.TipoPago)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("tipo_pago");

                entity.Property(e => e.TotalVenta).HasColumnName("total_venta");

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.UsuarioId)

                    .HasConstraintName("FK__ventas__usuario___7E37BEF6");
                base.OnModelCreating(modelBuilder);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
