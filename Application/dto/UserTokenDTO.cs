using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.dto
{
    public class UserTokenDTO
    {
        public string? Token { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public IList<string>? Roles { get; set; } = new List<string>();
    }
}
