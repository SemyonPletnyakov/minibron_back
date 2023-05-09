using Microsoft.IdentityModel.Tokens;
using MiniBron.Application.DTO;
using MiniBron.Application.Serviсes.Interfaces;
using MiniBron.Common;
using MiniBron.Domain;
using MiniBron.EntityFramework.Repository.Implementation;
using MiniBron.EntityFramework.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.Serviсes.Implementation
{
    public class AccountsService : IAccountsService
    {
        IUsersSelects usersSelects;
        public AccountsService()
        {
            usersSelects = new UsersSelects();
        }
        public AccountGetDTO LoginAccount(AccountLoginDTO accountLoginDTO)
        {
            try
            {
                ClaimsIdentity claimsIdentity;
                User user = usersSelects.GetUserByHotelLoginPassword(accountLoginDTO.HotelId, accountLoginDTO.Login, accountLoginDTO.Password);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role),
                        new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid", user.HotelId.ToString())
                    };
                    claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                }
                else return null;

                var now = DateTime.UtcNow;
                // создаем JWT-токен
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: claimsIdentity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                AccountGetDTO result = new AccountGetDTO
                {
                    JWTToken = encodedJwt,
                    Role = user?.Role,
                    FIO = user?.FIO
                };
                return result;
            }
            catch
            {
                return null;
            }
        }
        public AccounDTO GetAccountInfo(int userId, int hotelId)
        {
            User u = usersSelects.GetHotelUsersById(userId, hotelId);
            return new AccounDTO()
            {
                FIO = u?.FIO,
                Role = u?.Role,
                Login = u?.Login,
                Password = u?.Password
            };
        }
        public bool ChangeAccount(AccounChangeDTO accountChangeDTO, int userId, int hotelId)
        {
            return usersSelects.ChangeUser(new User()
            {
                Id = userId,
                HotelId = hotelId,
                FIO = accountChangeDTO.FIO,
                Login = accountChangeDTO.Login,
                Password = accountChangeDTO.Password
            }, false);
        }
    }
}
