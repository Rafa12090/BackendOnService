using AutoMapper;
using BackEnd_SocialE.Learning.Domain.Models;
using BackEnd_SocialE.Learning.Resources;

namespace BackEnd_SocialE.Learning.Mapping;

public class ModelToResourceProfile: Profile {
    public ModelToResourceProfile() {
        CreateMap<Event, EventResource>();
        CreateMap<Payment, PaymentResource>();
    }
}