using OPN.Domain;
using OPN.Domain.Login;
using OPN.Services.Interfaces;
using OPN.Services.Requests;

namespace OPN.Services;

public class LoginService: ILoginService
{
    private readonly IUnitOfWork _unitOfWork;

    public LoginService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<LoggedUser> Login(LoginRequest request)
    {
        var user = await _unitOfWork.UserRepository.Login(request.IDN);

        if(user == null)
        {
            user = await _unitOfWork.UserRepository.CreateUser(request.IDN, request.UserName);
            await _unitOfWork.CommitAsync();
        }
        
        return user;
    }
}
