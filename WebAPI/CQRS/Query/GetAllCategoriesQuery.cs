
using Base;
using MediatR;
using Schema;

namespace WebAPI.CQRS.Query
{
    public class GetAllCategoriesQuery : IRequest<BaseResponse<List<CategoryResponse>>>
    {
    }
    
}