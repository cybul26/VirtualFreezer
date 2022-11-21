using MassTransit;
using VirtualFreezer.Identity.Application.Exceptions;
using VirtualFreezer.Identity.Domain.Repositories;
using VirtualFreezer.MessageContracts.AccountVerification;

namespace VirtualFreezer.Identity.Application.Features.SignUp;

public class AccountVerifiedConsumer : IConsumer<AccountVerified>
{
    private readonly IUserRepository _userRepository;

    public AccountVerifiedConsumer(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task Consume(ConsumeContext<AccountVerified> context)
    {
        var user = await _userRepository.GetByEmailAsync(context.Message.Email);
        if (user is null)
        {
            throw new UserNotFoundException(context.Message.Email);
        }
        user.Verify();
        await _userRepository.UpdateAsync(user);
    }
}