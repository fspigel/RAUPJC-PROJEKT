using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Kravate4.Data;

namespace Kravate4.Migrations
{
    [DbContext(typeof(KravateDbContext))]
    partial class KravateDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Kravate4.Models.Comment", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<Guid>("PoliticianID");

                    b.Property<Guid>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("PoliticianID");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Kravate4.Models.Politician", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<Guid>("PartyID");

                    b.Property<float>("Popularity");

                    b.Property<string>("Surname");

                    b.HasKey("ID");

                    b.ToTable("Politician");
                });

            modelBuilder.Entity("Kravate4.Models.Rating", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<Guid>("PoliticianID");

                    b.Property<Guid>("UserID");

                    b.Property<short>("Value");

                    b.HasKey("ID");

                    b.HasIndex("PoliticianID");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("Kravate4.Models.Comment", b =>
                {
                    b.HasOne("Kravate4.Models.Politician", "Politician")
                        .WithMany("Comments")
                        .HasForeignKey("PoliticianID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Kravate4.Models.Rating", b =>
                {
                    b.HasOne("Kravate4.Models.Politician", "Politician")
                        .WithMany("Ratings")
                        .HasForeignKey("PoliticianID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
