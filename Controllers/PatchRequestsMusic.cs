using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundUp.Models;
using System.Security.Claims;

namespace SoundUp.Controllers
{
    [Controller]
    [Route("api/[controller]/[action]")]
    public class PatchRequestsMusic(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext = dbContext;
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchMusic(Guid id, [FromBody] JsonPatchDocument<Music> patchDoc)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (patchDoc == null)
                return BadRequest("некорректный запрос");

            List<string> forbiddenFields = ["Id", "CreatedAt", "UpdatedAt", "AuthorId", "MusicAudioId"];


            foreach (var operation in patchDoc.Operations.ToList())
            {
                if (forbiddenFields.Contains(operation.path.TrimStart('/')))
                {

                    return BadRequest("Запрашиваемое изменение запрещено");
                }
            }
            var music = await _dbcontext.Music.FirstOrDefaultAsync(u => u.Id == id);
            if (music == null)
                return NotFound("музыка не найдена");

            //if (music.AuthorId.ToString() != userId)
            //    return BadRequest("Данный пользователь не имеет права изменять музыку");

            if (!ModelState.IsValid)
                return BadRequest("Некорректные данные");

            patchDoc.ApplyTo(music, ModelState);



            await _dbcontext.SaveChangesAsync();

            return Ok("Данные изменены");
        }
    }
}
