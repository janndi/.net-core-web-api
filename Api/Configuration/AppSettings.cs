using Microsoft.Extensions.Configuration;
using System;

namespace Api.Configuration
{
    public class AppSettings
    {
        private static AppSettings _instance = null;

        public string ConnectionString;

        public static AppSettings Instance
        {
            get
            {
                if (_instance == null)
                    throw new Exception("Settings not initialized. Load settings before usage.");

                return _instance;
            }
        }

        private AppSettings(IConfiguration configuration)
        {
            InitAppSettings(configuration);
        }

        private void InitAppSettings(IConfiguration configuration)
        {
            ConnectionString = configuration[Constants.Connections.KEY_CONNECTION_NAME] ?? Constants.Connections.DEFAULT_CONNECTION_STRING;
        }

        public static void LoadSettings(IConfiguration configuration)
        {
            _instance = new AppSettings(configuration);
        }
    }
}
