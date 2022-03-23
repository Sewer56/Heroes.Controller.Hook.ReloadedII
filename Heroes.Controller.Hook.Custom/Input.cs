using System;
using System.Collections.ObjectModel;
using System.IO;
using Heroes.Controller.Hook.Custom.Enum;
using Heroes.Controller.Hook.Interfaces.Definitions;
using Heroes.Controller.Hook.Interfaces.Structures.Interfaces;
using Reloaded.Input;
using Reloaded.Input.Configurator;
using Reloaded.Input.Configurator.WPF;
using Reloaded.Input.Structs;

namespace Heroes.Controller.Hook.Custom;

public class Input
{
    private string _modFolder;
    private string _configFolder;
    private VirtualController[] _controllers;

    public Input(string modFolder, string modConfigDirectory)
    {
        _modFolder = modFolder;
        _configFolder = modConfigDirectory;
        _controllers = new VirtualController[Mappings.ControllerFiles.Length];

        // Migrate configurations (from old loader setup)
        foreach (var controllerFile in Mappings.ControllerFiles)
        {
            var oldPath = Path.Combine(_modFolder, controllerFile);
            var newPath = Path.Combine(_configFolder, controllerFile);
            try { File.Move(oldPath, newPath, true); }
            catch (Exception) { /* ignored */ }
        }

        // Initialise controllers
        for (int x = 0; x < Mappings.ControllerFiles.Length; x++)
            _controllers[x] = new VirtualController(Path.Combine(_configFolder, Mappings.ControllerFiles[x]));
    }

    public void SetInputs(ref IInputs inputs, in int port)
    {
        if (port >= 0 && port < _controllers.Length)
        {
            var controller = _controllers[port];

            // Buttons 
            if (controller.GetButton((int)MappingEntries.Jump)) inputs.ButtonFlags |= ButtonFlags.Jump;
            if (controller.GetButton((int)MappingEntries.Action)) inputs.ButtonFlags |= ButtonFlags.Action;
            if (controller.GetButton((int)MappingEntries.FormationR)) inputs.ButtonFlags |= ButtonFlags.FormationR;
            if (controller.GetButton((int)MappingEntries.FormationL)) inputs.ButtonFlags |= ButtonFlags.FormationL;
            if (controller.GetButton((int)MappingEntries.DPadUp)) inputs.ButtonFlags |= ButtonFlags.DpadUp;
            if (controller.GetButton((int)MappingEntries.DPadDown)) inputs.ButtonFlags |= ButtonFlags.DpadDown;
            if (controller.GetButton((int)MappingEntries.DPadLeft)) inputs.ButtonFlags |= ButtonFlags.DpadLeft;
            if (controller.GetButton((int)MappingEntries.DPadRight)) inputs.ButtonFlags |= ButtonFlags.DpadRight;
            if (controller.GetButton((int)MappingEntries.CameraR)) inputs.ButtonFlags |= ButtonFlags.CameraR;
            if (controller.GetButton((int)MappingEntries.CameraL)) inputs.ButtonFlags |= ButtonFlags.CameraL;
            if (controller.GetButton((int)MappingEntries.Start)) inputs.ButtonFlags |= ButtonFlags.Start;
            if (controller.GetButton((int)MappingEntries.TeamBlast)) inputs.ButtonFlags |= ButtonFlags.TeamBlast;

            // Axis
                
            var leftStickX = controller.GetAxis((int)MappingEntries.LeftStickX);
            var leftStickY = controller.GetAxis((int)MappingEntries.LeftStickY);
            var rightStickX = controller.GetAxis((int)MappingEntries.RightStickX);
            var rightStickY = controller.GetAxis((int)MappingEntries.RightStickY);
            var leftBumper = controller.GetAxis((int)MappingEntries.LeftTriggerPressure);
            var rightBumper = controller.GetAxis((int)MappingEntries.RightTriggerPressure);

            inputs.LeftStickX = AnalogToHeroesRange(leftStickX);
            inputs.LeftStickY = AnalogToHeroesRange(-leftStickY);
            inputs.RightStickX = AnalogToHeroesRange(rightStickX);
            inputs.RightStickY = AnalogToHeroesRange(-rightStickY);

            if (controller.GetMapping((int) MappingEntries.LeftTriggerPressure) != null)
                inputs.LeftTriggerPressure = TriggerToHeroesRange(leftBumper);

            if (controller.GetMapping((int) MappingEntries.RightTriggerPressure) != null)
                inputs.RightTriggerPressure = TriggerToHeroesRange(rightBumper);
                
            // Button to Axis
            if (controller.GetButton((int)MappingEntries.Up)) inputs.LeftStickY = -1.0f;
            if (controller.GetButton((int)MappingEntries.Down)) inputs.LeftStickY = 1.0f;
            if (controller.GetButton((int)MappingEntries.Left)) inputs.LeftStickX = -1.0f;
            if (controller.GetButton((int)MappingEntries.Right)) inputs.LeftStickX = 1.0f;
            if (controller.GetButton((int)MappingEntries.RSUp)) inputs.RightStickY = -1.0f;
            if (controller.GetButton((int)MappingEntries.RSDown)) inputs.RightStickY = 1.0f;
            if (controller.GetButton((int)MappingEntries.RSLeft)) inputs.RightStickX = -1.0f;
            if (controller.GetButton((int)MappingEntries.RSRight)) inputs.RightStickX = 1.0f;

            if (controller.GetButton((int)MappingEntries.LeftTrigger)) inputs.LeftTriggerPressure = 255;
            if (controller.GetButton((int)MappingEntries.RightTrigger)) inputs.RightTriggerPressure = 255;
        }
    }

    public void Configure()
    {
        // Make a new WPF Application
        var configurator = new Configurator();

        // Make the configurator inputs.
        var input = new ConfiguratorInput[Mappings.ControllerFiles.Length];
        for (int x = 0; x < input.Length; x++)
            input[x] = new ConfiguratorInput($"Controller {x}", Path.Combine(_configFolder, Mappings.ControllerFiles[x]), new ObservableCollection<MappingEntry>(Mappings.Entries));

        // Run the main window.
        configurator.Run(new ConfiguratorWindow(input));
    }

    private float AnalogToHeroesRange(float analogOriginal)
    {
        return analogOriginal / 10000.0f;
    }

    private byte TriggerToHeroesRange(float analogOriginal)
    {
        return (byte)((((analogOriginal + AxisSet.MaxValue) / 2) / AxisSet.MaxValue) * byte.MaxValue);
    }
}