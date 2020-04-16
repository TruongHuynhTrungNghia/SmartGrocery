using SmartGrocery.Model.Common;

namespace SmartGrocery.Model.Role
{
    public class User : Entity
    {
        public string Email { get; set; }

        public string EmailConfirmed { get; set; }

        public string PasseordHash { get; set; }

        public string PhoneNumber { get; set; }

        public string UserName { get; set; }
    }
}