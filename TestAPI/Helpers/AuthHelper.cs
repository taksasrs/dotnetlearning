// using System;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;

// namespace TestAPI.Helpers{
//     public class AuthHelper{
//         public string GenerateJWTToken(SystemUser user) {
//         var claims = new List<Claim> {
//             new(ClaimTypes.NameIdentifier, user.Id.ToString()),
//             new(ClaimTypes.Name, user.Name),
//         };
//         var jwtToken = new JwtSecurityToken(
//             claims: claims,
//             notBefore: DateTime.UtcNow,
//             expires: DateTime.UtcNow.AddDays(30),
//             signingCredentials: new SigningCredentials(
//                 new SymmetricSecurityKey(
//                 Encoding.UTF8.GetBytes(configuration["ApplicationSettings:JWT_Secret"])
//                     ),
//                 SecurityAlgorithms.HmacSha256Signature)
//             );
//         return new JwtSecurityTokenHandler().WriteToken(jwtToken);
//         }
//     }
// }