using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_4_1.Dtos.Responses
{
    public class UserUpdateResponse
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public int Id { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
