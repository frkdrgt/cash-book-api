using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CashBook.Services
{
    public class JWTTokenManager
    {
        public static string CreateToken(string user, string secretkey = "", string issuer = "")
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user ?? ""));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() ?? ""));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.ToString() ?? ""));
            claims.Add(new Claim("UserId", user.ToString()));
            //foreach (var item in user.Roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, item.RoleName));
            //}

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretkey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: issuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(90),
                signingCredentials: creds);

            var usertoken = new JwtSecurityTokenHandler().WriteToken(token);
            return usertoken;
        }
    }
}