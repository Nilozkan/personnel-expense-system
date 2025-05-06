
using Base;
using MediatR;
using Schema;
using WebAPI.DbContext;
using WebAPI.Entities;

namespace WebAPI.CQRS.Command
{
    public class CategoryCommandHandler :
    IRequestHandler<CreateCategoryCommand, BaseResponse<CategoryResponse>>,
    IRequestHandler<UpdateCategoryCommand, BaseResponse<CategoryResponse>>,
    IRequestHandler<DeleteCategoryCommand, BaseResponse<bool>>
    {
        private readonly AppDbContext _context;
        public CategoryCommandHandler(AppDbContext context) => _context = context;
        public async Task<BaseResponse<CategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
           var category = new Category { Name = request.Model.Name };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<CategoryResponse>(
                new CategoryResponse { Id = category.Id, Name = category.Name },
                "Category created successfully");
        }

        public async Task<BaseResponse<CategoryResponse>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.Id);
            if (category == null)
                return new BaseResponse<CategoryResponse>("Category not found");

            category.Name = request.Model.Name;
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<CategoryResponse>(
                new CategoryResponse { Id = category.Id, Name = category.Name },
                "Category updated successfully");
        }

        

        public async Task<BaseResponse<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.Id);
            if (category == null)
                return new BaseResponse<bool>("Category not found");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<bool>(true, "Category deleted successfully");
        }
    }
}