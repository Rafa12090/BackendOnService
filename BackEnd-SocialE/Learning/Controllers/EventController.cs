using AutoMapper;
using BackEnd_SocialE.Learning.Domain.Models;
using BackEnd_SocialE.Learning.Domain.Services;
using BackEnd_SocialE.Learning.Resources;
using BackEnd_SocialE.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_SocialE.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class EventController: ControllerBase {
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;

    public EventController(IEventService eventService, IMapper mapper) {
        _eventService = eventService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<EventResource>> GetAllAsync() {
        var events = await _eventService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Event>, IEnumerable<EventResource>>(events);
        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveEventResource resource)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState.GetErrorMessages()); }
        var @event = _mapper.Map<SaveEventResource, Event>(resource);
        var result = await _eventService.SaveAsync(@event);
        if (!result.Success)
            return BadRequest(result.Message);
        var eventResource = _mapper.Map<Event,EventResource>(result.Resource);
        return Ok(eventResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveEventResource resource) {
        if (!ModelState.IsValid) {return BadRequest(ModelState.GetErrorMessages());}
        var @event = _mapper.Map<SaveEventResource,Event>(resource);
        var result = await _eventService.UpdateAsync(id, @event);
        if (!result.Success)
            return BadRequest(result.Message);
        var eventResource = _mapper.Map<Event,EventResource>(result.Resource);
        return Ok(eventResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id) {
        var result = await _eventService.DeleteAsync(id);
        if (!result.Success) {return BadRequest(result.Message);}
        var eventResource = _mapper.Map<Event,EventResource>(result.Resource);
        return Ok(eventResource);
    }
}