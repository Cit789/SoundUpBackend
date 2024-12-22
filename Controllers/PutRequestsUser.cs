using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundUp.Contracts;
using SoundUp.Interfaces.Auth;

namespace SoundUp.Controllers
{
    [Controller]
    [Route("api/[controller]/[action]")]
    public class PutRequestsUser(ApplicationDbContext dbcontext, IPasswordHasher passwordhasher) : ControllerBase
    {
        private readonly IPasswordHasher _passwordHasher = passwordhasher;
        private readonly ApplicationDbContext _dbcontext = dbcontext;
        
    }
}
