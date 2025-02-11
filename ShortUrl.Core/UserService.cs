using AutoMapper;
using Microsoft.Extensions.Logging;
using ShortUrl.Core.DTO;
using ShortUrl.Core.Exceptions;
using ShortUrl.Core.Interfaces;
using ShortUrl.Core.Models;
using ShortUrl.Entities;
using ShortUrl.Repository.Interfaces;

namespace ShortUrl.Core
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IMapper _mapper;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository, IPasswordHashService passwordHashService, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
            _mapper = mapper;
        }

        public void Create(UserRegisterDTO userRegisterDto)
        {
            try
            {
                var userEntity = _mapper.Map<UserEntity>(userRegisterDto);

                userEntity.Password = _passwordHashService.HashInputPassword(userEntity.Password);

                _userRepository.Create(userEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public UserCredentialsModel GetCredentialsByEmail(string email)
        {
            try
            {
                var userEntity = _userRepository.GetByEmail(email);

                if (userEntity is null)
                    throw new UserNotFound($"User with Email {email} was not found.");

                return _mapper.Map<UserCredentialsModel>(userEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public UserProfileDto GetProfileById(int id)
        {
            try
            {
                var user = _userRepository.GetById(id);

                if(user is null)
                    throw new UserNotFound($"User with Id {id} was not found.");

                return _mapper.Map<UserProfileDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public int GetIdByEmail(string email)
        {
            try
            {
                var idUser = _userRepository.GetIdByEmail(email);

                if(idUser == 0)
                    throw new UserNotFound($"User with Email {email} was not found.");

                return idUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}