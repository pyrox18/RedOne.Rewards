using RedOne.Rewards.Domain.Entities;
using System.Threading.Tasks;

namespace RedOne.Rewards.Domain.Interfaces
{
    public interface IConsumerUserRepository
    {
        Task<ConsumerUser> GetConsumerUserByPhoneNumberAsync(string phoneNumber);
        Task InsertAsync(ConsumerUser consumerUser);
    }
}
