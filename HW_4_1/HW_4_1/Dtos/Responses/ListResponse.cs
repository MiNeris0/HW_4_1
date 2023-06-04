using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_4_1.Dtos.Responses
{
    public class ListResponse<T>
        where T : class
    {
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }
        [JsonProperty(PropertyName = "per_page")]
        public int PerPage { get; set; }
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }
        [JsonProperty(PropertyName = "total_pages")]
        public int TotalPages { get; set; }
        public List<T> Data { get; set; }
        public SupportDto Support { get; set; }
    }
}
