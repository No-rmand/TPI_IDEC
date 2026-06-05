using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OdooScopeEntities.Entities;

public partial class SqlServerContext : DbContext
{
    public SqlServerContext()
    {
    }

    public SqlServerContext(DbContextOptions<SqlServerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplicationOdoo> ApplicationOdoos { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<CreationListe> CreationListes { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionApplication> QuestionApplications { get; set; }

    public virtual DbSet<Repondre> Repondres { get; set; }

    public virtual DbSet<Resultat> Resultats { get; set; }

    public virtual DbSet<SecteurActivite> SecteurActivites { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationOdoo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Applicat__3214EC07FA44145A");

            entity.ToTable("ApplicationOdoo");

            entity.HasIndex(e => e.Id, "UQ__Applicat__3214EC0627266287").IsUnique();

            entity.HasOne(d => d.SecteurActivite).WithMany(p => p.ApplicationOdoos)
                .HasForeignKey(d => d.SecteurActiviteId)
                .HasConstraintName("ApplicationOdoo_fk5");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Client__3214EC0789E0E3BD");

            entity.ToTable("Client");

            entity.HasIndex(e => e.Id, "UQ__Client__3214EC06BC18B4D0").IsUnique();

            entity.Property(e => e.Email).HasColumnName("EMail");

            entity.HasOne(d => d.SecteurActivite).WithMany(p => p.Clients)
                .HasForeignKey(d => d.SecteurActiviteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Client_fk4");
        });

        modelBuilder.Entity<CreationListe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Creation__3214EC07AE1468A8");

            entity.ToTable("CreationListe");

            entity.HasIndex(e => e.Id, "UQ__Creation__3214EC068F971681").IsUnique();

            entity.HasOne(d => d.ApplicationOdoo).WithMany(p => p.CreationListes)
                .HasForeignKey(d => d.ApplicationOdooId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CL_Application");

            entity.HasOne(d => d.Resultat).WithMany(p => p.CreationListes)
                .HasForeignKey(d => d.ResultatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CL_Resultat");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC07CD33FE21");

            entity.ToTable("Question");

            entity.HasIndex(e => e.Id, "UQ__Question__3214EC0602CD04A1").IsUnique();
        });

        modelBuilder.Entity<QuestionApplication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC07B671A906");

            entity.ToTable("QuestionApplication");

            entity.HasOne(d => d.ApplicationOdoo).WithMany(p => p.QuestionApplications)
                .HasForeignKey(d => d.ApplicationOdooId)
                .HasConstraintName("FK_QA_Application");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionApplications)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_QA_Question");
        });

        modelBuilder.Entity<Repondre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Repondre__3214EC071E8B2BC1");

            entity.ToTable("Repondre");

            entity.HasOne(d => d.Client).WithMany(p => p.Repondres)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_Repondre_Client");

            entity.HasOne(d => d.Question).WithMany(p => p.Repondres)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_Repondre_Question");
        });

        modelBuilder.Entity<Resultat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Resultat__3214EC078EC8A408");

            entity.ToTable("Resultat");

            entity.HasIndex(e => e.Id, "UQ__Resultat__3214EC06F1B01D0A").IsUnique();

            entity.HasOne(d => d.Client).WithMany(p => p.Resultats)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Resultat_fk3");
        });

        modelBuilder.Entity<SecteurActivite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SecteurA__3214EC07DAB68DB0");

            entity.ToTable("SecteurActivite");

            entity.HasIndex(e => e.Id, "UQ__SecteurA__3214EC06A7E78CE4").IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
