using System.ComponentModel;
using System.Text.Json.Serialization;
using Heroes.Controller.Hook.Shared.Configuration;
using SharpDX.XInput;

namespace Heroes.Controller.Hook.XInput;

public class Config : Configurable<Config>
{
    [Browsable(false)]
    [JsonIgnore]
    public int                ControllerPort { get; set; } = -1;

    [DisplayName("Accept / Jump")]
    [DefaultValue(GamepadButtonFlags.A)]
    public GamepadButtonFlags Jump          { get; set; } = GamepadButtonFlags.A;

    [DisplayName("Action")]
    [DefaultValue(GamepadButtonFlags.X)]
    public GamepadButtonFlags Action        { get; set; } = GamepadButtonFlags.X;

    [DisplayName("Omochao / Formation Left")]
    [DefaultValue(GamepadButtonFlags.Y)]
    public GamepadButtonFlags FormationL    { get; set; } = GamepadButtonFlags.Y;

    [DisplayName("Decline / Formation Right")]
    [DefaultValue(GamepadButtonFlags.B)]
    public GamepadButtonFlags FormationR { get; set; } = GamepadButtonFlags.B;

    [DisplayName("DPad: Up")]
    [DefaultValue(GamepadButtonFlags.DPadUp)]
    public GamepadButtonFlags DpadUp    { get; set; } = GamepadButtonFlags.DPadUp;

    [DisplayName("DPad: Down")]
    [DefaultValue(GamepadButtonFlags.DPadDown)]
    public GamepadButtonFlags DpadDown  { get; set; } = GamepadButtonFlags.DPadDown;

    [DisplayName("DPad: Left")]
    [DefaultValue(GamepadButtonFlags.DPadLeft)]
    public GamepadButtonFlags DpadLeft  { get; set; } = GamepadButtonFlags.DPadLeft;

    [DisplayName("DPad: Right")]
    [DefaultValue(GamepadButtonFlags.DPadRight)]
    public GamepadButtonFlags DpadRight { get; set; } = GamepadButtonFlags.DPadRight;

    [DisplayName("Camera Right")]
    [DefaultValue(GamepadButtonFlags.RightShoulder)]
    public GamepadButtonFlags CameraR   { get; set; } = GamepadButtonFlags.RightShoulder;

    [DisplayName("Camera Left")]
    [DefaultValue(GamepadButtonFlags.LeftShoulder)]
    public GamepadButtonFlags CameraL   { get; set; } = GamepadButtonFlags.LeftShoulder;

    [DisplayName("Start")]
    [DefaultValue(GamepadButtonFlags.Start)]
    public GamepadButtonFlags Start     { get; set; } = GamepadButtonFlags.Start;

    [DisplayName("Team Blast")]
    [DefaultValue(GamepadButtonFlags.Back)]
    public GamepadButtonFlags TeamBlast { get; set; } = GamepadButtonFlags.Back;
}