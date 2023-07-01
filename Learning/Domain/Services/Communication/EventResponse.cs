using BackEnd_SocialE.Learning.Domain.Models;
using BackEnd_SocialE.Shared.Domain.Services.Communication;

namespace BackEnd_SocialE.Learning.Domain.Services.Communication;

public class EventResponse :BaseResponse<Event>
{
    public EventResponse(string message) : base(message) { }

    public EventResponse(Event resource) : base(resource) { }
}