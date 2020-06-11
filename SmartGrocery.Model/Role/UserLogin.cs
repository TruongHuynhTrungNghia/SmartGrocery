using SmartGrocery.Model.Common;
using System;

namespace SmartGrocery.Model.Role
{
    public class UserLogin : Entity
    {
        public string ProviderKey { get; set; }

        public Guid UserId { get; set; }

        public string LoginProvider { get; set; }

        public virtual User User { get; set; }
    }
}