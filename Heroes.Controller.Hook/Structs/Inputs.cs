using Heroes.Controller.Hook.Interfaces.Structures.Interfaces;
using Heroes.SDK.Definitions.Structures.Input;
using ButtonFlags = Heroes.Controller.Hook.Interfaces.Definitions.ButtonFlags;

namespace Heroes.Controller.Hook.Interfaces.Structures
{
    /// <summary>
    /// Contains all of the inputs to be passed to the game.
    /// </summary>
    public struct Inputs : IInputs
    {
        /// <summary>
        /// Contains the currently pressed buttons at any point.
        /// </summary>
        public Definitions.ButtonFlags ButtonFlags { get; set; }

        /// <summary>
        /// Range -1.0 to 1.0.
        /// </summary>
        public float LeftStickX { get; set; }

        /// <summary>
        /// Range -1.0 to 1.0.
        /// </summary>
        public float LeftStickY { get; set; }

        /// <summary>
        /// Range -1.0 to 1.0.
        /// </summary>
        public float RightStickX { get; set; }

        /// <summary>
        /// Range -1.0 to 1.0.
        /// </summary>
        public float RightStickY { get; set; }

        /// <summary>
        /// Range 0 to 255.
        /// </summary>
        public byte LeftTriggerPressure { get; set; }

        /// <summary>
        /// Range 0 to 255.
        /// </summary>
        public byte RightTriggerPressure { get; set; }

        /// <summary>
        /// Instantiates this structure given original Heroes Controller Inputs.
        /// </summary>
        public static Inputs FromHeroesController(HeroesController controller)
        {
            var inputs = new Inputs
            {
                ButtonFlags = (ButtonFlags) controller.ButtonFlags,
                LeftStickX = controller.LeftStickX,
                LeftStickY = controller.LeftStickY,
                RightStickX = controller.RightStickX,
                RightStickY = controller.RightStickY,
                LeftTriggerPressure = 0,
                RightTriggerPressure = 0
            };

            return inputs;
        }

        /// <summary>
        /// Converts the <see cref="Inputs"/> structure into the <see cref="HeroesController"/> structure.
        /// Note: <see cref="HeroesController"/> structure must still be finalized using <see cref="Finalize"/>
        /// with inputs from the last frame.
        /// </summary>
        public void ToHeroesController(out HeroesController controller)
        {
            controller = new HeroesController();
            controller.ButtonFlags = (SDK.Definitions.Structures.Input.ButtonFlags) ButtonFlags;
            controller.LeftStickX = LeftStickX;
            controller.LeftStickY = LeftStickY;
            controller.RightStickX = RightStickX;
            controller.RightStickY = RightStickY;
            controller.ProbablyIsEnabled = 1;

            // If triggers are pressed, enable the trigger buttons.
            if (LeftTriggerPressure > 0)
                controller.ButtonFlags |= (SDK.Definitions.Structures.Input.ButtonFlags) ButtonFlags.CameraL;

            if (RightTriggerPressure > 0)
                controller.ButtonFlags |= (SDK.Definitions.Structures.Input.ButtonFlags) ButtonFlags.CameraR;
        }
    }
}
