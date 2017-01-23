using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Kravate4.Data;

namespace Kravate4.Migrations
{
    [DbContext(typeof(KravateDbContext))]
    [Migration("20170117132222_Added-Rating")]
    partial class AddedRating
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Kravate4.Models.Politician", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<Guid>("PartyID");

                    b.Property<float>("Popularity");

                    b.Property<float>("Score");

                    b.Property<string>("Surname");

                    b.Property<int>("VoteCount");

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

                    b.ToTable("Rating");
                });
        }
    }
}
