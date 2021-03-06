// <auto-generated />
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CleanArchMvc.Infra.Data.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20220630220510_CreateTables")]
partial class CreateTables
{
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "6.0.6")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

        modelBuilder.Entity("CleanArchMvc.Domain.Entities.Category", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Categories");
            });

        modelBuilder.Entity("CleanArchMvc.Domain.Entities.Product", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                b.Property<int>("CategoryId")
                    .HasColumnType("int");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Image")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<decimal>("Price")
                    .HasColumnType("decimal(18,2)");

                b.Property<int>("Stock")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("CategoryId");

                b.ToTable("Products");
            });

        modelBuilder.Entity("CleanArchMvc.Domain.Entities.Product", b =>
            {
                b.HasOne("CleanArchMvc.Domain.Entities.Category", "category")
                    .WithMany("Product")
                    .HasForeignKey("CategoryId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("category");
            });

        modelBuilder.Entity("CleanArchMvc.Domain.Entities.Category", b =>
            {
                b.Navigation("Product");
            });
#pragma warning restore 612, 618
    }
}
