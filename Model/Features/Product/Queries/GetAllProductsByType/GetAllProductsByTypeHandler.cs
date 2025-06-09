using MediatR;
using Model.DataAccess.Interfaces;

namespace Model.Features.Product.Queries.GetAllProductsByType;

public class GetAllProductsByTypeHandler(IProductDao productDao) : IRequestHandler<GetAllProductsByTypeQuery, object?>
{
    public Task<object?> Handle(GetAllProductsByTypeQuery request, CancellationToken cancellationToken)
    {
        return productDao.GetAllProductsByType(request.Type);
    }
}
