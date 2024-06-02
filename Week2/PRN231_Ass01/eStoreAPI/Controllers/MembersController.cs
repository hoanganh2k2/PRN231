using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;
using Repositories.Repository;

namespace eStoreAPI.Controllers
{
    [Route("api/members")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberRepository _member = new MemberRepository();


        [HttpGet]
        public async Task<IEnumerable<Member>> GetMembers()
        {
            return await _member.GetAllMembersAsync();
        }


        [HttpGet("{id:int}", Name = "GetMember")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            if (id <= 0)
                return BadRequest();

            Member? member = await _member.GetMemberByIdAsync(id);
            if (member == null)
                if (member == null)
                    return NotFound();

            return Ok(member);
        }


        [HttpPost]
        public async Task<ActionResult> Post(Member member)
        {
            if (member == null)
                return BadRequest();

            Task<Member> member1 = _member.GetMemberByEmail(member.Email);
            if (member1 != null)
            {
                ModelState.AddModelError("CustomerError", "Email member already Exists!");
                return BadRequest(ModelState);
            }

            Boolean check = await _member.AddMemberAsync(member);
            if (check)
            {
                return NoContent();
            }
            return BadRequest();
        }


        [HttpPut("{id:int}", Name = "UpdateMember")]
        public async Task<ActionResult> UpdateMember(Member member)
        {
            await _member.UpdateMemberAsync(member);
            return NoContent();
        }


        [HttpDelete("{id:int}", Name = "DeleteMember")]
        public async Task<ActionResult> DeleteMember(int id)
        {
            await _member.DeleteMemberAsync(id);
            return NoContent();
        }

        [HttpGet("Login")]
        public async Task<Member> Login(string email, string password)
        {
            return await _member.LoginMemberAsync(email, password);
        }
        [HttpGet("Email")]
        public async Task<Member> GetEmail(string email)
        {
            return await _member.GetMemberByEmail(email);
        }
    }
}
