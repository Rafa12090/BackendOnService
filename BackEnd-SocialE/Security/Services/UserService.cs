using AutoMapper;
using BackEnd_SocialE.Learning.Domain.Repositories;
using BackEnd_SocialE.Security.Authorization.Handlers.Interfaces;
using BackEnd_SocialE.Security.Domain.Models;
using BackEnd_SocialE.Security.Domain.Repositories;
using BackEnd_SocialE.Security.Domain.Services.Communication;
using BackEnd_SocialE.Security.Exceptions;
using BCryptNet = BCrypt.Net.BCrypt;
namespace BackEnd_SocialE.Security.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtHandler _jwtHandler;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IJwtHandler jwtHandler, IMapper mapper) {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtHandler = jwtHandler;
        _mapper = mapper;
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        var user = await _userRepository.FindByEmailAsync(model.Email);
        Console.WriteLine($"Request: {model.Email}, {model.Password}");
        Console.WriteLine($"User: {user.Id}, {user.Username}, {user.Email}, {user.Type}, {user.PasswordHash}");
        
        // validate
        if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
        {
            Console.WriteLine("Authentication Error");
            throw new AppException("El Email o la contraseña es incorreta");
        }
            
        Console.WriteLine("Autentificación realizada, generando token");
        // authentication successful
        var response = _mapper.Map<AuthenticateResponse>(user);
        Console.WriteLine($"Response: {response.Id}, {response.Username}, {response.Email}, {response.Type}");
        response.Token = _jwtHandler.GenerateToken(user);
        Console.WriteLine($"El token generado es : {response.Token}");
        return response;
    }

    public async Task<IEnumerable<User>> ListAsync() {
        return await _userRepository.ListAsync();
    }

    public async Task<User> GetByIdAsync(int id) {
        var user = await _userRepository.FindByIdAsync(id);
        if (user == null) throw new KeyNotFoundException("Usuario no encontrado");
        return user;
    }

    public async Task RegisterAsync(RegisterRequest model) {
        if (_userRepository.ExistsByUsername(model.Username))
            throw new AppException("Ya equiste un usuario con el nombre : " + model.Username);
        var user = _mapper.Map<User>(model);
        user.PasswordHash = BCryptNet.HashPassword(model.Password);
        try {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e) {
            throw new AppException($"Un error ha ocurrrido al guardar el usuario : {e.Message}");
        }
    }
    
    private User GetById(int id) {
        var user = _userRepository.FindById(id);
        if (user == null) throw new KeyNotFoundException("Usuario no encontrado");
        return user;
    }
    
    public async Task UpdateAsync(int id, UpdateRequest model) {
        var user = GetById(id);
        if (_userRepository.ExistsByUsername(model.Username))
            throw new AppException("Ya equiste un usuario con el nombre : " + model.Username);
        if (!string.IsNullOrEmpty(model.Password)) {user.PasswordHash = BCryptNet.HashPassword(model.Password);}
        _mapper.Map(model, user);
        try {
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e) {
            throw new AppException($"Un error ha ocurrrido al actualizar el usuario : {e.Message}");
        }
    }

    public async Task DeleteAsync(int id) {
        var user = GetById(id);
        try {
            _userRepository.Remove(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e) {
            throw new AppException($"Un error ha ocurrrido al eliminar el usuario : {e.Message}");
        }
    }
}