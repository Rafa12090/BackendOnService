using BackEnd_SocialE.Security.Domain.Models;

namespace BackEnd_SocialE.Learning.Domain.Models;

public class Event {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public string EventDate { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string Image { get; set; }
    //RELACIONES
    public int ManagerId { get; set; }
    public User Manager { get; set; }
}