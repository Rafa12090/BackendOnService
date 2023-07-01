using BackEnd_SocialE.Learning.Domain.Models;
using BackEnd_SocialE.Learning.Domain.Repositories;
using BackEnd_SocialE.Learning.Domain.Services;
using BackEnd_SocialE.Learning.Domain.Services.Communication;
using BackEnd_SocialE.Security.Domain.Repositories;

namespace BackEnd_SocialE.Learning.Services;

public class PaymentService: IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public PaymentService(IPaymentRepository paymentRepository, IEventRepository eventRepository, IUnitOfWork unitOfWork, IUserRepository userRepository) {
        _paymentRepository = paymentRepository;
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Payment>> ListAsync() {
        return await _paymentRepository.ListAsync();
    }

    public async Task<IEnumerable<Payment>> ListByUserIdAsync(int UserId) {
        return await _paymentRepository.FindByUserIdAsync(UserId);
    }

    public async Task<IEnumerable<Payment>> ListByEventIdAsync(int EventId) {
        return await _paymentRepository.FindByEventIdAsync(EventId);
    }
    
    public async Task<PaymentResponse> SaveAsync(Payment payment) {
        // Validación del UserId
        var existsUser = await _userRepository.FindByIdAsync(payment.UserId);
        if (existsUser == null) {return new PaymentResponse("El usuario no es válido");}
        // Validación del EventId
        var existsEvent = await _eventRepository.FindByIdAsync(payment.EventId);
        if (existsEvent == null) {return new PaymentResponse("El evento no es válido");}

        try {
            await _paymentRepository.AddAsync(payment);
            await _unitOfWork.CompleteAsync();
            return new PaymentResponse(payment);
        }
        catch (Exception e) {
            return new PaymentResponse($"Ocurrió un error mientras se guardaba el pago: {e.Message}");
        }
    }

    public async Task<PaymentResponse> UpdateAsync(int id, Payment payment) {
        //Validación de si el evento existe
        var exists = await _paymentRepository.FindByIdAsync(id);
        if (exists == null) {
            return new PaymentResponse("No se encontró la transacción");
        }
        // Validación del UserId
        var existsUser = await _userRepository.FindByIdAsync(payment.UserId);
        if (existsUser == null) {return new PaymentResponse("El usuario no es válido");}
        // Validación del EventId
        var existsEvent = await _eventRepository.FindByIdAsync(payment.EventId);
        if (existsEvent == null) {return new PaymentResponse("El evento no es válido");}

        exists.CardNumber = payment.CardNumber;
        exists.Type = payment.Type;
        exists.Direction = payment.Direction;
        exists.Phone = payment.Phone;
        exists.UserName = payment.UserName;
        exists.UserLastName = payment.UserLastName;
        exists.Country = payment.Country;
        exists.ExpiryDate = payment.ExpiryDate;
        exists.SecurityCode = payment.SecurityCode;
        exists.PostalCode = payment.PostalCode;
        exists.UserId = payment.UserId;
        exists.EventId = payment.EventId;
        try {
            _paymentRepository.Update(exists);
            await _unitOfWork.CompleteAsync();
            return new PaymentResponse(exists);
        }
        catch (Exception e) {
            return new PaymentResponse($"Ocurrió un error mientras se actualizaba la tranasacción: {e.Message}");
        }
    }

    public async Task<PaymentResponse> DeleteAsync(int id) {
        var exists = await _paymentRepository.FindByIdAsync(id);
        if (exists == null) {
            return new PaymentResponse("No se encontró el evento");
        }

        try {
            _paymentRepository.Remove(exists);
            await _unitOfWork.CompleteAsync();
            return new PaymentResponse(exists);
        }
        catch (Exception e) {
            return new PaymentResponse($"Ocurrió un error mientras se eliminaba la transacción: {e.Message}");
        }
    }
}