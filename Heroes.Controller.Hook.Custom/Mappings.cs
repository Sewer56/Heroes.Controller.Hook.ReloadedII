using Heroes.Controller.Hook.Custom.Enum;
using Reloaded.Input.Configurator;
using Reloaded.Input.Structs;

namespace Heroes.Controller.Hook.Custom
{
    public class Mappings
    {
        public static string[] ControllerFiles =
        {
            "Controller1.json",
            "Controller2.json",
            "Controller3.json",
            "Controller4.json",
        };

        public static MappingEntry[] Entries = new MappingEntry[]
        {
            new MappingEntry("Jump / Accept", (int) MappingEntries.Jump, MappingType.Button),
            new MappingEntry("Action / Decline", (int) MappingEntries.Action, MappingType.Button),
            new MappingEntry("Camera Left", (int) MappingEntries.CameraL, MappingType.Button),
            new MappingEntry("Camera Right", (int) MappingEntries.CameraR, MappingType.Button),
            new MappingEntry("Formation Left", (int) MappingEntries.FormationL, MappingType.Button),
            new MappingEntry("Formation Right", (int) MappingEntries.FormationR, MappingType.Button),

            new MappingEntry("DPad Up", (int) MappingEntries.DPadUp, MappingType.Button),
            new MappingEntry("DPad Down", (int) MappingEntries.DPadDown, MappingType.Button),
            new MappingEntry("DPad Left", (int) MappingEntries.DPadLeft, MappingType.Button),
            new MappingEntry("DPad Right", (int) MappingEntries.DPadRight, MappingType.Button),

            new MappingEntry("Start", (int) MappingEntries.Start, MappingType.Button),
            new MappingEntry("Team Blast", (int) MappingEntries.TeamBlast, MappingType.Button),

            // Button Axis
            new MappingEntry("Up (Button)", (int) MappingEntries.Up, MappingType.Button),
            new MappingEntry("Down (Button)", (int) MappingEntries.Down, MappingType.Button),
            new MappingEntry("Left (Button)", (int) MappingEntries.Left, MappingType.Button),
            new MappingEntry("Right (Button)", (int) MappingEntries.Right, MappingType.Button),

            new MappingEntry("R. Stick Up (Button)", (int) MappingEntries.RSUp, MappingType.Button),
            new MappingEntry("R. Stick Down (Button)", (int) MappingEntries.RSDown, MappingType.Button),
            new MappingEntry("R. Stick Left (Button)", (int) MappingEntries.RSLeft, MappingType.Button),
            new MappingEntry("R. Stick Right (Button)", (int) MappingEntries.RSRight, MappingType.Button),

            new MappingEntry("Left Trigger (Button)", (int) MappingEntries.LeftTrigger, MappingType.Button),
            new MappingEntry("Right Trigger (Button)", (int) MappingEntries.RightTrigger, MappingType.Button),

            // Axis
            new MappingEntry("Analog Stick X", (int) MappingEntries.LeftStickX, MappingType.Axis),
            new MappingEntry("Analog Stick Y", (int) MappingEntries.LeftStickY, MappingType.Axis),
            new MappingEntry("R. Analog Stick X", (int) MappingEntries.RightStickX, MappingType.Axis),
            new MappingEntry("R. Analog Stick Y", (int) MappingEntries.RightStickY, MappingType.Axis),
            new MappingEntry("Left Trigger", (int) MappingEntries.LeftTriggerPressure, MappingType.Axis),
            new MappingEntry("Right Trigger", (int) MappingEntries.RightTriggerPressure, MappingType.Axis),
        };
    }
}
