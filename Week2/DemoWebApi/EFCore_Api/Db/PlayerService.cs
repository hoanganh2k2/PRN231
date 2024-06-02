using EFCore_Api.Dto;
using EFCore_Api.Dto.Players;

namespace EFCore_Api.Db
{
    public class PlayerService : IPlayerService
    {
        private readonly CodeFirstDemoContext _dbContext;
        public PlayerService(CodeFirstDemoContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task CreatePlayerAsync(CreatePlayerRequest playerRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePlayerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<GetPlayerResponse>> GetPlayerAsync(UrlQueryParameters urlQueryParameters)
        {
            throw new NotImplementedException();
        }

        public Task<GetPlayerDetailResponse> GetPlayerDetailAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePlayerAsync(int id, UpdatePlayerRequest playerRequest)
        {
            throw new NotImplementedException();
        }
    }
}
