namespace Application.Models.Redis;

public class RedisCacheSettings
{
    public bool IsEnabled { get; set; }
    public string ConnectionString { get; set; }
}
