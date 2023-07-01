using BackEnd_SocialE.Learning.Domain.Models;
using BackEnd_SocialE.Learning.Domain.Repositories;
using BackEnd_SocialE.Learning.Domain.Services;
using BackEnd_SocialE.Learning.Domain.Services.Communication;
using BackEnd_SocialE.Security.Domain.Models;
using BackEnd_SocialE.Security.Domain.Repositories;

namespace BackEnd_SocialE.Learning.Services;

public class EventService : IEventService
{
    //
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public EventService(IEventRepository eventRepository, IUnitOfWork unitOfWork, IUserRepository userRepository) {
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Event>> ListAsync() {
        return await _eventRepository.ListAsync();
    }

    public async Task<IEnumerable<Event>> ListByUserIdAsync(int ManagerId)
    {
        return await _eventRepository.FindByUserIdAsync(ManagerId);
    }

    public async Task<EventResponse> SaveAsync(Event _event) {
        // Validación del ManagerId
        var existsUser = await _userRepository.FindByIdAsync(_event.ManagerId);
        var userManager = _userRepository.FindByIdAsync(_event.ManagerId).Result.Type=="manager";
        if (existsUser == null || !userManager) {return new EventResponse("El usuario no es válido");}
        
        try {
            await _eventRepository.AddAsync(_event);
            await _unitOfWork.CompleteAsync();
            return new EventResponse(_event);
        }
        catch (Exception e) {
            return new EventResponse($"Ocurrió un error mientras se guardaba el evento: {e.Message}");
        }
    }

    public async Task<EventResponse> UpdateAsync(int id, Event _event) {
        //Validación de si el evento existe
        var exists = await _eventRepository.FindByIdAsync(id);
        if (exists == null) {
            return new EventResponse("No se encontró el evento");
        }
        // Validación del ManagerId
        var existsUser = await _userRepository.FindByIdAsync(_event.ManagerId);
        var userManager = _userRepository.FindByIdAsync(_event.ManagerId).Result.Type=="manager";
        if (existsUser == null || !userManager) {return new EventResponse("El usuario no es válido");}

        exists.Name = _event.Name;
        exists.Description = _event.Description;
        exists.Price = _event.Price;
        exists.EventDate = _event.EventDate;
        exists.StartTime = _event.StartTime;
        exists.EndTime = _event.EndTime;
        exists.ManagerId = _event.ManagerId;
        exists.Image = _event.Image;
        try {
            _eventRepository.Update(exists);
            await _unitOfWork.CompleteAsync();
            return new EventResponse(exists);
        }
        catch (Exception e) {
            return new EventResponse($"Ocurrió un error mientras se actualizaba el evento: {e.Message}");
        }
    }

    public async Task<EventResponse> DeleteAsync(int id) {
        var exists = await _eventRepository.FindByIdAsync(id);
        if (exists == null) {
            return new EventResponse("No se encontró el evento");
        }

        try {
            _eventRepository.Remove(exists);
            await _unitOfWork.CompleteAsync();
            return new EventResponse(exists);
        }
        catch (Exception e) {
            return new EventResponse($"Ocurrió un error mientras se eliminaba el evento: {e.Message}");
        }
    }
}