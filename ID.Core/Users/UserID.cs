﻿using Microsoft.AspNetCore.Identity;

namespace ID.Core.Users
{
    public class UserID : IdentityUser
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public DateTime? BirthDate { get; set; }
        public List<int>? AvailableFunctionality { get; set; }

        public static string CreatePassword(int length)
        {
            Random random = new(DateTime.Now.Millisecond);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";

            return new string(Enumerable.Repeat(chars, length).Select(st => st[random.Next(st.Length)]).ToArray());
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