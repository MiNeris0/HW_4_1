using HW_4_1.Dtos;
using HW_4_1.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_4_1.Services.Interfaces
{
    public interface IResourceService
    {
        Task<ResourceDto> GetResourceById(int id);
        Task<ListResponse<ResourceDto>> GetResourcesList();
    }
}
