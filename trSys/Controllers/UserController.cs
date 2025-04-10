using Microsoft.AspNetCore.Mvc;
using trSys.Interfaces;
using trSys.Models;
using trSys.Repos;

namespace trSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<User>
    {
        private readonly UserRepository _userRepository;

        public UserController(IRepository<User> repository) : base(repository) { }
        // методы юзеров
    }
}
