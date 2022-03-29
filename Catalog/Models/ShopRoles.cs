namespace Catalog.Models
{
    public static class ShopRoles
    {
        public const string Admin = "Admin";
        public const string Marketing = "Marketing";
        public const string AdminMarketing = $"{Admin},{Marketing}";
    }
}
