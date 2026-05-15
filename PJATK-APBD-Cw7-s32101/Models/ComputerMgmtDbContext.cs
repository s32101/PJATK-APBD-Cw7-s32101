using Microsoft.EntityFrameworkCore;

namespace PJATK_APBD_Cw7_s32101.Models;

public class ComputerMgmtDbContext(DbContextOptions<ComputerMgmtDbContext> options) : DbContext(options)
{
    public DbSet<Component> Components { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    public DbSet<PCComponent> PCComponents { get; set; }
    public DbSet<PC> PCs { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PC>(entity =>
        {
            entity.ToTable("PCs");
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Name)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(s => s.Weight)
                .HasColumnType("float(5)");
            
            entity.HasMany(s => s.PCComponents)
                .WithOne(s => s.PC)
                .HasForeignKey(s => s.PCId);
        });
        
        modelBuilder.Entity<ComponentType>(entity =>
        {
            entity.ToTable("ComponentTypes");
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Abbreviation)
                .HasMaxLength(30)
                .IsRequired();

            entity.Property(s => s.Name)
                .HasMaxLength(150)
                .IsRequired();

            entity.HasMany(s => s.Components)
                .WithOne(s => s.ComponentType)
                .HasForeignKey(s => s.ComponentTypesId);
        });
        
        modelBuilder.Entity<ComponentManufacturer>(entity =>
        {
            entity.ToTable("ComponentManufacturers");
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Abbreviation)
                .HasMaxLength(30)
                .IsRequired();

            entity.Property(s => s.FullName)
                .HasMaxLength(300)
                .IsRequired();
            
            entity.HasMany(s => s.Components)
                .WithOne(s => s.ComponentManufacturer)
                .HasForeignKey(s => s.ComponentManufacturersId);
        });
        
        modelBuilder.Entity<Component>(entity =>
        {
            entity.ToTable("Components");
            entity.HasKey(s => s.Code);
            entity.Property(s => s.Code)
                .HasColumnType("char(10)");

            entity.Property(s => s.Name)
                .HasMaxLength(300)
                .IsRequired();
            
            entity.Property(s => s.Description)
                .HasColumnType("nvarchar(max)");
            
            entity.HasMany(s => s.PCComponents)
                .WithOne(s => s.Component)
                .HasForeignKey(s => s.ComponentCode);
        });
        
        modelBuilder.Entity<PCComponent>(entity =>
        {
            entity.ToTable("PCComponents");
            entity.HasKey(s => new { s.PCId, s.ComponentCode });
            entity.Property(s => s.ComponentCode)
                .HasColumnType("char(10)");
        });
        
        
        
    }
}