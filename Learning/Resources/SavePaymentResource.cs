namespace BackEnd_SocialE.Learning.Resources;

public class SavePaymentResource
{
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
    public int UserId { get; set; }
    public int EventId { get; set; }
}