﻿using ClassifiedAds.Application;
using ClassifiedAds.Modules.Identity.Entities;
using ClassifiedAds.Modules.Identity.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ClassifiedAds.Modules.Identity.Commands.Users;

public class AddRoleCommand : ICommand
{
    public User User { get; set; }
    public UserRole Role { get; set; }
}

public class AddRoleCommandHandler : ICommandHandler<AddRoleCommand>
{
    private readonly IUserRepository _userRepository;

    public AddRoleCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task HandleAsync(AddRoleCommand command, CancellationToken cancellationToken = default)
    {
        command.User.UserRoles.Add(command.Role);
        await _userRepository.AddOrUpdateAsync(command.User, cancellationToken);
        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
