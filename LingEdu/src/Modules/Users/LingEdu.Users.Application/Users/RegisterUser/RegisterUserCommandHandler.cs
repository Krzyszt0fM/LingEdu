using LingEdu.BuildingBlocks.Application.Common;
using LingEdu.BuildingBlocks.Application.Cqrs;
using LingEdu.Users.Application.Common;
using LingEdu.Users.Domain.Enums;
using LingEdu.Users.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace LingEdu.Users.Application.Users.RegisterUser
{
    internal sealed class RegisterUserCommandHandler
        : ICommandHandler<RegisterUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegisterUserCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var email = request.Email.Trim();

            var exists = await _userRepository.EmailExistsAsync(email, cancellationToken);
            if (exists)
            {
                return Result<UserDto>.Failure("EmailAlreadyInUse");
            }

            var language = (Language)request.Language;
            var user = User.Create(email, request.UserName.Trim(), "TEMP_HASH", language);
            var hashed = _passwordHasher.HashPassword(user, request.Password);
            user.ChangePassword(hashed);
            await _userRepository.AddAsync(user, cancellationToken);

            var dto = user.ToDto();
            return Result<UserDto>.Success(dto);
        }
    }
}