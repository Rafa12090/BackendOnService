namespace BackEnd_SocialE.Learning.Resources;

public class SaveEventResource
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public string EventDate { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string Image { get; set; }
    public int ManagerId { get; set; }
}