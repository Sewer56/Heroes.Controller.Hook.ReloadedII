using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Heroes.Controller.Hook.Shared;
using Heroes.Controller.Hook.XInput.Json;
using SharpDX.XInput;

namespace Heroes.Controller.Hook.XInput.Help
{
    public class Buttons : JsonSerializable<Buttons>
    {
        public GamepadButtonFlags XboxButtonA { get; set; } = GamepadButtonFlags.A;
        public GamepadButtonFlags XboxButtonB { get; set; } = GamepadButtonFlags.B;
        public GamepadButtonFlags XboxButtonX { get; set; } = GamepadButtonFlags.X;
        public GamepadButtonFlags XboxButtonY { get; set; } = GamepadButtonFlags.Y;

        public GamepadButtonFlags XboxDpadUp    { get; set; } = GamepadButtonFlags.DPadUp;
        public GamepadButtonFlags XboxDpadDown  { get; set; } = GamepadButtonFlags.DPadDown;
        public GamepadButtonFlags XboxDpadLeft  { get; set; } = GamepadButtonFlags.DPadLeft;
        public GamepadButtonFlags XboxDpadRight { get; set; } = GamepadButtonFlags.DPadRight;

        public GamepadButtonFlags XboxRightShoulder { get; set; } = GamepadButtonFlags.RightShoulder;
        public GamepadButtonFlags XboxLeftShoulder  { get; set; } = GamepadButtonFlags.LeftShoulder;

        public GamepadButtonFlags XboxStart           { get; set; } = GamepadButtonFlags.Start;
        public GamepadButtonFlags XboxBack            { get; set; } = GamepadButtonFlags.Back;

        public GamepadButtonFlags XboxLeftThumbstick  { get; set; } = GamepadButtonFlags.LeftThumb;
        public GamepadButtonFlags XboxRightThumbstick { get; set; } = GamepadButtonFlags.RightThumb;

        public GamepadButtonFlags XboxNullButton      { get; set; } = GamepadButtonFlags.None;

        public static void SaveIfNotExist(string modFolder)
        {
            string path = $"{modFolder}\\Buttons.json";
            if (! File.Exists(path))
                ToPath(new Buttons(), path);
        }
    }
}
