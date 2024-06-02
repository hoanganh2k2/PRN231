using EFCore_Api.Dto.PlayerInstrument;
using System.ComponentModel.DataAnnotations;

namespace EFCore_Api.Dto.Players
{
    public class CreatePlayerRequest
    {
        [Required]
        public string NickName { get; set; }
        [Required]
        public List<CreatePlayerInstrumentRequest> PlayerInstruments { get; set; }

    }
}
