using RedOne.Rewards.Application.Dtos;
using RedOne.Rewards.Application.Exceptions;
using RedOne.Rewards.Application.Interfaces;
using RedOne.Rewards.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedOne.Rewards.Application.Services
{
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _bannerRepository;

        public BannerService(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        public async Task<IEnumerable<BannerDto>> GetBannersAsync()
        {
            var result = await _bannerRepository.GetAllAsync();

            return result.Select(r => new BannerDto(r));
        }

        public async Task<BannerDto> CreateBannerAsync(CreateBannerDto dto)
        {
            var result = await _bannerRepository.InsertAsync(dto.ToBanner());

            return new BannerDto(result);
        }

        public async Task DeleteBannerAsync(int id)
        {
            if (!await _bannerRepository.ExistsAsync(id))
                throw new NotFoundException($"Banner with ID {id} not found.");

            await _bannerRepository.DeleteByIdAsync(id);
        }
    }
}
