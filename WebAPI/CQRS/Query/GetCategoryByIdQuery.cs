

using Base;
using MediatR;
using Schema;

namespace WebAPI.CQRS.Query
{
    public class GetCategoryByIdQuery : IRequest<BaseResponse<CategoryResponse>>
    {
         public int Id { get; set; }
        public GetCategoryByIdQuery(int id) => Id = id;
    }
}