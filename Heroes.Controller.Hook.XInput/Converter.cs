using System.Runtime.CompilerServices;
using Heroes.Controller.Hook.Interfaces.Definitions;
using Heroes.Controller.Hook.Interfaces.Structures.Interfaces;
using SharpDX.XInput;

namespace Heroes.Controller.Hook.XInput;

/// <summary>
/// Converts XInput to Heroes Controllers
/// </summary>
public static class Converter
{
    public static void ToHeroesController(ref State state, ref IInputs inputs, Config configuration)
    {
        // Buttons
        if (XInputButtonPressed(ref state, configuration.Jump))         inputs.ButtonFlags |= ButtonFlags.Jump;
        if (XInputButtonPressed(ref state, configuration.FormationR))   inputs.ButtonFlags |= ButtonFlags.FormationR;
        if (XInputButtonPressed(ref state, configuration.Action))       inputs.ButtonFlags |= ButtonFlags.Action;
        if (XInputButtonPressed(ref state, configuration.FormationL))   inputs.ButtonFlags |= ButtonFlags.FormationL;

        if (XInputButtonPressed(ref state, configuration.CameraL))   inputs.ButtonFlags |= ButtonFlags.CameraL;
        if (XInputButtonPressed(ref state, configuration.CameraR))   inputs.ButtonFlags |= ButtonFlags.CameraR;
        if (XInputButtonPressed(ref state, configuration.Start))     inputs.ButtonFlags |= ButtonFlags.Start;
        if (XInputButtonPressed(ref state, configuration.TeamBlast)) inputs.ButtonFlags |= ButtonFlags.TeamBlast;

        if (XInputButtonPressed(ref state, configuration.DpadUp))    inputs.ButtonFlags |= ButtonFlags.DpadUp;
        if (XInputButtonPressed(ref state, configuration.DpadDown))  inputs.ButtonFlags |= ButtonFlags.DpadDown;
        if (XInputButtonPressed(ref state, configuration.DpadLeft))  inputs.ButtonFlags |= ButtonFlags.DpadLeft;
        if (XInputButtonPressed(ref state, configuration.DpadRight)) inputs.ButtonFlags |= ButtonFlags.DpadRight;

        // Triggers.
        inputs.LeftStickX    = XInputRangeToHeroesRange(state.Gamepad.LeftThumbX);
        inputs.LeftStickY    = XInputRangeToHeroesRange(state.Gamepad.LeftThumbY * -1);

        inputs.RightStickX   = XInputRangeToHeroesRange(state.Gamepad.RightThumbX);
        inputs.RightStickY   = XInputRangeToHeroesRange(state.Gamepad.RightThumbY * -1);

        inputs.LeftTriggerPressure = state.Gamepad.LeftTrigger;
        inputs.RightTriggerPressure = state.Gamepad.RightTrigger;
    }

    /* XInput Utility Functions */

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool XInputButtonPressed(ref State state, GamepadButtonFlags flag)
    {
        return (state.Gamepad.Buttons & flag) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static float XInputRangeToHeroesRange(float value)
    {
        return value / short.MaxValue;
    }
}