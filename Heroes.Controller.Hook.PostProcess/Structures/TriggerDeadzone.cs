using Heroes.Controller.Hook.PostProcess.Misc;
using System.ComponentModel;

namespace Heroes.Controller.Hook.PostProcess.Structures;

public class TriggerDeadzone
{
    [DisplayName("Trigger Deadzone")]
    [Description("When the trigger is under this value (in percent), the value is reported to the game as 0.")]
    [DefaultValue(15f)]
    public float DeadzonePercent    { get; set; } = 15.0f;

    [DisplayName("Trigger Radius")]
    [Description("The radius of the trigger.\n" +
                 "Example: A value of 50 means the game will only see the trigger\n" +
                 "moved 50% of the way across when fully pressed.")]
    [DefaultValue(100f)]
    public float RadiusPercent      { get; set; } = 100.0f;

    [DisplayName("Inverted?")]
    [Description("Inverts the readings from the triggers.\n" +
                 "i.e. The game will think the trigger is fully pressed when it is not pressed at all.")]
    [DefaultValue(false)]
    public bool  IsInverted         { get; set; } = false;

    public float GetMinimumPressure()  => (DeadzonePercent / Percent.MaxValue) * byte.MaxValue;

    public byte ApplySettings(byte stickValue)
    {
        stickValue = ApplyDeadzone(stickValue);
        stickValue = ScaleValue(stickValue);
        if (IsInverted)
            stickValue = (byte)(byte.MaxValue - stickValue);

        return stickValue;
    }

    public byte ScaleValue(byte stickValue)
    {
        var scaled = stickValue * (RadiusPercent / Percent.MaxValue);
        if (scaled > byte.MaxValue)
            scaled = byte.MaxValue;

        return (byte)scaled;
    }

    public byte ApplyDeadzone(byte triggerValue)
    {
        if (triggerValue < GetMinimumPressure())
            return 0;

        return triggerValue;
    }

    public override string ToString() => $"Deadzone: {DeadzonePercent}, Invert: {IsInverted}";
}