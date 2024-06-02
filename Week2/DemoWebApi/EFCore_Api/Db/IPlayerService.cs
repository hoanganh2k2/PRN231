using EFCore_Api.Dto;
using EFCore_Api.Dto.Players;

namespace EFCore_Api.Db
{
    public interface IPlayerService
    {
        Task CreatePlayerAsync(CreatePlayerRequest playerRequest);
        Task<bool> UpdatePlayerAsync(int id, UpdatePlayerRequest playerRequest);
        Task<bool> DeletePlayerAsync(int id);
        Task<GetPlayerDetailResponse> GetPlayerDetailAsync(int id);
        Task<PagedResponse<GetPlayerResponse>> GetPlayerAsync(UrlQueryParameters urlQueryParameters);
    }
}
