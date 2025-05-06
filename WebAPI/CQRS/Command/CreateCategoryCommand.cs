

using Base;
using MediatR;
using Schema;

namespace WebAPI.CQRS.Command
{
    public class CreateCategoryCommand : IRequest<BaseResponse<CategoryResponse>>
    {
        public CategoryRequest Model { get; set; }
        public CreateCategoryCommand(CategoryRequest model) => Model = model;
    }
}