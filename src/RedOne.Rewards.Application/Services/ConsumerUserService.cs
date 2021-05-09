using CryptoHelper;
using RedOne.Rewards.Application.Dtos;
using RedOne.Rewards.Application.Exceptions;
using RedOne.Rewards.Application.Interfaces;
using RedOne.Rewards.Domain.Entities;
using RedOne.Rewards.Domain.Interfaces;
using System.Threading.Tasks;

namespace RedOne.Rewards.Application.Services
{
    public class ConsumerUserService : IConsumerUserService
    {
        private readonly IConsumerUserRepository _consumerUserRepository;

        public ConsumerUserService(IConsumerUserRepository consumerUserRepository)
        {
            _consumerUserRepository = consumerUserRepository;
        }

        public async Task<bool> AuthenticateUserAsync(AuthenticateConsumerUserDto dto)
        {
            var user = await _consumerUserRepository.GetConsumerUserByPhoneNumberAsync(dto.PhoneNumber);
            if (user is null)
                return false;

            return Crypto.VerifyHashedPassword(user.Password, dto.Password);
        }

        public async Task<ConsumerUserInfoDto> GetConsumerUserInfoAsync(string phoneNumber)
        {
            var user = await _consumerUserRepository.GetConsumerUserByPhoneNumberAsync(phoneNumber);
            if (user is null)
                throw new NotFoundException($"Consumer user with phone number {phoneNumber} not found.");

            return new ConsumerUserInfoDto(user);
        }

        public async Task SeedConsumerUserDataAsync()
        {
            if (await _consumerUserRepository.GetConsumerUserByPhoneNumberAsync("018009999") is null)
            {
                var user = new ConsumerUser
                {
                    PhoneNumber = "018009999",
                    Password = Crypto.HashPassword("imredone"),
                    Name = "Test User",
                    EmailAddress = "test@user.com",
                    IsActive = true,
                    IsIDDActivated = false,
                    IsRoamingActivated = false,
                    TotalRewardPoints = 0
                };

                await _consumerUserRepository.InsertAsync(user);
            }
        }
    }
}
