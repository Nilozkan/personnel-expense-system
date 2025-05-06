

using Base;
using MediatR;

namespace WebAPI.CQRS.Command
{
    public class DeleteCategoryCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
        public DeleteCategoryCommand(int id) => Id = id;
        
    }
}