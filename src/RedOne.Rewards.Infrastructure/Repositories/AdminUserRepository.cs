using Dapper;
using Microsoft.Extensions.Configuration;
using RedOne.Rewards.Domain.Entities;
using RedOne.Rewards.Domain.Interfaces;
using System.Threading.Tasks;

namespace RedOne.Rewards.Infrastructure.Repositories
{
    public class AdminUserRepository : BaseRepository, IAdminUserRepository
    {
        public AdminUserRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<AdminUser> GetAdminUserByUsernameAsync(string username)
        {
            var query = "SELECT * FROM AdminUser WHERE Username = @Username";

            using (var connection = DbConnection)
            {
                return await connection.QuerySingleOrDefaultAsync<AdminUser>(query, new
                {
                    Username = username
                });
            }
        }
    }
}
