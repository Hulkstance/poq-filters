using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Poq.Api.Contracts.Data;
using Poq.Results;
using Poq.Results.Errors;

namespace Poq.Api.Services;

public sealed class ProductService : IProductService
{
    private const string BaseUrl = "http://www.mocky.io/v2/5e307edf3200005d00858b49";
    private const int MaxRetries = 5;

    private readonly TimeSpan _backoffInterval = TimeSpan.FromSeconds(5);

    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<Result<GetAllProductsResponse>> GetAllAsync()
    {
        return Observable
            .FromAsync(() => _httpClient.GetAsync(BaseUrl))
            .SubscribeOn(TaskPoolScheduler.Default)
            .Retry(MaxRetries)
            .Timeout(_backoffInterval)
            .SelectMany(async x =>
            {
                var response = await x.Content.ReadFromJsonAsync<GetAllProductsResponse>();
                return Result<GetAllProductsResponse>.FromSuccess(response!);
            })
            .Catch<Result<GetAllProductsResponse>, TimeoutException>(_ =>
                Observable.Return(Result<GetAllProductsResponse>.FromError(new TimeoutError())))
            .Catch<Result<GetAllProductsResponse>, Exception>(ex =>
                Observable.Return(Result<GetAllProductsResponse>.FromError(new ExceptionError(ex))))
            .ToTask();
    }
}
