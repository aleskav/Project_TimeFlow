using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TimeFlow.Models;

namespace TimeFlow.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public override DbSet<User> Users { get; set; }
        public DbSet<Superviseur> Superviseurs { get; set; }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<Projet> Projets { get; set; }
        public DbSet<Tache> Taches { get; set; }
        public DbSet<FeuilleTemps> FeuilleTemps  { get; set; }
        public DbSet<FeuilleJour> FeuilleJours { get; set; }

        public DbSet<JourTache> JourTaches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FeuilleJour>()
               .HasMany(e => e.Taches)
               .WithMany(e => e.FeuilleJours)
               .UsingEntity<JourTache>();

        }
    }
}