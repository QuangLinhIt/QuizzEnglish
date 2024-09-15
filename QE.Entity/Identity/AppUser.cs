using Microsoft.AspNetCore.Identity;
using QE.Entity.Entity.Jwt;


namespace QE.Entity.Identity
{
    public class AppUser:IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
