using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_4_1.Dtos.Responses
{
    public class LoginResponse
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
    }
}
