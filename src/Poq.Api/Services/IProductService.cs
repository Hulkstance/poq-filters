using Poq.Api.Contracts.Data;
using Poq.Results;

namespace Poq.Api.Services;

public interface IProductService
{
    Task<Result<GetAllProductsResponse>> GetAllAsync();
}
