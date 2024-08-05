using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMusic.Data.Migrations
{
    public partial class SeedMusicsAndArtistsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert Artists
            migrationBuilder.Sql("INSERT INTO Artists (Name) VALUES ('Linkin Park')");
            migrationBuilder.Sql("INSERT INTO Artists (Name) VALUES ('Iron Maiden')");
            migrationBuilder.Sql("INSERT INTO Artists (Name) VALUES ('Flogging Molly')");
            migrationBuilder.Sql("INSERT INTO Artists (Name) VALUES ('Red Hot Chili Peppers')");

            // Insert Musics
            migrationBuilder.Sql(@"
                INSERT INTO Musics (Name, ArtistId) 
                VALUES ('In The End', (SELECT Id FROM Artists WHERE Name = 'Linkin Park'))
            ");
            migrationBuilder.Sql(@"
                INSERT INTO Musics (Name, ArtistId) 
                VALUES ('Numb', (SELECT Id FROM Artists WHERE Name = 'Linkin Park'))
            ");
            migrationBuilder.Sql(@"
                INSERT INTO Musics (Name, ArtistId) 
                VALUES ('Breaking The Habit', (SELECT Id FROM Artists WHERE Name = 'Linkin Park'))
            ");
            migrationBuilder.Sql(@"
                INSERT INTO Musics (Name, ArtistId) 
                VALUES ('Fear of the Dark', (SELECT Id FROM Artists WHERE Name = 'Iron Maiden'))
            ");
            migrationBuilder.Sql(@"
                INSERT INTO Musics (Name, ArtistId) 
                VALUES ('Number of the Beast', (SELECT Id FROM Artists WHERE Name = 'Iron Maiden'))
            ");
            migrationBuilder.Sql(@"
                INSERT INTO Musics (Name, ArtistId) 
                VALUES ('The Trooper', (SELECT Id FROM Artists WHERE Name = 'Iron Maiden'))
            ");
            migrationBuilder.Sql(@"
                INSERT INTO Musics (Name, ArtistId) 
                VALUES ('What''s Left of the Flag', (SELECT Id FROM Artists WHERE Name = 'Flogging Molly'))
            ");
            migrationBuilder.Sql(@"
                INSERT INTO Musics (Name, ArtistId) 
                VALUES ('Drunken Lullabies', (SELECT Id FROM Artists WHERE Name = 'Flogging Molly'))
            ");
            migrationBuilder.Sql(@"
                INSERT INTO Musics (Name, ArtistId) 
                VALUES ('If I Ever Leave This World Alive', (SELECT Id FROM Artists WHERE Name = 'Flogging Molly'))
            ");
            migrationBuilder.Sql(@"
                INSERT INTO Musics (Name, ArtistId) 
                VALUES ('Californication', (SELECT Id FROM Artists WHERE Name = 'Red Hot Chili Peppers'))
            ");
            migrationBuilder.Sql(@"
                INSERT INTO Musics (Name, ArtistId) 
                VALUES ('Tell Me Baby', (SELECT Id FROM Artists WHERE Name = 'Red Hot Chili Peppers'))
            ");
            migrationBuilder.Sql(@"
                INSERT INTO Musics (Name, ArtistId) 
                VALUES ('Parallel Universe', (SELECT Id FROM Artists WHERE Name = 'Red Hot Chili Peppers'))
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Musics");
            migrationBuilder.Sql("DELETE FROM Artists");
        }
    }
}