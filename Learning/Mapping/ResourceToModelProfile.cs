using AutoMapper;
using BackEnd_SocialE.Learning.Domain.Models;
using BackEnd_SocialE.Learning.Resources;

namespace BackEnd_SocialE.Learning.Mapping;

public class ResourceToModelProfile: Profile {
    public ResourceToModelProfile() {
        CreateMap<SaveEventResource, Event>();
        CreateMap<SavePaymentResource, Payment>();
    }
}