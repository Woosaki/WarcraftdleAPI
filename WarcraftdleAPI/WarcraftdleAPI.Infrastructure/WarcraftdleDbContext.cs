using Microsoft.EntityFrameworkCore;
using WarcraftdleAPI.Domain.WowCharacter;

namespace WarcraftdleAPI.Infrastructure;

public class WarcraftdleDbContext(DbContextOptions<WarcraftdleDbContext> options) : DbContext(options)
{
    public DbSet<WowCharacter> WowCharacter { get; set; }
    public DbSet<Race> Race { get; set; }
    public DbSet<Expansion> Expansion { get; set; }
    public DbSet<Class> Class { get; set; }
    public DbSet<Gender> Gender { get; set; }
    public DbSet<Zone> Zone { get; set; }
    public DbSet<Affiliation> Affiliation { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gender>().HasData(
            new Gender { Id = 1, Name = "Male" },
            new Gender { Id = 2, Name = "Female" });

        modelBuilder.Entity<Class>().HasData(
            new Class { Id = 1, Name = "Warrior" },
            new Class { Id = 2, Name = "Paladin" },
            new Class { Id = 3, Name = "Hunter" },
            new Class { Id = 4, Name = "Rogue" },
            new Class { Id = 5, Name = "Priest" },
            new Class { Id = 6, Name = "Shaman" },
            new Class { Id = 7, Name = "Mage" },
            new Class { Id = 8, Name = "Warlock" },
            new Class { Id = 9, Name = "Monk" },
            new Class { Id = 10, Name = "Druid" },
            new Class { Id = 11, Name = "Demon Hunter" },
            new Class { Id = 12, Name = "Death Knight" },
            new Class { Id = 13, Name = "Evoker" });

        modelBuilder.Entity<Race>().HasData(
            new Race() { Id = 1, Name = "Human" },
            new Race() { Id = 2, Name = "Dwarf" },
            new Race() { Id = 3, Name = "Night Elf" },
            new Race() { Id = 4, Name = "Gnome" },
            new Race() { Id = 5, Name = "Draenei" },
            new Race() { Id = 6, Name = "Worgen" },
            new Race() { Id = 7, Name = "Pandaren" },
            new Race() { Id = 8, Name = "Orc" },
            new Race() { Id = 9, Name = "Undead" },
            new Race() { Id = 10, Name = "Tauren" },
            new Race() { Id = 11, Name = "Troll" },
            new Race() { Id = 12, Name = "Blood Elf" },
            new Race() { Id = 13, Name = "Goblin" },
            new Race() { Id = 14, Name = "Dragon" });

        modelBuilder.Entity<Expansion>().HasData(
            new Expansion { Id = 1, Name = "Classic" },
            new Expansion { Id = 2, Name = "The Burning Crusade", Abbreviation = "TBC" },
            new Expansion { Id = 3, Name = "Wrath of the Lich King", Abbreviation = "WotLK" },
            new Expansion { Id = 4, Name = "Cataclysm", Abbreviation = "Cata" },
            new Expansion { Id = 5, Name = "Mists of Pandaria", Abbreviation = "MOP" },
            new Expansion { Id = 6, Name = "Warlords of Draenor", Abbreviation = "WOD" },
            new Expansion { Id = 7, Name = "Legion" },
            new Expansion { Id = 8, Name = "Battle for Azeroth", Abbreviation = "BFA" },
            new Expansion { Id = 9, Name = "Shadowlands", Abbreviation = "SL" },
            new Expansion { Id = 10, Name = "Dragonflight", Abbreviation = "DF" });
    }
}