using BloodBank.Application.Results;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Auth;
using MediatR;

namespace BloodBank.Application.Commands.Register;

public class RegisterHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IAuthService authService) : IRequestHandler<RegisterCommand, Result<int>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthService _authService = authService;

    public async Task<Result<int>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var isEmailInUse = await _userRepository.IsEmailInUseAsync(request.Email);
        var isCellPhoneNumberInUse = await _userRepository.IsCellPhoneNumberInUseAsync(request.CellPhoneNumber);

        if (isEmailInUse || isCellPhoneNumberInUse)
            return UserErrors.InvalidData;

        var passwordHash = _authService.HashPassword(request.Password);
        var user = request.ToEntity(passwordHash);

        await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return user.Id;
    }
}
