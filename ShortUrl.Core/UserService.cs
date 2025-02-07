using AutoMapper;
using Microsoft.Extensions.Logging;
using ShortUrl.Core.DTO;
using ShortUrl.Core.Interfaces;
using ShortUrl.Entities;
using ShortUrl.Repository.Interfaces;

namespace ShortUrl.Core
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository, IPasswordService passwordService, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        public void Create(UserRegisterDTO userRegister)
        {
            try
            {
                var user = _mapper.Map<User>(userRegister);

                user.Password = _passwordService.HashInputPassword(user.Password);

                _userRepository.Create(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user.");
                throw;
            }
        }
    }
}