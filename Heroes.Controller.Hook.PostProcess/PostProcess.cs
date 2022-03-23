using Heroes.Controller.Hook.Interfaces.Structures.Interfaces;
using Heroes.Controller.Hook.PostProcess.Configuration;

namespace Heroes.Controller.Hook.PostProcess;

public class PostProcess
{
    private const int ControllerCount = 4;

    private Config[] _configurations;
    private Configurator _configurator;

    public PostProcess(string modFolder, string configDirectory)
    {
        _configurator = new Configurator(configDirectory);
        _configurator.Migrate(modFolder, configDirectory);

        _configurations     = new Config[ControllerCount];
        for (int x = 0; x < _configurations.Length; x++)
        {
            _configurations[x] = _configurator.GetConfiguration<Config>(x);

            // Self-updating Controller Bindings 
            var controllerId = x;
            _configurations[x].ConfigurationUpdated += configurable =>
            {
                _configurations[controllerId] = (Config) configurable;
                Program.Logger.WriteLine($"[{Program.MyModId}] Configuration for port {controllerId} updated.");
            };
        }
    }

    /// <summary>
    /// Sends inputs to the Inter Mod Communication's <see cref="Inputs"/> structure.
    /// </summary>
    public void PostProcessInputs(ref IInputs inputs, int port)
    {
        var config = _configurations[port];

        inputs.LeftStickX = config.LeftStickXDeadzone.ApplySettings(inputs.LeftStickX);
        inputs.LeftStickY = config.LeftStickYDeadzone.ApplySettings(inputs.LeftStickY);
        inputs.RightStickX = config.RightStickXDeadzone.ApplySettings(inputs.RightStickX);
        inputs.RightStickY = config.RightStickYDeadzone.ApplySettings(inputs.RightStickY);
        inputs.LeftTriggerPressure = config.LeftTriggerDeadzone.ApplySettings(inputs.LeftTriggerPressure);
        inputs.RightTriggerPressure = config.RightTriggerDeadzone.ApplySettings(inputs.RightTriggerPressure);

        if (config.SwapTriggers)
            (inputs.LeftTriggerPressure, inputs.RightTriggerPressure) = (inputs.RightTriggerPressure, inputs.LeftTriggerPressure);
    }
}