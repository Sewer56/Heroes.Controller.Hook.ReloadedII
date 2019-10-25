using System.IO;
using Heroes.Controller.Hook.Shared.Configuration;
using Reloaded.Mod.Interfaces;

namespace Heroes.Controller.Hook.XInput.Configuration
{
    public class Configurator : IConfigurator
    {
        /* For latest documentation:
            - See the interface! (Go To Definition) or if not available
            - Google the Source Code!
        */

        /// <summary>
        /// Full path to the mod folder.
        /// </summary>
        public string ModFolder { get; private set; }

        /// <summary>
        /// Returns a list of configurations.
        /// </summary>
        public IConfigurable[] Configurations => _configurations ?? (_configurations = new IConfigurable[]  {
                
            // Add more configurations here if needed.
            Configurable.Load<Config>(Path.Combine(ModFolder, "Controller0.json"), "Controller Port 0"),
            Configurable.Load<Config>(Path.Combine(ModFolder, "Controller1.json"), "Controller Port 1"),
            Configurable.Load<Config>(Path.Combine(ModFolder, "Controller2.json"), "Controller Port 2"),
            Configurable.Load<Config>(Path.Combine(ModFolder, "Controller3.json"), "Controller Port 3")
        });

        private IConfigurable[] _configurations;

        public Configurator() { }
        public Configurator(string modDirectory) : this() => ModFolder = modDirectory;

        /* Configurator */

        /// <summary>
        /// Gets an individual user configuration.
        /// </summary>
        public TType GetConfiguration<TType>(int index) => (TType)Configurations[index];

        /* IConfigurator. */

        /// <summary>
        /// Sets the mod directory for the Configurator.
        /// </summary>
        public void SetModDirectory(string modDirectory) => ModFolder = modDirectory;

        /// <summary>
        /// Returns a list of user configurations.
        /// </summary>
        public IConfigurable[] GetConfigurations() => Configurations;

        /// <summary>
        /// Allows for custom launcher/configurator implementation.
        /// If you have your own configuration program/code, run that code here and return true, else return false.
        /// </summary>
        public bool TryRunCustomConfiguration() => false;
    }
}
