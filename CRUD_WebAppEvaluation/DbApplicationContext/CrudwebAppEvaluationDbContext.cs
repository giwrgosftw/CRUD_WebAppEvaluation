using Microsoft.EntityFrameworkCore;

namespace CRUD_WebAppEvaluation.DbApplicationContext;

public partial class CrudwebAppEvaluationDbContext : DbContext
{
    public CrudwebAppEvaluationDbContext()
    {
    }

    public CrudwebAppEvaluationDbContext(DbContextOptions<CrudwebAppEvaluationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Evaluation> Evaluations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\Alienware_X16GK; Database=CRUDWebAppEvaluationDB; MultipleActiveResultSets=true; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Evaluation>(entity =>
        {
            entity.HasKey(e => new { e.StartDate, e.Storeid, e.Id, e.Detid }).HasName("PK__Evaluati__E800E11AE464A64D");

            entity.ToTable("Evaluation");

            entity.Property(e => e.StartDate).HasColumnType("smalldatetime");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Detid).HasColumnName("detid");
            entity.Property(e => e.AmountAsset).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.AmountRemain).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Comments)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.DateAsset).HasColumnType("smalldatetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
