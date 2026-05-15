using Microsoft.EntityFrameworkCore;

namespace PJATK_APBD_Cw7_s32101.Models;

public class ComputerMgmtDbContext(DbContextOptions<ComputerMgmtDbContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}