// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PressYourLuck.Models;

namespace PressYourLuck.Migrations
{
    [DbContext(typeof(AuditContext))]
    [Migration("20211111031109_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PressYourLuck.Models.AuditType", b =>
                {
                    b.Property<int>("AuditTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AuditTypeID");

                    b.ToTable("AuditTypes");

                    b.HasData(
                        new
                        {
                            AuditTypeID = 1,
                            Name = "Cash In"
                        },
                        new
                        {
                            AuditTypeID = 2,
                            Name = "Cash Out"
                        },
                        new
                        {
                            AuditTypeID = 3,
                            Name = "Win"
                        },
                        new
                        {
                            AuditTypeID = 4,
                            Name = "Lose"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
