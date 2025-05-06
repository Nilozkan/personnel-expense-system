

using Base;
using MediatR;
using Schema;

namespace WebAPI.CQRS.Command
{
    public class UpdateCategoryCommand : IRequest<BaseResponse<CategoryResponse>>
    {
        public int Id { get; set; }
        public CategoryRequest Model { get; set; }
        
        public UpdateCategoryCommand(int id, CategoryRequest model)
        {
            Id = id;
            Model = model;
        }
    }
}