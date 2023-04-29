using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Common
{
    public static class GetJwt
    {
        public static string GetJwtToken(this ControllerBase b)
        {
            return (b.Request.Headers.ContainsKey("authorization") ? b.Request.Headers["authorization"] : b.Request.Headers["Authorization"]).ToString().Replace("Bearer ", "");
        }
        public static int GetHotelIdFromJwtToken(this ControllerBase b)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(b.GetJwtToken());
            var tokenS = (JwtSecurityToken)jsonToken;

            int hotelId = Convert.ToInt32(tokenS.Claims.
                Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").
                Select(claim => claim.Value).FirstOrDefault());
            return hotelId;
        }
        public static int GetUserIdFromJwtToken(this ControllerBase b)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(b.GetJwtToken());
            var tokenS = (JwtSecurityToken)jsonToken;

            int hotelId = Convert.ToInt32(tokenS.Claims.
                Where(c => c.Type == ClaimsIdentity.DefaultNameClaimType).
                Select(claim => claim.Value).FirstOrDefault());
            return hotelId;
        }
    }
}
