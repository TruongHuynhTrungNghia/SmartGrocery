using SmartGrocery.Model.Common;
using System.Collections.Generic;

namespace SmartGrocery.Model.Role
{
    public class Permission : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}