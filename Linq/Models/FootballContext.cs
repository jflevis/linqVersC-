using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

//#nullable disable

namespace Linq.Models
{
    public partial class FootballContext : DbContext
    {
        public FootballContext()
        {
        }

        public FootballContext(DbContextOptions<FootballContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Conference> Conferences { get; set; }
        public virtual DbSet<Equipe> Equipes { get; set; }
        public virtual DbSet<Etat> Etats { get; set; }
        public virtual DbSet<Joueur> Joueurs { get; set; }
        public virtual DbSet<JoueurEquipe> JoueurEquipes { get; set; }
        public virtual DbSet<Ville> Villes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning //To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Football;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
 
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "French_CI_AS");

            modelBuilder.Entity<Conference>(entity =>
            {
                entity.HasKey(e => e.IdConference);

                entity.ToTable("Conference");

                entity.Property(e => e.IdConference).HasColumnName("id_Conference");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Equipe>(entity =>
            {
                entity.HasKey(e => e.IdEquipe);

                entity.ToTable("Equipe");

                entity.Property(e => e.IdEquipe).HasColumnName("id_Equipe");

                entity.Property(e => e.IdConference).HasColumnName("id_Conference");

                entity.Property(e => e.IdVille).HasColumnName("id_Ville");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surnom)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdConferenceNavigation)
                    .WithMany(p => p.Equipes)
                    .HasForeignKey(d => d.IdConference)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Equipe_Conference");

                entity.HasOne(d => d.IdVilleNavigation)
                    .WithMany(p => p.Equipes)
                    .HasForeignKey(d => d.IdVille)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Equipe_Ville");
            });

            modelBuilder.Entity<Etat>(entity =>
            {
                entity.HasKey(e => e.IdEtat);

                entity.ToTable("Etat");

                entity.Property(e => e.IdEtat).HasColumnName("id_Etat");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Joueur>(entity =>
            {
                entity.HasKey(e => e.IdJoueur);

                entity.ToTable("Joueur");

                entity.Property(e => e.IdJoueur).HasColumnName("id_Joueur");

                entity.Property(e => e.DateNaissance).HasColumnType("date");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Prenom)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JoueurEquipe>(entity =>
            {
                entity.HasKey(e => new { e.IdJoueur, e.IdEquipe });

                entity.ToTable("JoueurEquipe");

                entity.Property(e => e.IdJoueur).HasColumnName("id_Joueur");

                entity.Property(e => e.IdEquipe).HasColumnName("id_Equipe");

                entity.HasOne(d => d.IdEquipeNavigation)
                    .WithMany(p => p.JoueurEquipes)
                    .HasForeignKey(d => d.IdEquipe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JoueurEquipe_Equipe");

                entity.HasOne(d => d.IdJoueurNavigation)
                    .WithMany(p => p.JoueurEquipes)
                    .HasForeignKey(d => d.IdJoueur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JoueurEquipe_Joueur");
            });

            modelBuilder.Entity<Ville>(entity =>
            {
                entity.HasKey(e => e.IdVille);

                entity.ToTable("Ville");

                entity.Property(e => e.IdVille).HasColumnName("id_Ville");

                entity.Property(e => e.IdEtat).HasColumnName("id_Etat");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEtatNavigation)
                    .WithMany(p => p.Villes)
                    .HasForeignKey(d => d.IdEtat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ville_Etat");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
