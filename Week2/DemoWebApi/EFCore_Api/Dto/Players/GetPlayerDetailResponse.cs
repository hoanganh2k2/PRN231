using EFCore_Api.Dto.PlayerInstrument;

namespace EFCore_Api.Dto.Players
{
    public class GetPlayerDetailResponse
    {
        public string NickName { get; set; }
        public DateTime JoinedDate { get; set; }
        public List<GetPlayerInstrumentResponse> getPlayerInstruments { get; set; }
    }
}
