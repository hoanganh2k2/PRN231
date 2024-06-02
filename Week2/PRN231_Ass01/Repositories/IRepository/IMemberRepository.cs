using BusinessObject.Models;

namespace Repositories.IRepository
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetAllMembersAsync();
        Task<Member> GetMemberByIdAsync(int memberId);
        Task<Boolean> AddMemberAsync(Member member);
        Task UpdateMemberAsync(Member member);
        Task DeleteMemberAsync(int memberId);
        Task<Member> LoginMemberAsync(string email, string password);
        Task<Member> GetMemberByEmail(string email);
    }

}
