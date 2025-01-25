using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Gameoria.Domain.Enums
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Organizer = "Organizer";
        public const string Customer = "Customer";

        public static readonly IReadOnlyList<string> AllRoles = new[]
        {
            Admin,
            Organizer,
            Customer
        };

        public static class Permissions
        {
            public static class Admin
            {
                public const string ManageUsers = "Permissions.Admin.ManageUsers";
                public const string ManageRoles = "Permissions.Admin.ManageRoles";
                public const string ManageStores = "Permissions.Admin.ManageStores";
                public const string ViewReports = "Permissions.Admin.ViewReports";
                public const string ManageSettings = "Permissions.Admin.ManageSettings";
            }

            public static class Organizer
            {
                public const string ManageOwnStore = "Permissions.Organizer.ManageOwnStore";
                public const string ManageProducts = "Permissions.Organizer.ManageProducts";
                public const string ManageCodes = "Permissions.Organizer.ManageCodes";
                public const string ViewStoreReports = "Permissions.Organizer.ViewStoreReports";
                public const string RespondToReviews = "Permissions.Organizer.RespondToReviews";
            }

            public static class Customer
            {
                public const string PlaceOrders = "Permissions.Customer.PlaceOrders";
                public const string ViewOrderHistory = "Permissions.Customer.ViewOrderHistory";
                public const string WriteReviews = "Permissions.Customer.WriteReviews";
                public const string ManageProfile = "Permissions.Customer.ManageProfile";
            }
        }

        public static IEnumerable<string> GetPermissions(string role)
        {
            switch (role)
            {
                case Admin:
                    return typeof(Permissions.Admin)
                        .GetFields()
                        .Select(f => f.GetValue(null)?.ToString() ?? string.Empty);

                case Organizer:
                    return typeof(Permissions.Organizer)
                        .GetFields()
                        .Select(f => f.GetValue(null)?.ToString() ?? string.Empty);

                case Customer:
                    return typeof(Permissions.Customer)
                        .GetFields()
                        .Select(f => f.GetValue(null)?.ToString() ?? string.Empty);

                default:
                    return Enumerable.Empty<string>();
            }
        }
    }
}