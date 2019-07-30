using System;
using System.Text.Json.Serialization;
using Heroes.Controller.Hook.PostProcess.Misc;

namespace Heroes.Controller.Hook.PostProcess.Structures
{
    public class StickDeadzone
    {
        public bool  IsEnabled          { get; set; } = true;
        public float DeadzonePercent    { get; set; } = 25.0F;

        [JsonIgnore]
        public float MinimumValue => (DeadzonePercent / Percent.MaxValue);

        /// <summary>
        /// Applies the deadzone to the provided stick value.
        /// If the value is in the deadzone and the deadzone is enabled, returns 0, otherwise returns original value.
        /// </summary>
        /// <param name="stickValue">Value between 0 and 1.0.</param>
        public float ApplyDeadzone(float stickValue)
        {
            if (Math.Abs(stickValue) < MinimumValue && IsEnabled)
                return 0;

            return stickValue;
        }
    }
}
