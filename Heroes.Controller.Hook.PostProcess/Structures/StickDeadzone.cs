using System;
using System.Text.Json.Serialization;
using Heroes.Controller.Hook.PostProcess.Misc;

namespace Heroes.Controller.Hook.PostProcess.Structures
{
    public class StickDeadzone
    {
        public const float MinValue = -1.0f;
        public const float MaxValue = 1.0f;

        public bool  IsEnabled          { get; set; } = true;
        public float DeadzonePercent    { get; set; } = 25.0F;
        public float RadiusScale        { get; set; } = 1.0f;
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
            var scaled = stickValue * RadiusScale;
            if (scaled > MaxValue)
                scaled = MaxValue;

            if (scaled < MinValue)
                scaled = MinValue;

            return scaled;
        }

        public float ApplyDeadzone(float stickValue)
        {
            if (Math.Abs(stickValue) < GetMinimumValue() && IsEnabled)
                return 0;

            return stickValue;
        }

        public override string ToString() => $"Enabled: {IsEnabled}, Deadzone: {DeadzonePercent}, Invert: {IsInverted}";
    }
}
