using QE.Entity.Entity.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Model
{
    public partial class AppUserModel
    {
        public required string Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string FullName { get; set; } = "";
        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
