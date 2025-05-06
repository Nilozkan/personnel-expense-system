

using Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schema;
using WebAPI.DbContext;

namespace WebAPI.CQRS.Query
{
    public class CategoryQueryHandler :
    IRequestHandler<GetAllCategoriesQuery, BaseResponse<List<CategoryResponse>>>,
    IRequestHandler<GetCategoryByIdQuery, BaseResponse<CategoryResponse>>
    {
        private readonly AppDbContext _context;
        public CategoryQueryHandler(AppDbContext context) => _context = context;
        public async Task<BaseResponse<List<CategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories
                .Select(c => new CategoryResponse { Id = c.Id, Name = c.Name })
                .ToListAsync(cancellationToken);

            return new BaseResponse<List<CategoryResponse>>(categories, "Categories listed successfully");
        }

        public async Task<BaseResponse<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .Where(c => c.Id == request.Id)
                .Select(c => new CategoryResponse { Id = c.Id, Name = c.Name })
                .FirstOrDefaultAsync(cancellationToken);

            if (category == null)
                return new BaseResponse<CategoryResponse>("Category not found");

            return new BaseResponse<CategoryResponse>(category, "Category retrieved successfully");
        }
    }
}