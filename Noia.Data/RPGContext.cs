using Microsoft.EntityFrameworkCore;
using Noia.MonsterNS;
using Noia.PlayerNS;
using Noia.Skills;

namespace Noia.Data;

public class RPGContext : DbContext
{

    public DbSet<Player> Players { get; set; }
    
    public DbSet<Attack> Attacks { get; set; }
    public DbSet<Spell> Spells { get; set; }
    public DbSet<PlayerAttack> PlayerAttacks { get; set; }
    public DbSet<PlayerSpell> PlayerSpells { get; set; }
    public DbSet<Monster> Monsters { get; set; }
    

    private readonly string StringConexao = "Server=localhost;Port=3306;Database=db_rpg_vscode;Uid=root;Pwd=1234;";
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(StringConexao, ServerVersion.AutoDetect(StringConexao));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Player>().HasKey(p => p.IdPlayer);

        modelBuilder.Entity<PlayerAttack>()
            .HasKey(pa => new { pa.IdPlayer, pa.IdAttack });

        modelBuilder.Entity<PlayerAttack>()
            .HasOne(pa => pa.Player)
            .WithMany(p => p.PlayerAttacks)
            .HasForeignKey(pa => pa.IdPlayer);

        modelBuilder.Entity<PlayerAttack>()
            .HasOne(pa => pa.Attack)
            .WithMany(a => a.PlayerAttacks)
            .HasForeignKey(pa => pa.IdAttack);

        modelBuilder.Entity<PlayerSpell>()
            .HasKey(ps => new { ps.IdPlayer, ps.IdSpell });

        modelBuilder.Entity<PlayerSpell>()
            .HasOne(ps => ps.Player)
            .WithMany(p => p.PlayerSpells)
            .HasForeignKey(ps => ps.IdPlayer);

        modelBuilder.Entity<PlayerSpell>()
            .HasOne(ps => ps.Spell)
            .WithMany(s => s.PlayerSpells)
            .HasForeignKey(ps => ps.IdSpell);

        modelBuilder.Entity<MonsterAttack>()
            .HasKey(ma => new { ma.IdMonster, ma.IdAttack});

        modelBuilder.Entity<MonsterAttack>()
            .HasOne(ma => ma.Monster)
            .WithMany(m => m.MonsterAttacks)
            .HasForeignKey(ma => ma.IdMonster);

        modelBuilder.Entity<MonsterAttack>()
            .HasOne(ma => ma.Attack)
            .WithMany(a => a.MonsterAttacks)
            .HasForeignKey(ma => ma.IdAttack);

        modelBuilder.Entity<MonsterSpell>()
            .HasKey(ms => new { ms.IdMonster, ms.IdSpell});

        modelBuilder.Entity<MonsterSpell>()
            .HasOne(ms => ms.Monster)
            .WithMany(m => m.MonsterSpells)
            .HasForeignKey(ms => ms.IdMonster);

        modelBuilder.Entity<MonsterSpell>()
            .HasOne(ms => ms.Spell)
            .WithMany(m => m.MonsterSpells)
            .HasForeignKey(ms => ms.IdSpell);

       
            
    }
}
