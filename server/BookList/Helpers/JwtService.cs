using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BookList.Helpers
{
    public class JwtService : IJwtService
    {
        private string secureKey = "tabun uc tabos jomi dabun tospei";

        public string Generate(ObjectId id)
        {
            var symmKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var credentials = new SigningCredentials(symmKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);
            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));

            var jwtToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secureKey);

            tokenHandler.ValidateToken(jwt, new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false
            }, out SecurityToken validationResult);

            return (JwtSecurityToken)validationResult;
        }
    }
}
