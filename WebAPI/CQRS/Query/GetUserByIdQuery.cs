

using Base;
using MediatR;
using Schema;

namespace WebAPI.CQRS.Query
{
    public class GetUserByIdQuery : IRequest<BaseResponse<UserResponse>>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
        
    }
}