using System.Net.Http;
using System.Threading.Tasks;

namespace HW_4_1.Services.Interfaces;

public interface IInternalHttpClientService
{
    Task<TResponse> SendAsync<TResponse, TRequest>(string url, HttpMethod method, TRequest content = null)
        where TRequest : class;
}