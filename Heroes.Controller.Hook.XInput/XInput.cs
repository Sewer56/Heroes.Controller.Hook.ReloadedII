﻿using System.Collections.Generic;
using Heroes.Controller.Hook.Interfaces.Structures.Interfaces;
using Heroes.Controller.Hook.XInput.Configuration;
using SharpDX.XInput;

namespace Heroes.Controller.Hook.XInput;

public class XInput
{
    private const int XInputControllerLimit = 4;
    private Dictionary<int, ControllerConfigTuple> _controllers = new Dictionary<int, ControllerConfigTuple>();

    public XInput(string modFolder, string configDirectory)
    {
        var configurator = new Configurator(configDirectory);
        configurator.Migrate(modFolder, configDirectory);

        for (int controllerNo = 0; controllerNo < XInputControllerLimit; controllerNo++)
        {
            var config = configurator.GetConfiguration<Config>(controllerNo);
            if (config.ControllerPort == -1)
                config.ControllerPort = controllerNo;

            // Self-updating Controller Bindings 
            var controllerId = controllerNo;
            config.ConfigurationUpdated += configurable =>
            {
                _controllers[controllerId].Config = (Config) configurable;
                Program.Logger.WriteLine($"[{Program.MyModId}] Configuration for port {controllerId} updated.");
            }; 

            _controllers[config.ControllerPort] = new ControllerConfigTuple(new SharpDX.XInput.Controller((UserIndex) controllerNo), config);
        }
    }

    /// <summary>
    /// Sends inputs to the Inter Mod Communication's "Inputs" structure.
    /// </summary>
    public void SendInputs(ref IInputs inputs, int port)
    {
        if (!_controllers.TryGetValue(port, out var controllerTuple)) 
            return;

        var controller = controllerTuple.Controller;
        var config     = controllerTuple.Config;

        if (!controller.IsConnected)
            return;

        controller.GetState(out State state);
        Converter.ToHeroesController(ref state, ref inputs, config);
    }
}