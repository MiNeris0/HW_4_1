using HW_4_1.Config;
using HW_4_1.Dtos.Responses;
using HW_4_1.Dtos;
using HW_4_1.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_4_1.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IInternalHttpClientService _httpClientService;
        private readonly ILogger<UserService> _logger;
        private readonly ApiOption _options;
        private readonly string _resourceApi = "api/unknown/";

        public ResourceService(
        IInternalHttpClientService httpClientService,
        IOptions<ApiOption> options,
        ILogger<UserService> logger)
        {
            _httpClientService = httpClientService;
            _logger = logger;
            _options = options.Value;
        }

        public async Task<ListResponse<ResourceDto>> GetResourcesList()
        {
            var result = await _httpClientService.SendAsync<ListResponse<ResourceDto>, object>($"{_options.Host}{_resourceApi}", HttpMethod.Get);

            if (result != null)
            {
                _logger.LogInformation($"Page: {result.Page},\n\tPer page: {result.PerPage}\n\t,Total: {result.Total}\n\tTotal pages: {result.TotalPages}\n");

                foreach (var resource in result.Data)
                {
                    _logger.LogInformation($"id = {resource.Id}\n\tName: {resource.Name}\n\tYear: {resource.Year}\n\tColor: {resource.Color}\n\tPantone value: {resource.PantoneValue}");
                }

                _logger.LogInformation($"Support:\n\tUrl: {result.Support.Url}\n\tText: {result.Support.Text}");
            }

            return result;
        }

        public async Task<ResourceDto> GetResourceById(int id)
        {
            var result = await _httpClientService.SendAsync<BaseResponse<ResourceDto>, object>($"{_options.Host}{_resourceApi}{id}", HttpMethod.Get);

            if (result != null)
            {
                _logger.LogInformation($"Resource with id = {result.Data.Id} was found.\n\tName: {result.Data.Name}\n\tYear: {result.Data.Year}\n\tColor: {result.Data.Color}\n\tPantone value: {result.Data.PantoneValue}");
                _logger.LogInformation($"Support:\n\tUrl: {result.Support.Url}\n\tText: {result.Support.Text}");
            }
            else
            {
                _logger.LogInformation($"Resource with id = {id} was not found.");
            }

            return result?.Data;
        }
    }
}
