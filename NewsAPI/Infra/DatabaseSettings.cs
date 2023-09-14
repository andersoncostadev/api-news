using NewsAPI.Infra.Interfaces;

namespace NewsAPI.Infra
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}