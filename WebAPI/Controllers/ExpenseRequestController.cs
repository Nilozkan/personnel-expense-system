
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schema;
using WebAPI.CQRS;
using WebAPI.CQRS.Command;
using WebAPI.CQRS.Query;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseRequestController : ControllerBase
    {
         private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExpenseRequestController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetCurrentUserId()
{
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token");
            }
            
            return userId;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExpenseRequestRequest request)
        {
            var command = new CreateExpenseRequestCommand(request, GetCurrentUserId());
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExpenseRequestRequest request)
        {
            var command = new UpdateExpenseRequestCommand(id, request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteExpenseRequestCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}/approve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int id, [FromBody] ApproveExpenseRequest request)
        {
            var command = new ApproveExpenseRequestCommand(id, request.IsApproved, request.RejectionReason);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? userId)
        {
            var query = new GetAllExpenseRequestsQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetExpenseRequestByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}