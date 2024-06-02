using BusinessObject;
using BusinessObject.Models;
using DataAccess;
using Repositories.IRepository;

namespace Repositories.Repository
{
    public sealed class MemberRepository : IMemberRepository
    {

        public MemberDAO _context { get; }

        public MemberRepository() => _context = new MemberDAO(new MyDbContext());

        public async Task<IEnumerable<Member>> GetAllMembersAsync() => await _context.GetAllMembersAsync();

        public async Task<Member> GetMemberByIdAsync(int memberId) => await _context.GetMemberByIdAsync(memberId);

        public async Task<Boolean> AddMemberAsync(Member member) => await _context.AddMemberAsync(member);

        public async Task UpdateMemberAsync(Member member) => await _context.UpdateMemberAsync(member);

        public async Task DeleteMemberAsync(int memberId) => await _context.DeleteMemberAsync(memberId);

        public async Task<Member> LoginMemberAsync(string email, string password) => await _context.LoginMemberAsync(email, password);

        public async Task<Member> GetMemberByEmail(string email) => await _context.GetMemberByEmail(email);
    }

}
