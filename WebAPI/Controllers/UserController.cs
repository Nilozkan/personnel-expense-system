
using System.Threading.Tasks;
using Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schema;
using WebAPI.CQRS.Command;
using WebAPI.CQRS.Query;
using WebAPI.DbContext;
using WebAPI.Service.Token;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController  : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly AppDbContext _context;
        private readonly IMediator _mediator;

        public UserController(ITokenService tokenService, AppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
            _tokenService = tokenService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);
            
            var command = new CreateUserCommand(request);
            var result = await _mediator.Send(command);
            return Ok(result);
           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id,[FromBody] UserRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateUserCommand(id, request);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteUserCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var query = new GetAllUsersQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
        {
            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserPartially(int id, [FromBody] UserUpdateRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateUserPartialCommand(id, request);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

         [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Unauthorized("Email veya şifre hatalı");

            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }


    }
}