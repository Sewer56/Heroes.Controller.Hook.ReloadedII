using Heroes.Controller.Hook.Shared;
using SharpDX.XInput;

namespace Heroes.Controller.Hook.XInput.Json
{
    public class ConfigReadWriter : JsonSerializable<Config>
    {
        /* Instances */
        private string _configFolder;

        public ConfigReadWriter(string configFolder)
        {
            _configFolder = configFolder;
        }

        public string GetFilePath(int port)         => $"{_configFolder}\\Controller{port}.json";
        public Config FromJson(int port)            => FromPath(GetFilePath(port));
        public void ToJson(Config config, int port) => ToPath(config, GetFilePath(port));
    }
}
