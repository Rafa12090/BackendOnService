using System.Text.Json.Serialization;
using BackEnd_SocialE.Learning.Domain.Models;

namespace BackEnd_SocialE.Security.Domain.Models;

public class User
{
    /*
      "id": 1,
      "username": "Oizzram",
      "email": "oizzram1@gmail.com",
      "password": "Lamus1ca",
      "type": "normal"
     */
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Type { get; set; }
    [JsonIgnore]
    public string PasswordHash { get; set; }
}