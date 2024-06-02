using System.ComponentModel.DataAnnotations;

namespace EFCore_Api.Dto.Players
{
    public class UpdatePlayerRequest
    {
        [Required]
        public string NickName { get; set; }
    }
}
