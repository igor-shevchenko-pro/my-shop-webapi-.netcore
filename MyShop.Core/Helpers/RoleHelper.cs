using MyShop.Core.Entities.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Core.Helpers
{
    public class RoleHelper
    {
        private static readonly RoleHelper _instance = new RoleHelper();
        public static RoleHelper Current => _instance;

        private RoleHelper()
        {
            SuperAdmin = new Role() { Id = "28a569fe-22f7-49c4-a22b-a909197a46f5", Title = "Super-Admin" };
            Admin = new Role() { Id = "89a586fe-2d77-49e4-122b-a9er197456f5", Title = "Admin" };
            MainModerator = new Role() { Id = "d7a58qfe-2df6-47e4-122b-a9er1956e6f5", Title = "Super-Moderator" };
            Moderator = new Role() { Id = "d7a5were-dff6-47e4-f22b-89er1956e6f5", Title = "Moderator" };
            Customer = new Role() { Id = "c9789f6e-r9f7-4254-a22b-a90rtyj646f5", Title = "Customer" };
            User = new Role() { Id = "18a569fe-12f7-49c4-a22b-a909197a46f5", Title = "User" };

            Roles = new List<Role>()
            {
                SuperAdmin,
                Admin,
                MainModerator,
                Moderator,
                Customer,
                User,
            };
        }

        public List<Role> Roles { get; private set; }

        public Role SuperAdmin { get; private set; }
        public Role Admin { get; private set; }
        public Role MainModerator { get; private set; }
        public Role Moderator { get; private set; }
        public Role Customer { get; private set; }
        public Role User { get; private set; }

        public string GetName(string roleId)
        {
            var roleName = Roles.FirstOrDefault(x => x.Id == roleId).Title;
            return roleName ?? throw new ArgumentException($"Role {roleId} is not found");
        }

        public Role GetRole(string id)
        {
            return Roles.FirstOrDefault(x => x.Id == id);
        }
    }
}
