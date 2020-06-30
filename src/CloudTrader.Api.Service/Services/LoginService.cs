﻿using System.Threading.Tasks;
using CloudTrader.Api.Service.Exceptions;
using CloudTrader.Api.Service.Interfaces;
using CloudTrader.Api.Service.Models;

namespace CloudTrader.Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;

        private readonly ITokenGenerator _tokenGenerator;

        private readonly IPasswordUtils _passwordUtils;

        public LoginService(IUserRepository userRepository, ITokenGenerator tokenGenerator, IPasswordUtils passwordUtils)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _passwordUtils = passwordUtils;
        }

        public async Task<AuthDetails> Login(string username, string password)
        {
            var user = await _userRepository.GetUser(username);
            if (user == null)
            {
                throw new UnauthorizedException("Username or password is incorrect");
            }

            var success = _passwordUtils.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);
            if (!success)
            {
                throw new UnauthorizedException("Username or password is incorrect");
            }

            var token = _tokenGenerator.GenerateToken(user.Id);

            return new AuthDetails
            {
                Id = user.Id,
                Username = user.Username,
                Token = token
            };
        }
    }
}