using Microsoft.AspNetCore.Identity;

namespace ID.Core.Users
{
    public class UserID : IdentityUser
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public DateTime? BirthDate { get; set; }
        public List<int>? AvailableFunctionality { get; set; }

        public static string GeneratePassword(int length)
        {
            Random random = new(DateTime.Now.Millisecond);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";

            return new string(Enumerable.Repeat(chars, length).Select(st => st[random.Next(st.Length)]).ToArray());
        }

        public string GenerateCode(int codeLength)
        {
            if(codeLength > 50)
                throw new NotSupportedException($"the code length of more than 50 characters is not supported");

            string code = string.Empty;
            Random random = new();

            int numberOfAttempts = 1000;

            while (code.Length < codeLength && numberOfAttempts != 0)
            {
                numberOfAttempts--;

                var value = codeLength >= 10 
                    ? random.Next(0, 99).ToString()
                    : random.Next(0, 9).ToString();

                if (!code.Contains(value)) code += value;
                else continue;
            }

            return code;
        }

        /// <summary>
        /// Приводит номер телефона к общепринятому стандарту (+79998887766)
        /// </summary>
        /// <returns><see cref="string"/> or null</returns>
        public static string? PhoneNormalize(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                char[] numbers = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                value = new string(value.Where(x => numbers.Contains(x)).ToArray());

                if (value.StartsWith("8")) return $"+7{value[1..]}";
                else return $"+{value}";
            }

            return null;
        }
    }
}
