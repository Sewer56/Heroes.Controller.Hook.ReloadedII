using System.ComponentModel;
using Heroes.Controller.Hook.PostProcess.Misc;

namespace Heroes.Controller.Hook.PostProcess.Structures;

public class StickDeadzone
{
    public const float MinValue = -1.0f;
    public const float MaxValue = 1.0f;

    [DisplayName("Deadzone (Percent)")]
    [Description("When the stick is under this value (in percent), the value is reported to the game as 0.")]
    [DefaultValue(25.0f)]
    public float DeadzonePercent    { get; set; } = 25.0F;


    [DisplayName("Stick Radius")]
    [Description("The radius of the stick in this axis.\n" +
                 "Example: A value of 50 means the game will only see the stick\n" +
                 "moved 50% of the way across when fully held in the direction.")]
    [DefaultValue(100f)]
    public float RadiusPercent      { get; set; } = 100.0f;

    [DisplayName("Invert Stick")]
    [Description("Reverses the direction of the stick in this axis.")]
    [DefaultValue(false)]
    public bool  IsInverted         { get; set; } = false;

    public float GetMinimumValue() => (DeadzonePercent / Percent.MaxValue);

    public float ApplySettings(float stickValue)
    {
        stickValue = ApplyDeadzone(stickValue);
        stickValue = ScaleValue(stickValue);
        if (IsInverted)
            stickValue *= -1;

        return stickValue;
    }

    public float ScaleValue(float stickValue)
    {
        var scaled = stickValue * (RadiusPercent / Percent.MaxValue);
        if (scaled > MaxValue)
            scaled = MaxValue;

        if (scaled < MinValue)
            scaled = MinValue;

        return scaled;
    }

    public float ApplyDeadzone(float stickValue)
    {
        if (Math.Abs(stickValue) < GetMinimumValue())
            return 0;

        return stickValue;
    }

    public override string ToString() => $"Deadzone: {DeadzonePercent}, Invert: {IsInverted}";
}