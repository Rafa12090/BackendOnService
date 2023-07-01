using BackEnd_SocialE.Security.Domain.Models;

namespace BackEnd_SocialE.Learning.Domain.Models;

public class Payment
{
    public int Id { get; set; }
    public string CardNumber { get; set; }
    public string Type { get; set; }
    public string Direction { get; set; }
    public double Phone { get; set; }
    public string UserName { get; set; }
    public string UserLastName { get; set; }
    public string Country { get; set; }
    public string ExpiryDate { get; set; }
    public int SecurityCode { get; set; }
    public int PostalCode { get; set; }
    //RELACIONES
    public int UserId { get; set; }
    public User User { get; set; }
    public int EventId { get; set; }
    public Event Event { get; set; }
}