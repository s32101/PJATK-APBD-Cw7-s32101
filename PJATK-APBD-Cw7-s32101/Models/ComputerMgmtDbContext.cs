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
            
            entity.HasData(
                new ComponentType()
                {
                    Id = 1,
                    Abbreviation = "MC",
                    Name = "Memory Card"
                },
                new ComponentType()
                {
                    Id = 2,
                    Abbreviation = "GC",
                    Name = "Graphics Card"
                },
                new ComponentType()
                {
                    Id = 3,
                    Abbreviation = "PCS",
                    Name = "Processor"
                });
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
            
            entity.HasData(
                new ComponentManufacturer(){
                    Id = 1,
                    FullName = "Super Computers",
                    Abbreviation = "SP",
                    FoundationDate = new DateTime(2000, 1, 1),
                    
                },
                new ComponentManufacturer()
                {
                    Id = 1,
                    FullName = "Super Bad Computers",
                    Abbreviation = "SBP",
                    FoundationDate = new DateTime(1995, 2, 1),
                },
                new ComponentManufacturer()
                {
                    Id = 1,
                    FullName = "Great Manufacturer",
                    Abbreviation = "GM",
                    FoundationDate = new DateTime(2005, 3, 1),
                });
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
            
            entity.HasData(
                new Component(){
                    ComponentTypesId = 1,
                    ComponentManufacturersId = 1,
                    Description = "none",
                    Name = "Graphic Card",
                    Code = "123",
                },
                new Component()
                {
                 
                },
                new Component()
                {
               
                });
        });
        
        modelBuilder.Entity<PCComponent>(entity =>
        {
            entity.ToTable("PCComponents");
            entity.HasKey(s => new { s.PCId, s.ComponentCode });
            entity.Property(s => s.ComponentCode)
                .HasColumnType("char(10)");

            entity.HasData(
                new PCComponent()
                {
                    
                },
                new PCComponent()
                {

                },
                new PCComponent()
                {

                });
        });
        
        
        
    }
}