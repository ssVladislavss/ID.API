using ID.Data.Configurations;
using ServiceExtender.Application;
using ServiceExtender.Application.Configuration;

namespace ID.Host.Infrastracture
{
    public class AppSettings
    {
        private static ServerMode mode = ServerMode.DevVersion;
        private static ServiceAddresses? serviceAddresses;

        public static ServerMode Mode => mode;
        public static ServiceAddresses? ServiceAddresses => serviceAddresses;

        public AppSettings(IConfiguration configuration)
        {
            mode = configuration["mode"] switch
            {
                "dev" => ServerMode.DevVersion,
                "test" => ServerMode.DevVersion,
                "real" => ServerMode.DevVersion,
                _ => throw new ArgumentException(),
            };

            var addresses = configuration.GetSection("addresses").Get<ConfigurationAddresses>() ?? new ConfigurationAddresses();

            if(addresses.Dev != null)
            {
                serviceAddresses = mode switch
                {
                    ServerMode.DevVersion => new ServiceAddresses().Initialize(addresses.Dev),
                    ServerMode.TestVersion => new ServiceAddresses().Initialize(addresses.Test),
                    ServerMode.RealVersion => new ServiceAddresses().Initialize(addresses.Real),
                    _ => throw new NotImplementedException()
                };
            }

            DbConnections.Npgsql = mode switch
            {
                ServerMode.DevVersion => configuration.GetSection("dbAddresses")["dev"],
                ServerMode.TestVersion => configuration.GetSection("dbAddresses")["test"],
                ServerMode.RealVersion => configuration.GetSection("dbAddresses")["real"],
                _ => throw new NotImplementedException(),
            };
        }
    }
}
