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
        new("Jump / Accept", (int) MappingEntries.Jump, MappingType.Button, "Makes the character jump."),
        new("Action / Decline", (int) MappingEntries.Action, MappingType.Button, "Makes the character attack/kick."),
        new("Camera Left", (int) MappingEntries.CameraL, MappingType.Button, "Moves the camera left."),
        new("Camera Right", (int) MappingEntries.CameraR, MappingType.Button, "Moves the camera right."),
        new("Formation Left", (int) MappingEntries.FormationL, MappingType.Button, "Switches the formation circle left."),
        new("Formation Right", (int) MappingEntries.FormationR, MappingType.Button, "Switches the formation circle right."),

        new("DPad Up", (int) MappingEntries.DPadUp, MappingType.Button),
        new("DPad Down", (int) MappingEntries.DPadDown, MappingType.Button),
        new("DPad Left", (int) MappingEntries.DPadLeft, MappingType.Button),
        new("DPad Right", (int) MappingEntries.DPadRight, MappingType.Button),

        new("Start", (int) MappingEntries.Start, MappingType.Button),
        new("Team Blast", (int) MappingEntries.TeamBlast, MappingType.Button),

        // Button Axis
        new("Up (Button)", (int) MappingEntries.Up, MappingType.Button, "Button that emulates pressing analog stick up. (useful for keyboard)"),
        new("Down (Button)", (int) MappingEntries.Down, MappingType.Button, "Button that emulates pressing analog stick down. (useful for keyboard)"),
        new("Left (Button)", (int) MappingEntries.Left, MappingType.Button, "Button that emulates pressing analog stick left. (useful for keyboard)"),
        new("Right (Button)", (int) MappingEntries.Right, MappingType.Button, "Button that emulates pressing analog stick right. (useful for keyboard)"),

        new("R. Stick Up (Button)", (int) MappingEntries.RSUp, MappingType.Button),
        new("R. Stick Down (Button)", (int) MappingEntries.RSDown, MappingType.Button),
        new("R. Stick Left (Button)", (int) MappingEntries.RSLeft, MappingType.Button),
        new("R. Stick Right (Button)", (int) MappingEntries.RSRight, MappingType.Button),

        new("Left Trigger (Button)", (int) MappingEntries.LeftTrigger, MappingType.Button, "Button that emulates a left trigger press"),
        new("Right Trigger (Button)", (int) MappingEntries.RightTrigger, MappingType.Button, "Button that emulates a right trigger press"),

        // Axis
        new("Analog Stick X", (int) MappingEntries.LeftStickX, MappingType.Axis, "Horizontal movement of the left analog stick"),
        new("Analog Stick Y", (int) MappingEntries.LeftStickY, MappingType.Axis, "Vertical movement of the left analog stick"),
        new("R. Analog Stick X", (int) MappingEntries.RightStickX, MappingType.Axis, "Horizontal movement of the right analog stick"),
        new("R. Analog Stick Y", (int) MappingEntries.RightStickY, MappingType.Axis, "Vertical movement of the right analog stick"),
        new("Left Trigger", (int) MappingEntries.LeftTriggerPressure, MappingType.Axis, "Left Trigger on your controller."),
        new("Right Trigger", (int) MappingEntries.RightTriggerPressure, MappingType.Axis, "Right Trigger on your controller."),
    };
}