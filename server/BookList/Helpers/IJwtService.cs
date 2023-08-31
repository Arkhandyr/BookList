using MongoDB.Bson;
using System.IdentityModel.Tokens.Jwt;

namespace BookList.Helpers
{
    public interface IJwtService
    {
        public string Generate(ObjectId id);

        public JwtSecurityToken Verify(string jwt);
    }
}
