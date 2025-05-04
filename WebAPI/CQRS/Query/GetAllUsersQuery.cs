
using Base;
using MediatR;
using Schema;

namespace WebAPI.CQRS.Query
{
    public class GetAllUsersQuery : IRequest<BaseResponse<List<UserResponse>>>
    {
        
    }
}