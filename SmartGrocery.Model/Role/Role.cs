using SmartGrocery.Model.Common;
using System.Collections.Generic;

namespace SmartGrocery.Model.Role
{
    public class Role : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}