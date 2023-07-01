namespace BackEnd_SocialE.Security.Resources;

public class UserResource
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Type { get; set; }
}