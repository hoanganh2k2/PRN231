using BusinessObject;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class MemberDAO
    {
        private readonly MyDbContext _dbContext;

        public MemberDAO(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            return await _dbContext.Members.ToListAsync();
        }

        public async Task<Member> GetMemberByIdAsync(int memberId)
        {
            return await _dbContext.Members.FindAsync(memberId);
        }

        public async Task<Boolean> AddMemberAsync(Member member)
        {
            Member? existingMember = await _dbContext.Members.FirstOrDefaultAsync(m => m.Email == member.Email);

            if (existingMember != null)
                return false;

            _dbContext.Members.Add(member);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task UpdateMemberAsync(Member member)
        {
            _dbContext.Entry(member).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteMemberAsync(int memberId)
        {
            BusinessObject.Models.Member? member = await _dbContext.Members.FindAsync(memberId);
            if (member != null)
            {
                _dbContext.Members.Remove(member);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Member> LoginMemberAsync(string email, string password)
        {
            Member? user = await _dbContext.Members.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
            return user;
        }

        public async Task<Member> GetMemberByEmail(string email)
        {
            return await _dbContext.Members.FirstOrDefaultAsync(x => x.Email == email);
        }
    }

}