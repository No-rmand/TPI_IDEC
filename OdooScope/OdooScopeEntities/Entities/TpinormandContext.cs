using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OdooScopeEntities.Entities;

public partial class TpinormandContext : DbContext
{
    public TpinormandContext()
    {
    }

    public TpinormandContext(DbContextOptions<TpinormandContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplicationOdoo> ApplicationOdoos { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<CreationListe> CreationListes { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionApplication> QuestionApplications { get; set; }

    public virtual DbSet<Resultat> Resultats { get; set; }

    public virtual DbSet<SecteurActivite> SecteurActivites { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;User ID=sa;Password=Id€c2o25++;Database=TPINormand;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationOdoo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Applicat__3214EC07658FA12E");

            entity.ToTable("ApplicationOdoo");

            entity.HasIndex(e => e.Id, "UQ__Applicat__3214EC06F1BFC4AD").IsUnique();

            entity.HasOne(d => d.SecteurActivite).WithMany(p => p.ApplicationOdoos)
                .HasForeignKey(d => d.SecteurActiviteId)
                .HasConstraintName("ApplicationOdoo_fk5");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Client__3214EC07DA426CD9");

            entity.ToTable("Client");

            entity.HasIndex(e => e.Id, "UQ__Client__3214EC068ADAA47F").IsUnique();

            entity.Property(e => e.Email).HasColumnName("EMail");

            entity.HasOne(d => d.SecteurActivite).WithMany(p => p.Clients)
                .HasForeignKey(d => d.SecteurActiviteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Client_fk4");
        });

        modelBuilder.Entity<CreationListe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Creation__3214EC073F500580");

            entity.ToTable("CreationListe");

            entity.HasIndex(e => e.Id, "UQ__Creation__3214EC060D2B5C5B").IsUnique();

            entity.HasOne(d => d.ApplicationOdoo).WithMany(p => p.CreationListes)
                .HasForeignKey(d => d.ApplicationOdooId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CreationListe_fk2");

            entity.HasOne(d => d.Resultat).WithMany(p => p.CreationListes)
                .HasForeignKey(d => d.ResultatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CreationListe_fk1");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC07C45B2A8E");

            entity.ToTable("Question");

            entity.HasIndex(e => e.Id, "UQ__Question__3214EC06EA10650C").IsUnique();

            entity.HasOne(d => d.QuestionNavigation).WithMany(p => p.InverseQuestionNavigation)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("Question_fk3");

            entity.HasOne(d => d.SecteurActivite).WithMany(p => p.Questions)
                .HasForeignKey(d => d.SecteurActiviteId)
                .HasConstraintName("Question_fk4");
        });

        modelBuilder.Entity<QuestionApplication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC0780A85C1D");

            entity.ToTable("QuestionApplication");

            entity.HasIndex(e => e.Id, "UQ__Question__3214EC069109E6B6").IsUnique();

            entity.HasOne(d => d.ApplicationOdoo).WithMany(p => p.QuestionApplications)
                .HasForeignKey(d => d.ApplicationOdooId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("QuestionApplication_fk1");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionApplications)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("QuestionApplication_fk2");
        });

        modelBuilder.Entity<Resultat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Resultat__3214EC07210D8F36");

            entity.ToTable("Resultat");

            entity.HasIndex(e => e.Id, "UQ__Resultat__3214EC0643C33180").IsUnique();

            entity.HasOne(d => d.Client).WithMany(p => p.Resultats)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Resultat_fk3");
        });

        modelBuilder.Entity<SecteurActivite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SecteurA__3214EC0758E09185");

            entity.ToTable("SecteurActivite");

            entity.HasIndex(e => e.Id, "UQ__SecteurA__3214EC06EA1D4885").IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
