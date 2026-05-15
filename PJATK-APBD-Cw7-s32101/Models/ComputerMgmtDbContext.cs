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

            entity.HasData(
                new PC()
                {
                    Id = 1,
                    Name = "Gaming Beast",
                    Weight = 12.5f
                },
                new PC()
                {
                    Id = 2,
                    Name = "Office Pro",
                    Weight = 8.2f
                },
                new PC()
                {
                    Id = 3,
                    Name = "Mini Workstation",
                    Weight = 6.7f
                });
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
                    Abbreviation = "RAM",
                    Name = "Random Access Memory"
                },
                new ComponentType()
                {
                    Id = 2,
                    Abbreviation = "GPU",
                    Name = "Graphics Card"
                },
                new ComponentType()
                {
                    Id = 3,
                    Abbreviation = "CPU",
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
                new ComponentManufacturer()
                {
                    Id = 1,
                    FullName = "Super Computers",
                    Abbreviation = "SC",
                    FoundationDate = new DateTime(2000, 1, 1),
                },
                new ComponentManufacturer()
                {
                    Id = 2,
                    FullName = "Tech Masters",
                    Abbreviation = "TM",
                    FoundationDate = new DateTime(1995, 5, 15),
                },
                new ComponentManufacturer()
                {
                    Id = 3,
                    FullName = "Future Electronics",
                    Abbreviation = "FE",
                    FoundationDate = new DateTime(2010, 9, 20),
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
                new Component()
                {
                    Code = "RAM0000001",
                    Name = "Corsair 16GB DDR4",
                    Description = "16GB DDR4 RAM 3200MHz",
                    ComponentTypesId = 1,
                    ComponentManufacturersId = 1
                },
                new Component()
                {
                    Code = "GPU0000001",
                    Name = "RTX 4060",
                    Description = "NVIDIA graphics card",
                    ComponentTypesId = 2,
                    ComponentManufacturersId = 2
                },
                new Component()
                {
                    Code = "CPU0000001",
                    Name = "Intel i7 14700K",
                    Description = "14th generation Intel processor",
                    ComponentTypesId = 3,
                    ComponentManufacturersId = 3
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
                    PCId = 1,
                    ComponentCode = "GPU0000001",
                    Amount = 1
                },
                new PCComponent()
                {
                    PCId = 1,
                    ComponentCode = "RAM0000001",
                    Amount = 2
                },
                new PCComponent()
                {
                    PCId = 2,
                    ComponentCode = "CPU0000001",
                    Amount = 1
                });
        });
    }
}