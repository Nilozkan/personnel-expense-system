
using System.Threading.Tasks;
using Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Schema;
using WebAPI.CQRS.Command;
using WebAPI.CQRS.Query;
using WebAPI.DbContext;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController  : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMediator _mediator;

        public UserController(AppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
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


    }
}