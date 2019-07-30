using Heroes.Controller.Hook.Interfaces;
using Heroes.Controller.Hook.Interfaces.Enums;

namespace Heroes.Controller.Hook.Heroes
{
    /// <summary>
    /// The internal structure consumed by the game logic which actually determines the behaviour of the game.
    /// </summary>
    public struct SkyPad
    {
        public ButtonFlags ButtonFlags;
        public int MinusOneMinusButtonFlags;
        public int OneFramePressButtonFlags;
        public int OneFrameReleaseButtonFlags;
        public short TriggerPressureL;          // 0-255
        public short TriggerPressureR;          // 0-255
        public int Field14;
        public int Field18;
        public int LeftAnalogStickVector;

        public float LeftAnalogStickForce;
        public int RightAnalogStickVector;      // Max 1.0F
        public float RightAnalogStickForce;     // Max 1.0F

        // Repetition count for button presses. Ignored, we let the game handle this.
        public int Field2C;
        public int Field30;
        public int Field34;
        public int Field38;
        public int Field3C;
        public int Field40;
        public int Field44;
        public int Field48;
    }
}
