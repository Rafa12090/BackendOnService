using AutoMapper;
using BackEnd_SocialE.Security.Domain.Models;
using BackEnd_SocialE.Security.Domain.Services.Communication;

namespace BackEnd_SocialE.Security.Mapping;

public class ResourceToModelProfile: Profile {
    public ResourceToModelProfile() {
        CreateMap<RegisterRequest, User>();
        CreateMap<UpdateRequest, User>().ForAllMembers(
            options => options.Condition(
                (source, target, property) =>
                {
                    if (property == null) return false;
                    if (property.GetType() == typeof(string) &&
                        string.IsNullOrEmpty((string)property)) return false;
                    return true;
                }
                )
            );
    }
}