using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BookList.Helpers
{
    public class JwtService : IJwtService
    {
        private string secureKey = "tabun uc tabos jomi dabun tospei";

        public string Generate(int id)
        {
            var symmKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var credentials = new SigningCredentials(symmKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);
            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));

            var jwtToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
