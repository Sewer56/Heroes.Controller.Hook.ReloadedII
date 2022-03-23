using Heroes.Controller.Hook.Custom.Enum;
using Reloaded.Input.Configurator;
using Reloaded.Input.Structs;

namespace Heroes.Controller.Hook.Custom;

public class Mappings
{
    public static string[] ControllerFiles =
    {
        "Controller1.json",
        "Controller2.json",
        "Controller3.json",
        "Controller4.json",
    };

    public static MappingEntry[] Entries = 
    {
        new("Jump / Accept", (int) MappingEntries.Jump, MappingType.Button),
        new("Action / Decline", (int) MappingEntries.Action, MappingType.Button),
        new("Camera Left", (int) MappingEntries.CameraL, MappingType.Button),
        new("Camera Right", (int) MappingEntries.CameraR, MappingType.Button),
        new("Formation Left", (int) MappingEntries.FormationL, MappingType.Button),
        new("Formation Right", (int) MappingEntries.FormationR, MappingType.Button),

        new("DPad Up", (int) MappingEntries.DPadUp, MappingType.Button),
        new("DPad Down", (int) MappingEntries.DPadDown, MappingType.Button),
        new("DPad Left", (int) MappingEntries.DPadLeft, MappingType.Button),
        new("DPad Right", (int) MappingEntries.DPadRight, MappingType.Button),

        new("Start", (int) MappingEntries.Start, MappingType.Button),
        new("Team Blast", (int) MappingEntries.TeamBlast, MappingType.Button),

        // Button Axis
        new("Up (Button)", (int) MappingEntries.Up, MappingType.Button),
        new("Down (Button)", (int) MappingEntries.Down, MappingType.Button),
        new("Left (Button)", (int) MappingEntries.Left, MappingType.Button),
        new("Right (Button)", (int) MappingEntries.Right, MappingType.Button),

        new("R. Stick Up (Button)", (int) MappingEntries.RSUp, MappingType.Button),
        new("R. Stick Down (Button)", (int) MappingEntries.RSDown, MappingType.Button),
        new("R. Stick Left (Button)", (int) MappingEntries.RSLeft, MappingType.Button),
        new("R. Stick Right (Button)", (int) MappingEntries.RSRight, MappingType.Button),

        new("Left Trigger (Button)", (int) MappingEntries.LeftTrigger, MappingType.Button),
        new("Right Trigger (Button)", (int) MappingEntries.RightTrigger, MappingType.Button),

        // Axis
        new("Analog Stick X", (int) MappingEntries.LeftStickX, MappingType.Axis),
        new("Analog Stick Y", (int) MappingEntries.LeftStickY, MappingType.Axis),
        new("R. Analog Stick X", (int) MappingEntries.RightStickX, MappingType.Axis),
        new("R. Analog Stick Y", (int) MappingEntries.RightStickY, MappingType.Axis),
        new("Left Trigger", (int) MappingEntries.LeftTriggerPressure, MappingType.Axis),
        new("Right Trigger", (int) MappingEntries.RightTriggerPressure, MappingType.Axis),
    };
}