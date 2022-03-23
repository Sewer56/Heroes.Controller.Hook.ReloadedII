using System.Diagnostics;
using Reloaded.Mod.Interfaces;

namespace Heroes.Controller.Hook.Custom.Configuration.Implementation;

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
    public IUpdatableConfigurable[] Configurations => null;

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
    public IConfigurable[] GetConfigurations() => null;

    /// <summary>
    /// Allows for custom launcher/configurator implementation.
    /// If you have your own configuration program/code, run that code here and return true, else return false.
    /// </summary>
    public bool TryRunCustomConfiguration()
    {
        Process.Start($"{ModFolder}//sonicheroes.controller.hook.custom.exe");
        return true;
    }
}