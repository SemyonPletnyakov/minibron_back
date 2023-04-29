using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Common
{
    public class AuthOptions
    {
        public const string ISSUER = "RestaurantServer";
        public const string AUDIENCE = "RestaurantClient";
        const string KEY = "flknasf32rfjhnewnih3iBFAUh3@e22!!";
        public const int LIFETIME = 600;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
