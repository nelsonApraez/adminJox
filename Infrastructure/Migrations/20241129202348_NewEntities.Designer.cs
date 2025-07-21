using System;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20241129202348_NewEntities")]
    partial class NewEntities
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.AggregateModels.AuthorizationPermissions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EntityId")
                        .HasColumnType("int");

                    b.Property<bool>("PermissionCreate")
                        .HasColumnType("bit");

                    b.Property<bool>("PermissionDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("PermissionList")
                        .HasColumnType("bit");

                    b.Property<bool>("PermissionUpdate")
                        .HasColumnType("bit");

                    b.Property<bool>("PermissionView")
                        .HasColumnType("bit");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.HasIndex("RoleId");

                    b.ToTable("AuthorizationPermissions", "dbo", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("Domain.AggregateModels.Entity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Entity", "dbo", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("Domain.AggregateModels.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Badge")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BadgeClass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Class")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsExternalLink")
                        .HasColumnType("bit");

                    b.Property<bool>("IsParent")
                        .HasColumnType("bit");

                    b.Property<int?>("MenuId")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("Menu", "dbo", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("Domain.AggregateModels.Moneda.Moneda", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Codigo"));

                    b.Property<DateTime>("ActivoDesde")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ActivoHasta")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Codigo");

                    b.ToTable("Moneda", "dbo", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("Domain.AggregateModels.Pregunta", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumeroCategoria")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Pregunta", "dbo");
                });

            modelBuilder.Entity("Domain.AggregateModels.Processingresult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Proyectoid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Proyectoid");

                    b.ToTable("Processingresult", "dbo");
                });

            modelBuilder.Entity("Domain.AggregateModels.Prompt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Prompt", "dbo");
                });

            modelBuilder.Entity("Domain.AggregateModels.Proyecto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Proyecto", "dbo");
                });

            modelBuilder.Entity("Domain.AggregateModels.Respuesta", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Preguntaid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Proyectoid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Preguntaid");

                    b.HasIndex("Proyectoid");

                    b.ToTable("Respuesta", "dbo");
                });

            modelBuilder.Entity("Domain.AggregateModels.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Role", "dbo", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("Domain.AggregateModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User", "dbo", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("Domain.AggregateModels.AuthorizationPermissions", b =>
                {
                    b.HasOne("Domain.AggregateModels.Entity", "Entity")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.AggregateModels.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.AggregateModels.Entity", b =>
                {
                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "Name", b1 =>
                        {
                            b1.Property<int>("EntityId")
                                .HasColumnType("int");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("EntityId");

                            b1.ToTable("Entity", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("EntityId");
                        });

                    b.Navigation("Name");
                });

            modelBuilder.Entity("Domain.AggregateModels.Menu", b =>
                {
                    b.HasOne("Domain.AggregateModels.Menu", null)
                        .WithMany("SubMenu")
                        .HasForeignKey("MenuId");

                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "Title", b1 =>
                        {
                            b1.Property<int>("MenuId")
                                .HasColumnType("int");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Title");

                            b1.HasKey("MenuId");

                            b1.ToTable("Menu", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("MenuId");
                        });

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.AggregateModels.Moneda.Moneda", b =>
                {
                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "Descripcion", b1 =>
                        {
                            b1.Property<int>("MonedaCodigo")
                                .HasColumnType("int");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Descripcion");

                            b1.HasKey("MonedaCodigo");

                            b1.ToTable("Moneda", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("MonedaCodigo");
                        });

                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "Nombre", b1 =>
                        {
                            b1.Property<int>("MonedaCodigo")
                                .HasColumnType("int");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Nombre");

                            b1.HasKey("MonedaCodigo");

                            b1.ToTable("Moneda", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("MonedaCodigo");
                        });

                    b.OwnsOne("Domain.AggregateModels.Moneda.ValueObjects.IdentificadorMonedaValido", "Identificador", b1 =>
                        {
                            b1.Property<int>("MonedaCodigo")
                                .HasColumnType("int");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Identificador");

                            b1.HasKey("MonedaCodigo");

                            b1.ToTable("Moneda", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("MonedaCodigo");
                        });

                    b.Navigation("Descripcion");

                    b.Navigation("Identificador");

                    b.Navigation("Nombre");
                });

            modelBuilder.Entity("Domain.AggregateModels.Pregunta", b =>
                {
                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "Descripcion", b1 =>
                        {
                            b1.Property<Guid>("PreguntaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Descripcion");

                            b1.HasKey("PreguntaId");

                            b1.ToTable("Pregunta", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("PreguntaId");
                        });

                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "NombreCategoria", b1 =>
                        {
                            b1.Property<Guid>("PreguntaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("NombreCategoria");

                            b1.HasKey("PreguntaId");

                            b1.ToTable("Pregunta", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("PreguntaId");
                        });

                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "Valor", b1 =>
                        {
                            b1.Property<Guid>("PreguntaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Valor");

                            b1.HasKey("PreguntaId");

                            b1.ToTable("Pregunta", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("PreguntaId");
                        });

                    b.Navigation("Descripcion");

                    b.Navigation("NombreCategoria");

                    b.Navigation("Valor");
                });

            modelBuilder.Entity("Domain.AggregateModels.Processingresult", b =>
                {
                    b.HasOne("Domain.AggregateModels.Proyecto", "ProyectoidNavigation")
                        .WithMany()
                        .HasForeignKey("Proyectoid")
                        .IsRequired()
                        .HasConstraintName("FK_Processingresult_Proyectoid_Proyecto");

                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "Accompanyingstrategy", b1 =>
                        {
                            b1.Property<Guid>("ProcessingresultId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Accompanyingstrategy");

                            b1.HasKey("ProcessingresultId");

                            b1.ToTable("Processingresult", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("ProcessingresultId");
                        });

                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "Benefitcalculation", b1 =>
                        {
                            b1.Property<Guid>("ProcessingresultId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Benefitcalculation");

                            b1.HasKey("ProcessingresultId");

                            b1.ToTable("Processingresult", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("ProcessingresultId");
                        });

                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "Suggestedsolution", b1 =>
                        {
                            b1.Property<Guid>("ProcessingresultId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Suggestedsolution");

                            b1.HasKey("ProcessingresultId");

                            b1.ToTable("Processingresult", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("ProcessingresultId");
                        });

                    b.Navigation("Accompanyingstrategy");

                    b.Navigation("Benefitcalculation");

                    b.Navigation("ProyectoidNavigation");

                    b.Navigation("Suggestedsolution");
                });

            modelBuilder.Entity("Domain.AggregateModels.Prompt", b =>
                {
                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "Folder", b1 =>
                        {
                            b1.Property<Guid>("PromptId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Folder");

                            b1.HasKey("PromptId");

                            b1.ToTable("Prompt", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("PromptId");
                        });

                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "Nombre", b1 =>
                        {
                            b1.Property<Guid>("PromptId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Nombre");

                            b1.HasKey("PromptId");

                            b1.ToTable("Prompt", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("PromptId");
                        });

                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "Promtsystem", b1 =>
                        {
                            b1.Property<Guid>("PromptId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Promtsystem");

                            b1.HasKey("PromptId");

                            b1.ToTable("Prompt", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("PromptId");
                        });

                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "Promtuser", b1 =>
                        {
                            b1.Property<Guid>("PromptId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Promtuser");

                            b1.HasKey("PromptId");

                            b1.ToTable("Prompt", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("PromptId");
                        });

                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "Tags", b1 =>
                        {
                            b1.Property<Guid>("PromptId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Tags");

                            b1.HasKey("PromptId");

                            b1.ToTable("Prompt", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("PromptId");
                        });

                    b.Navigation("Folder");

                    b.Navigation("Nombre");

                    b.Navigation("Promtsystem");

                    b.Navigation("Promtuser");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Domain.AggregateModels.Proyecto", b =>
                {
                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "Descripcion", b1 =>
                        {
                            b1.Property<Guid>("ProyectoId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Descripcion");

                            b1.HasKey("ProyectoId");

                            b1.ToTable("Proyecto", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("ProyectoId");
                        });

                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "Nombre", b1 =>
                        {
                            b1.Property<Guid>("ProyectoId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Nombre");

                            b1.HasKey("ProyectoId");

                            b1.ToTable("Proyecto", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("ProyectoId");
                        });

                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "Tecnologias", b1 =>
                        {
                            b1.Property<Guid>("ProyectoId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Tecnologias");

                            b1.HasKey("ProyectoId");

                            b1.ToTable("Proyecto", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("ProyectoId");
                        });

                    b.Navigation("Descripcion");

                    b.Navigation("Nombre");

                    b.Navigation("Tecnologias");
                });

            modelBuilder.Entity("Domain.AggregateModels.Respuesta", b =>
                {
                    b.HasOne("Domain.AggregateModels.Pregunta", "PreguntaidNavigation")
                        .WithMany()
                        .HasForeignKey("Preguntaid")
                        .IsRequired()
                        .HasConstraintName("FK_Respuesta_Preguntaid_Pregunta");

                    b.HasOne("Domain.AggregateModels.Proyecto", "ProyectoidNavigation")
                        .WithMany()
                        .HasForeignKey("Proyectoid")
                        .IsRequired()
                        .HasConstraintName("FK_Respuesta_Proyectoid_Proyecto");

                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "Valor", b1 =>
                        {
                            b1.Property<Guid>("RespuestaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Valor");

                            b1.HasKey("RespuestaId");

                            b1.ToTable("Respuesta", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("RespuestaId");
                        });

                    b.Navigation("PreguntaidNavigation");

                    b.Navigation("ProyectoidNavigation");

                    b.Navigation("Valor");
                });

            modelBuilder.Entity("Domain.AggregateModels.Role", b =>
                {
                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "Name", b1 =>
                        {
                            b1.Property<int>("RoleId")
                                .HasColumnType("int");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("RoleId");

                            b1.ToTable("Role", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("RoleId");
                        });

                    b.Navigation("Name");
                });

            modelBuilder.Entity("Domain.AggregateModels.User", b =>
                {
                    b.HasOne("Domain.AggregateModels.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.OwnsOne("Domain.AggregateModels.ValueObjects.NombreValido", "FullName", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("int");

                            b1.Property<string>("Valor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("FullName");

                            b1.HasKey("UserId");

                            b1.ToTable("User", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("FullName");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.AggregateModels.Menu", b =>
                {
                    b.Navigation("SubMenu");
                });
#pragma warning restore 612, 618
        }
    }
}
