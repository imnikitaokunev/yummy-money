using System;
using System.Collections.Generic;
using System.Linq;

namespace CostAccounting.Core.Entities.Membership
{
    public class User : Entity<Guid>
    {
        public const int EmailLength = 128;
        public const int UsernameMinLength = 8;
        public const int UsernameMaxLength = 128;
        public const int PasswordHashLength = 128;
        public const int PasswordSaltLength = 128;
        public const int FirstNameLength = 128;
        public const int LastNameLength = 128;

        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisteredAt { get; set; }
        public byte[] Photo { get; private set; }

        public ICollection<UserRole> Roles { get; }

        public User()
        {
            Roles = new HashSet<UserRole>();
        }

        public User SetPhoto(byte[] photo)
        {
            if (Photo == null)
            {
                Photo = photo;
                return this;
            }

            if (!Photo.SequenceEqual(photo))
            {
                Photo = photo;
            }

            return this;
        }
    }
}
