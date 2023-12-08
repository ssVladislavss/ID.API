using Newtonsoft.Json;

namespace ID.Core.Users
{
    public class UserSearchFilter
    {
        private string? phone;
        private string? email;
        private string? lastName;
        private string? firstName;
        private string? secondName;
        private DateTime? birthDate;
        private string? role;

        public string? Phone => phone;
        public string? Email => email;
        public string? LastName => lastName;
        public string? FirstName => firstName;
        public string? SecondName => secondName;
        public DateTime? BirthDate => birthDate;
        public string? Role => role;

        public UserSearchFilter WithPhone(string phone)
        {
            this.phone = UserID.PhoneNormalize(phone);

            return this;
        }

        public UserSearchFilter WithEmail(string email)
        {
            this.email = email;

            return this;
        }

        public UserSearchFilter WithLastName(string lastName)
        {
            this.lastName = lastName;

            return this;
        }

        public UserSearchFilter WithFirstName(string firstName)
        {
            this.firstName = firstName;

            return this;
        }

        public UserSearchFilter WithSecondName(string secondName)
        {
            this.secondName = secondName;

            return this;
        }

        public UserSearchFilter WithBirthDate(DateTime birthDate)
        {
            this.birthDate = birthDate;

            return this;
        }

        public UserSearchFilter WithRole(string role)
        {
            this.role = role;

            return this;
        }

        internal void Apply(ref IQueryable<UserID> query)
        {
            if (BirthDate.HasValue)
                query = query.Where(x => x.BirthDate!.Value.Date == BirthDate.Value.Date);
            if (!string.IsNullOrEmpty(Phone))
                query = query.Where(x => x.PhoneNumber == Phone);
            if (!string.IsNullOrEmpty(Email))
                query = query.Where(x => x.Email == Email);
            if (!string.IsNullOrEmpty(FirstName))
                query = query.Where(x => x.FirstName == FirstName);
            if (!string.IsNullOrEmpty(LastName))
                query = query.Where(x => x.LastName == LastName);
            if (!string.IsNullOrEmpty(SecondName))
                query = query.Where(x => x.SecondName == SecondName);
        }

        public override string ToString()
            => JsonConvert.SerializeObject(this, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
    }
}
