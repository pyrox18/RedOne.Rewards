using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

namespace RedOne.Rewards.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        private readonly IConfiguration _configuration;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected IDbConnection DbConnection
        {
            get
            {
                return new MySqlConnection(_configuration.GetConnectionString("RedOneRewards"));
            }
        }
    }
}
