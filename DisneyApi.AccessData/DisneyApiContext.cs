using DisneyApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DisneyApi.AccessData
{
    public class DisneyApiContext : DbContext
    {
        public DisneyApiContext() { }
        public DisneyApiContext(DbContextOptions<DisneyApiContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.GenreId);
                entity.Property(e => e.GenreId)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(30)
                      .IsUnicode(false);

                entity.Property(e => e.Image)
                      .IsRequired()
                      .HasMaxLength(200)
                      .IsUnicode(false);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.MovieId);
                entity.Property(e => e.MovieId)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);


                entity.Property(e => e.CreationDate)
                      .HasColumnType("date")
                      .IsRequired();

                entity.Property(e => e.Qualification)
                      .IsRequired(); //Int- possible values 1 to 5

                entity.HasOne(g => g.Genre)
                      .WithMany(M => M.Movies)
                      .HasForeignKey(g => g.GenreId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Character>(entity =>
            {
                entity.HasKey(e => e.CharacterId);
                entity.Property(e => e.CharacterId)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Age)
                    .IsRequired();

                entity.Property(e => e.Weight)
                    .IsRequired();

                entity.Property(e => e.History)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CharacterMovie>(entity =>
            {
                entity.HasKey(e => new { e.MovieId, e.CharacterId });

                entity.HasOne(e => e.Movie)
                      .WithMany(e => e.CharacterMovie)
                      .HasForeignKey(e => e.MovieId);

                entity.HasOne(e => e.Character)
                      .WithMany(e => e.CharacterMovie)
                      .HasForeignKey(e => e.CharacterId);
            });

            modelBuilder.Entity<Character>().HasData(
                new Character
                {
                    CharacterId=1,
                    Image= "Image here!",
                    Name= "Mickey Mouse",
                    Age=93,
                    Weight=10,
                    History= "Mickey Mouse is a cartoon character created in 1928 by Walt Disney, and is the mascot of The Walt Disney Company. An anthropomorphic mouse who typically wears red shorts, large yellow shoes, and white gloves, Mickey is one of the world's most recognizable fictional characters",
                },
                new Character
                {
                    CharacterId = 2,
                    Image = "Image here!",
                    Name = "Donald Duck",
                    Age = 78,
                    Weight = 20,
                    History = "Donald duck is an animated character created by Walt Disney as a foil to Mickey Mouse.[15] Making his screen debut in The Wise Little Hen on June 9, 1934, Donald is characterized as a pompous, showboating duck wearing a sailor suit, cap and a bow tie"
                },
                new Character
                {
                    CharacterId = 3,
                    Image = "Image here!",
                    Name = "Snow White",
                    Age = 14,
                    Weight = 50,
                    History = "Snow White is the titular character and heroine of Disney's first animated feature-length film Snow White and the Seven Dwarfs. She is a young Princess; the Fairest of Them All who, in her innocence, cannot see any of the evil in the world",
                },
                new Character
                {
                    CharacterId = 4,
                    Image = "Image here!",
                    Name = "Mulan",
                    Age = 16,
                    Weight = 55,
                    History = "Mulan is the protagonist of Disney's 1998 animated feature film of the same name and its 2004 direct-to-video sequel. Mulan is the eighth official Disney Princess and first heroine in the line-up who is not actually royalty through either birth or marriage",
                },
                new Character
                {
                    CharacterId = 5,
                    Image = "Image here!",
                    Name = "Goofy",
                    Age = 89,
                    Weight = 70,
                    History = "Goofy is an animated character that first appeared in 1932's Mickey's Revue.He  is predominately known for his slapstick style of comedy, and regularly appears alongside his best friends Mickey Mouse and Donald Duck",
                }
                );
            modelBuilder.Entity<Movie>().HasData
                (
                new Movie
                {
                    MovieId=1,
                    Image = "Image here!",
                    Title = "Mickey, Donald, Goofy: The Three Musketeers",
                    CreationDate = new DateTime(2004,08,17), //"yyyy-MM-dd"
                    Qualification = 1,
                    GenreId = 1
                },
                new Movie
                {
                    MovieId = 2,
                    Image = "Image here!",
                    Title = "Mulan",
                    CreationDate = new DateTime(1998, 06, 05), //"yyyy-MM-dd"
                    Qualification = 2,
                    GenreId = 2
                },
                new Movie
                {
                    MovieId = 3,
                    Image = "Image here!",
                    Title = "The Karnival Kid",
                    CreationDate = new DateTime(1929, 05, 23), //"yyyy-MM-dd"
                    Qualification = 3,
                    GenreId = 2
                }
                );
                modelBuilder.Entity<Genre>().HasData
                (
                new Genre
                {
                    GenreId=1,
                    Name = "Fantasy",
                    Image = "Image here!"
                },
                new Genre
                {
                    GenreId = 2,
                    Name = "animation",
                    Image = "Image here!"
                }
                );
                modelBuilder.Entity<CharacterMovie>().HasData
               (
               new CharacterMovie
               {
                   CharacterId=1,
                   MovieId=1
               },
               new CharacterMovie
               {
                   CharacterId = 2,
                   MovieId = 1
               },
               new CharacterMovie
               {
                   CharacterId = 5,
                   MovieId = 1
               },
               new CharacterMovie
               {
                   CharacterId = 1,
                   MovieId = 3
               }
               );
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    RoleId = 1,
                    Name = "Admin",
                    Description = "Admin Role"
                },
                new Role
                {
                    RoleId = 2,
                    Name = "User",
                    Description = "User Role"
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "TestAdmin",
                    Email = "TestAdmin@disney.com",
                    Password = "12345678",
                    RoleId = 1
                },
                new User
                {
                    UserId = 2,
                    Username = "TestUser",
                    Email = "TestUser@disney.com",
                    Password = "12345678",
                    RoleId = 2
                }
            );
        }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Character> Character { get; set; }
        public DbSet<CharacterMovie> CharacterMovie { get; set; }
    }
}
