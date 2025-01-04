using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Utility
{
    public static class SignService
    {
        public static SecurityKey GetSymetricSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
