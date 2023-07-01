using BackEnd_SocialE.Learning.Domain.Models;
using BackEnd_SocialE.Shared.Domain.Services.Communication;

namespace BackEnd_SocialE.Learning.Domain.Services.Communication;

public class PaymentResponse :BaseResponse<Payment>
{
    public PaymentResponse(string message) : base(message) { }

    public PaymentResponse(Payment resource) : base(resource) { }
}