using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleProxyWebClient
{
    internal class ConfigWrapper
    {
        private static volatile ConfigWrapper _instance;
        private static object _syncRoot = new Object();

        private ConfigWrapper() { }

        public static ConfigWrapper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new ConfigWrapper();
                    }
                }
                return _instance;
            }
        }
        
        public string KeyGoogleMaps
        {
            get
            {
                var key = ConfigurationManager.AppSettings["googlemap-key"];
                if (key == null || string.IsNullOrWhiteSpace(key))
                    return string.Empty;
                else
                    return key;
            }
        }

        public string ProxyUrl
        {
            get
            {
                var key = ConfigurationManager.AppSettings["proxy-url"];
                if (key == null || string.IsNullOrWhiteSpace(key))
                    return string.Empty;
                else
                    return key;
            }
        }

    }
}
