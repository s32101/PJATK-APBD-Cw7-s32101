namespace PJATK_APBD_Cw7_s32101.Models;

public class ComponentManufacturer
{
    public int Id { get; set; }
    
    public string Abbreviation { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public DateTime FoundationDate { get; set; }

    public virtual Component Component { get; set; } = null!;
}