using Microsoft.Extensions.Configuration;
using System;

namespace Infrastructure.ThirdpartyService.IndentityServer
{
    public sealed class IdentityServerConfiguration
    {
        private static IdentityServerConfiguration _instance = null;

        public string IS4URI;
        public string CLIENT_ID;
        public string CLIENT_SECRET;

        private const string IS4_URI = "IDENTITYSERVER:IS4_URI";
        private const string DEFAULT_CLIENT_ID = "IDENTITYSERVER:CLIENT_ID";
        private const string DEFAULT_CLIENT_SECRET = "IDENTITYSERVER:CLIENT_SECRET";

        public static IdentityServerConfiguration Instance
        {
            get
            {
                if (_instance == null)
                    throw new Exception("IS4 Settings not initialized. Load settings before usage.");

                return _instance;
            }
        }

        private IdentityServerConfiguration(IConfiguration configuration)
        {
            InitIdentityServerConfiguration(configuration);
        }

        private void InitIdentityServerConfiguration(IConfiguration configuration)
        {
            IS4URI = configuration[IS4_URI] ?? IS4_URI;
            CLIENT_ID = configuration[DEFAULT_CLIENT_ID] ?? DEFAULT_CLIENT_ID;
            CLIENT_SECRET = configuration[DEFAULT_CLIENT_SECRET] ?? DEFAULT_CLIENT_SECRET;
        }

        public static void LoadIS4Settings(IConfiguration configuration)
        {
            _instance = new IdentityServerConfiguration(configuration);
        }
    }
}
