using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Heroes.Controller.Hook.Shared.Configuration;
using SharpDX.XInput;

namespace Heroes.Controller.Hook.XInput
{
    public class Config : Configurable
    {
        public int                ControllerPort { get; set; } = -1; 

        public GamepadButtonFlags Jump          { get; set; } = GamepadButtonFlags.A;
        public GamepadButtonFlags FormationR    { get; set; } = GamepadButtonFlags.B;
        public GamepadButtonFlags Action        { get; set; } = GamepadButtonFlags.X;
        public GamepadButtonFlags FormationL    { get; set; } = GamepadButtonFlags.Y;

        public GamepadButtonFlags DpadUp    { get; set; } = GamepadButtonFlags.DPadUp;
        public GamepadButtonFlags DpadDown  { get; set; } = GamepadButtonFlags.DPadDown;
        public GamepadButtonFlags DpadLeft  { get; set; } = GamepadButtonFlags.DPadLeft;
        public GamepadButtonFlags DpadRight { get; set; } = GamepadButtonFlags.DPadRight;

        public GamepadButtonFlags CameraR   { get; set; } = GamepadButtonFlags.RightShoulder;
        public GamepadButtonFlags CameraL   { get; set; } = GamepadButtonFlags.LeftShoulder;

        public GamepadButtonFlags Start     { get; set; } = GamepadButtonFlags.Start;
        public GamepadButtonFlags TeamBlast { get; set; } = GamepadButtonFlags.Back;
    }
}
