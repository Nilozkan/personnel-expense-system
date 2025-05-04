

using Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schema;
using WebAPI.DbContext;

namespace WebAPI.CQRS.Query
{
    public class UsersQueryHandler : 
    IRequestHandler<GetAllUsersQuery, BaseResponse<List<UserResponse>>>,
    IRequestHandler<GetUserByIdQuery, BaseResponse<UserResponse>>
    {
        private readonly AppDbContext _context;

        public UsersQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<List<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users
                .Include(u => u.Role)
                .Select(user => new UserResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Iban = user.Iban,
                    RoleName = user.Role.Name,
                })
                .ToListAsync(cancellationToken);
            return new BaseResponse<List<UserResponse>>(users, "Users listed successfully.");
        }

        public async Task<BaseResponse<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
             .Include(u => u.Role)
             .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
            
            if(user == null)
              return new BaseResponse<UserResponse>("User not found.");
            
            var response = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Iban = user.Iban,
                RoleName = user.Role.Name

            };
            return new BaseResponse<UserResponse>(response, "User retrieved successfully.");
        }
    }
}