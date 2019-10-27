using System;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Reloaded.Messaging.Interfaces;
using Reloaded.Messaging.Serializer.SystemTextJson;
using Reloaded.Mod.Interfaces;
using ISerializable = Reloaded.Messaging.Interfaces.Message.ISerializable;

namespace Heroes.Controller.Hook.Shared.Configuration
{
    public class Configurable : IConfigurable, ISerializable
    {
        // Default Serialization Options
        // If you wish to change the serializer used, refer to Reloaded.Messaging documentation: https://github.com/Reloaded-Project/Reloaded.Messaging
        public static JsonSerializerOptions SerializerOptions { get; } = new JsonSerializerOptions()
        {
            Converters = { new JsonStringEnumConverter() },
            WriteIndented = true
        };

        public ISerializer GetSerializer() => new SystemTextJsonSerializer(SerializerOptions);
        public ICompressor GetCompressor() => null;

        /* Class Properties */

        /// <summary>
        /// Full path to the configuration file.
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public string FilePath { get; private set; }

        /// <summary>
        /// The name of the configuration file.
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public string ConfigName { get; private set; }

        public Configurable() { }

        /* Load/Save support. */

        /// <summary>
        /// Saves the configuration to the hard disk.
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public Action Save { get; private set; }

        /// <summary>
        /// Loads a specified configuration from the hard disk, or creates a default if it does not exist.
        /// </summary>
        /// <typeparam name="TType">The type of config to return.</typeparam>
        /// <param name="filePath">The full file path of the config.</param>
        /// <param name="configName">The name of the configuration.</param>
        public static TType Load<TType>(string filePath, string configName) where TType : Configurable, new()
        {
            var config = File.Exists(filePath)
                ? (TType)Serializable.Deserialize<TType>(File.ReadAllBytes(filePath))
                : new TType();

            config.FilePath = filePath;
            config.ConfigName = configName;
            config.Save = () => File.WriteAllBytes(config.FilePath, config.Serialize());
            return config;
        }
    }
}
