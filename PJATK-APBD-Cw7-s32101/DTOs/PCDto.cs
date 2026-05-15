namespace PJATK_APBD_Cw7_s32101.DTOs;

public class PCDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public float Weight { get; set; }

    public int Warranty { get; set; }

    public DateTime CreatedAt { get; set; }

    public int Stock { get; set; }

    public List<PCComponentDto> Components { get; set; } = [];
}
