using System;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;


namespace Package.Utilities.Net.Utilities
{
    public static class SecurityToken
    {
        public static string HashPassword(string password, out string salt)
        {
            // Generate a 128-bit salt using a sequence of cryptographically strong random bytes.
            byte[] saltBytes = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes); // The array is now filled with cryptographically strong random bytes.
            }

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            salt = Convert.ToBase64String(saltBytes);
            return hashed;
        }

        public static bool VerifyHashedPassword(string password, string salt, string hashedPassword)
        {
            // Generate a 128-bit salt using a sequence of cryptographically strong random bytes.
            byte[] saltBytes = Convert.FromBase64String(salt); // divide by 8 to convert bits to bytes

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            byte[] hashed = KeyDerivation.Pbkdf2(
                password: password!,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8);

            return CryptographicOperations.FixedTimeEquals(hashed, Convert.FromBase64String(hashedPassword));
        }

        public static string GenerateTokenAuthentication(string id, string name, string email, string username)
        {
            var decryptedKey = Encoding.ASCII.GetBytes("authenticationConfiguration.Value.JwtKey");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Hash, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Email, email ?? string.Empty),
                    new Claim(ClaimTypes.NameIdentifier, name ?? string.Empty),
                    new Claim(ClaimTypes.Sid, id.ToString() ?? string.Empty),
                    new Claim(ClaimTypes.UserData, username?? string.Empty)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(decryptedKey), SecurityAlgorithms.HmacSha256Signature),
            };

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            var bearerToken = tokenHandler.WriteToken(createdToken);

            return bearerToken;
        }

        /// <summary>
        /// Validates the token authentication.
        /// </summary>
        /// <param name="token"></param>
        /// <returns>string</returns>
        public static string ValidateTokenAuthentication(string? token)
        {
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("authenticationConfiguration.Value.JwtKey")),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
            };
            try
            {
                var result = tokenHandler.ValidateToken(token, tokenValidationParameters, out var _);
                Claim? claim = result.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
                if (claim != null)
                {
                    return claim.Value;
                }
            }
            catch (System.ArgumentException)
            {
                throw new CustomException("ResourceValidations.Forbidden");
            }
            catch (SecurityTokenExpiredException)
            {
                throw new CustomException("Token expired!!");
            }
            throw new CustomException("");
        }

        /// <summary>
        /// Gets the Username from the token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetUsername(string? token)
        {
            if (token == null)
            {
                return "Anonymous";
            }
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("authenticationConfiguration.Value.JwtKey")),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
            };
            var result = tokenHandler.ValidateToken(token, tokenValidationParameters, out var _);
            Claim? claim = result.Claims.FirstOrDefault(x => x.Type == ClaimTypes.UserData);
            if (claim != null)
            {
                return claim.Value;
            }
            throw new AuthenticationException("ResourceValidations.Forbidden");
        }
    }
}
