using PasswordGeneratorFunction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordGeneratorFunction.Services
{
    public class PasswordGeneratorFunctionService
    {

        public class PasswordGeneratorService
        {

            public string GeneratePassword(PasswordRequest request)
            {
                var uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
                var numberChar = "0123456789";
                var specialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?";

                var charPool = new StringBuilder();
                if (request.IncludeUppercase) charPool.Append(uppercaseChars);
                if (request.IncludeLowercase) charPool.Append(lowercaseChars);
                if (request.IncludeNumbers) charPool.Append(numberChar);
                if (request.IncludeSpecialChars) charPool.Append(specialChars);

                if (charPool.Length == 0)
                    charPool.Append(lowercaseChars);


                var password = new StringBuilder();
                var rng = RandomNumberGenerator.Create();
                var poolLength = charPool.Length;


                var length = Math.Max(8, request.Length);

                var randomBytes = new byte[length];
                rng.GetBytes(randomBytes);

                for (int i = 0; i < length; i++)
                {
                    var index = randomBytes[i] % poolLength;
                    password.Append(charPool[index]);
                }

                return password.ToString();
            }

            public int CalculatePasswordStrength(string password)
            {
                int score = 0;

                if(password.Length >= 8) score++;
                if (password.Length >= 12) score++;

                if (password.Any(char.IsUpper)) score++;
                if (password.Any(char.IsLower)) score++;
                if (password.Any(char.IsDigit)) score++;
                if (password.Any(c => !char.IsLetterOrDigit(c))) score++;


                return Math.Min(5, score);

            }
        }

    }
}
