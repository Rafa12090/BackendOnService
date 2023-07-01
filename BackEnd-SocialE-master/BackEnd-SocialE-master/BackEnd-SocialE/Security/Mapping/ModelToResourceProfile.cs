using AutoMapper;
using BackEnd_SocialE.Security.Domain.Models;
using BackEnd_SocialE.Security.Domain.Services.Communication;
using BackEnd_SocialE.Security.Resources;

namespace BackEnd_SocialE.Security.Mapping;

public class ModelToResourceProfile : Profile {
    public ModelToResourceProfile()
    {
        CreateMap<User, AuthenticateResponse>();
        CreateMap<User, UserResource>();
    }
}