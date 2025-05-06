
using Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schema;
using WebAPI.DbContext;
using WebAPI.Entities;

namespace WebAPI.CQRS.Command
{
    public class UserCommandHandler : 
    IRequestHandler<CreateUserCommand,BaseResponse<UserResponse>>,
    IRequestHandler<UpdateUserCommand,BaseResponse<UserResponse>>,
    IRequestHandler<DeleteUserCommand,BaseResponse<bool>>,
    IRequestHandler<UpdateUserPartialCommand, BaseResponse<UserResponse>>
    {
        private readonly AppDbContext _context;
        public UserCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;

            var user = new User
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Iban = model.Iban,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Password = null,
                RoleId = model.RoleId,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            var roleName = _context.Roles.FirstOrDefault(r => r.Id == user.RoleId)?.Name ?? "Unknown";

            var response = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Iban = user.Iban,
                RoleName = roleName,
            };
            
            return new BaseResponse<UserResponse>(response, "User created successfully");
        }

        public async Task<BaseResponse<UserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(new object[] {request.Id}, cancellationToken);
            if (user == null)
               return new BaseResponse<UserResponse>("User not found.");

            user.Name = request.Model.Name;
            user.Surname = request.Model.Surname;
            user.Email = request.Model.Email;
            user.Iban = request.Model.Iban;
            user.Password = request.Model.Password;
            user.RoleId = request.Model.RoleId;

            await _context.SaveChangesAsync(cancellationToken);

            var roleName = await _context.Roles
                .Where(r => r.Id == user.RoleId)
                .Select(r => r.Name)
                .FirstOrDefaultAsync(cancellationToken);

             var response = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Iban = user.Iban,
                RoleName = roleName ?? "Unknown"
            };
            
            return new BaseResponse<UserResponse>(response, "User updated successfully.");

        }

        public async Task<BaseResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(new object[] {request.Id}, cancellationToken);
            if (user == null)
                return new BaseResponse<bool>("User not found.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<bool>(true, "User deleted successfully. ");
        }

        public async Task<BaseResponse<UserResponse>> Handle(UpdateUserPartialCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user == null)
                return new BaseResponse<UserResponse>("User not found.");

            if(!string.IsNullOrEmpty(request.Model.Name))
                user.Name = request.Model.Name;

            if(!string.IsNullOrEmpty(request.Model.Surname))
                user.Surname = request.Model.Surname;
            
            if (!string.IsNullOrEmpty(request.Model.Email))
                user.Email = request.Model.Email;
           
            if (!string.IsNullOrEmpty(request.Model.Password))
                user.Password = request.Model.Password;

            if (!string.IsNullOrEmpty(request.Model.Iban))
                user.Iban = request.Model.Iban;
            
            if(request.Model.RoleId.HasValue)
            {
                var role = await _context.Roles.FindAsync(request.Model.RoleId.Value);
                if(role == null)
                   return new BaseResponse<UserResponse>("Role not found.");
                user.RoleId = role.Id;
            }

            await _context.SaveChangesAsync(cancellationToken);

             var response = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Iban = user.Iban,
                RoleName = user.Role.Name
            };
            return new BaseResponse<UserResponse>(response, "User updated successfully.");
        }
    }
}