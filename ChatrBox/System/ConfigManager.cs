using System.Text.Json;

namespace ChatrBox.System
{
    public static class ConfigManager
    {
        private static bool _isInitialized = false;

        /// <summary>
        /// Stores the currently loaded configuration profile.
        /// </summary>
        private static Configuration _profile = new Configuration()
        {
            MessageUpdateRate = 30000,               //default message update rate in milliseconds
            StatusUpdateRate = 90000,                //default status update rate in milliseconds
            ActivityTimeOut = 120                    //default activity timeout in seconds
        };

        /// <summary>
        /// Get: retrieves the message update rate from loaded configuration profile (self-loading)
        /// Set: sets the message update rate of the current profile and saves it to the config file
        /// </summary>
        public static double MessageUpdateRate
        {
            get
            {
                if (!_isInitialized)
                    LoadConfig();

                return _profile.MessageUpdateRate;
            }
            set
            {
                _profile.MessageUpdateRate = value;
                SaveConfig();
            }
        }

        /// <summary>
        /// Get: retrieves the status update rate from loaded configuration profile (self-loading)
        /// Set: sets the status update rate of the current profile and saves it to the config file
        /// </summary>
        public static double StatusUpdateRate
        {
            get
            {
                if (!_isInitialized)
                    LoadConfig();

                return _profile.StatusUpdateRate;
            }
            set
            {
                _profile.StatusUpdateRate = value;
                SaveConfig();
            }
        }

        /// <summary>
        /// Get: retrieves the activity timeout setting from loaded configuration profile (self-loading)
        /// Set: sets the activity timeout setting of the current profile and saves it to the config file
        /// </summary>
        public static int ActivityTimeOut
        {
            get
            {
                if (!_isInitialized)
                    LoadConfig();

                return _profile.ActivityTimeOut;
            }
            set
            {
                _profile.ActivityTimeOut = value;
                SaveConfig();
            }
        }

        private static void LoadConfig()
        {
            if (File.Exists("config.json"))
            {
                using (var file = new FileStream("config.json", FileMode.Open))
                {
                    _profile = JsonSerializer.Deserialize<Configuration>(file) ?? throw new ArgumentNullException();
                }
            }
            else
            {
                SaveConfig();
            }

            _isInitialized = true;
        }

        private static void SaveConfig()
        {
            File.WriteAllText("config.json", JsonSerializer.Serialize(_profile));
        }
    }
}
