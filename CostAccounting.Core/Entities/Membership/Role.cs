namespace CostAccounting.Core.Entities.Membership
{
    public class Role : Entity<int>
    {
        public const int NameLength = 32;

        public string Name { get; set; }

        private Role()
        {
        }
    }
}
