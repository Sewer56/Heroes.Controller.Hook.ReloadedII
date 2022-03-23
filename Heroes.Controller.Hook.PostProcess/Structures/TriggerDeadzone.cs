using Heroes.Controller.Hook.PostProcess.Misc;

namespace Heroes.Controller.Hook.PostProcess.Structures;

public class TriggerDeadzone
{
    public bool  IsEnabled          { get; set; }
    public float DeadzonePercent    { get; set; }
    public float RadiusPercent      { get; set; } = 100.0f;
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
        if (triggerValue < GetMinimumPressure() && IsEnabled)
            return 0;

        return triggerValue;
    }

    public override string ToString() => $"Enabled: {IsEnabled}, Deadzone: {DeadzonePercent}, Invert: {IsInverted}";
}