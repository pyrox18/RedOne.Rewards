using System.Threading.Tasks;

namespace RedOne.Rewards.Application.Interfaces
{
    public interface IMemberLevelService
    {
        Task SeedMemberLevelDataAsync();
    }
}
