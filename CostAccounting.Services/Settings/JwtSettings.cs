namespace CostAccounting.Services.Settings
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int TokenLifetimeInSeconds { get; set; }
        public int RefreshTokenLifetimeInSeconds { get; set; }
    }
}
