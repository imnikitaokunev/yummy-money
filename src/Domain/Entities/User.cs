using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;

namespace Domain.Entities
{
    public class User : Entity<Guid>
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisteredAt { get; set; }
        public byte[] Photo { get; private set; }

        public ICollection<Role> Roles { get; set; } = new List<Role>();

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