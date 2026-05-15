using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PJATK_APBD_Cw7_s32101.DTOs;
using PJATK_APBD_Cw7_s32101.Models;

namespace PJATK_APBD_Cw7_s32101.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PCsController(ComputerMgmtDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var allFromDb = await db.PCs
            .ToListAsync(); //select * from PCs - jak masz 1 milion rekordów to rip
        
        return Ok(allFromDb.Select(x => new PCDto()
        {
            Id =  x.Id,
            Name = x.Name,
            Weight =  x.Weight,
            CreatedAt =  x.CreatedAt,
            Stock =  x.Stock,
            Warranty =   x.Warranty
        }));
    }

    [HttpGet("{id:int}/components")]
    public async Task<IActionResult> GetComponentsAsync(int id)
    {
        var pc = await db.PCs.Include(x => x.PCComponents)
            .ThenInclude(x => x.Component)
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        
        if (pc == null)
            return NotFound();
        
        return Ok(pc.PCComponents.Select(y => new PCComponentDto()
        {
            ComponentCode = y.ComponentCode,
            ComponentName = y.Component.Name,
            Amount = y.Amount
        }).ToList());
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] PCDto pcDto)
    {
        var x= await db.PCs.AddAsync(new PC
        {
            Name = pcDto.Name,
            CreatedAt = pcDto.CreatedAt,
            Stock = pcDto.Stock,
            Warranty = pcDto.Warranty,
            Weight = pcDto.Weight
        });
        await db.SaveChangesAsync();
        pcDto.Id = x.Entity.Id;
        return Ok(pcDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] PCDto pcDto)
    {
        var pc = await db.PCs
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        
        if (pc == null)
            return NotFound();
        
        pc.Name = pcDto.Name;
        pc.CreatedAt = pcDto.CreatedAt;
        pc.Stock = pcDto.Stock;
        pc.Warranty = pcDto.Warranty;
        pc.Weight = pcDto.Weight;
        db.Update(pc);
        await db.SaveChangesAsync();
        
        pcDto.Id = pc.Id;
        return Ok(pcDto);
    }
}