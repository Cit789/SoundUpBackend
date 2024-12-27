using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundUp.Models;
using System.Security.Claims;


namespace SoundUp.Controllers
{
    [Controller]
    [Route("api/[controller]/[action]")]
    public class PatchRequestsUser(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext = dbContext;
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(Guid id, [FromBody] JsonPatchDocument<BaseUser> patchDoc)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (patchDoc == null)
                return BadRequest("Неккоректные данные");
            
            
            var user = await _dbcontext.AllUsers.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return NotFound("пользователь не найден");

            //if (userId != user.Id.ToString())
            //    return BadRequest("Данный пользователь не имеет прав на изменение");

            List<string> forbiddenFields = [ "Id", "CreatedAt", "UpdatedAt","RefreshTokenId","ListenHistoryId" ];

           
            foreach (var operation in patchDoc.Operations.ToList())
            {
                if (forbiddenFields.Contains(operation.path.TrimStart('/')))
                {

                    return BadRequest("Запрашиваемое изменение запрещено");
                }
            }
            if (!ModelState.IsValid)
                return BadRequest("Некорректные данные");

            patchDoc.ApplyTo(user, ModelState);



            await _dbcontext.SaveChangesAsync();

            return Ok("Данные изменены");
        }
    }
}
