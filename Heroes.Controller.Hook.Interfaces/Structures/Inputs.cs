using Heroes.Controller.Hook.Interfaces.Enums;
using Heroes.Controller.Hook.Interfaces.Internal;

namespace Heroes.Controller.Hook.Interfaces.Structures
{
    /// <summary>
    /// Contains all of the inputs to be passed to the game.
    /// </summary>
    public struct Inputs
    {
        /// <summary>
        /// Contains the currently pressed buttons at any point.
        /// </summary>
        public ButtonFlags ButtonFlags;

        /// <summary>
        /// Range -1.0 to 1.0.
        /// </summary>
        public float LeftStickX;

        /// <summary>
        /// Range -1.0 to 1.0.
        /// </summary>
        public float LeftStickY;

        /// <summary>
        /// Range -1.0 to 1.0.
        /// </summary>
        public float RightStickX;

        /// <summary>
        /// Range -1.0 to 1.0.
        /// </summary>
        public float RightStickY;

        /// <summary>
        /// Range 0 to 255.
        /// </summary>
        public byte LeftTriggerPressure;

        /// <summary>
        /// Range 0 to 255.
        /// </summary>
        public byte RightTriggerPressure;

        /// <summary>
        /// Instantiates this structure given original Heroes Controller Inputs.
        /// </summary>
        public static Inputs FromHeroesController(IHeroesController controller)
        {
            var inputs = new Inputs
            {
                ButtonFlags = controller.ButtonFlags,
                LeftStickX = controller.LeftStickX,
                LeftStickY = controller.LeftStickY,
                RightStickX = controller.RightStickX,
                RightStickY = controller.RightStickY,
                LeftTriggerPressure = 0,
                RightTriggerPressure = 0
            };

            return inputs;
        }
    }
}
