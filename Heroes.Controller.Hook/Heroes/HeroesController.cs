using Heroes.Controller.Hook.Interfaces.Enums;
using Heroes.Controller.Hook.Interfaces.Internal;
using Heroes.Controller.Hook.Interfaces.Structures;

namespace Heroes.Controller.Hook.Heroes
{
    /// <summary>
    /// Describes a Sonic Heroes controller structure.
    /// </summary>
    public struct HeroesController : IHeroesController
    {
        /// <summary>
        /// Contains the currently pressed buttons at any point.
        /// </summary>
        public ButtonFlags ButtonFlags { get; set; }

        /// <summary>
        /// This value is (-1 - _buttonFlags).
        /// This value is also 0 when the window is not in focus.
        /// </summary>
        public int MinusOneMinusButtonFlags { get; set; }

        /// <summary>
        /// If a button is pressed and it was not pressed the last frame,
        /// set the <see cref="ButtonFlags"/> of said button.
        /// </summary>
        public ButtonFlags OneFramePressButtonFlag { get; set; }

        /// <summary>
        /// If a button is released and it was pressed the last frame,
        /// set the <see cref="ButtonFlags"/> of said button.
        /// </summary>
        public ButtonFlags OneFrameReleaseButtonFlag { get; set; }

        /* Range: -1.0 to 1.0 */
        public float LeftStickX { get; set; }
        public float LeftStickY { get; set; }
        public float RightStickX { get; set; }
        public float RightStickY { get; set; }

        /// <summary>
        /// This value never seems to change. It does not have any effect on the game.
        /// </summary>
        public int ProbablyIsEnabled;

        /*
            ------------------
            Functions (Public)
            ------------------
        */

        /// <summary>
        /// Before submitting back to game, completes the remaining members of the struct
        /// that are dependent on knowing the inputs from the previous frame.
        /// </summary>
        /// <param name="before">The inputs that were pressed on the last frame.</param>
        public void Finalize(ButtonFlags before)
        {
            OneFrameReleaseButtonFlag   = GetReleasedButtons(before, ButtonFlags);
            OneFramePressButtonFlag     = GetPressedButtons(before, ButtonFlags);
            MinusOneMinusButtonFlags    = GetMinusOneButtonFlags(ButtonFlags);
        }

        /// <summary>
        /// Converts the <see cref="Inputs"/> structure into the <see cref="HeroesController"/> structure.
        /// Note: <see cref="HeroesController"/> structure must still be finalized using <see cref="Finalize"/>
        /// with inputs from the last frame.
        /// </summary>
        public static void FromInputs(ref Inputs inputs, out HeroesController controller)
        {
            controller = new HeroesController();
            controller.ButtonFlags = inputs.ButtonFlags;
            controller.LeftStickX  = inputs.LeftStickX;
            controller.LeftStickY  = inputs.LeftStickY;
            controller.RightStickX = inputs.RightStickX;
            controller.RightStickY = inputs.RightStickY;
            controller.ProbablyIsEnabled = 1;

            // If triggers are pressed, enable the trigger buttons.
            if (inputs.LeftTriggerPressure > 0)
                controller.ButtonFlags |= ButtonFlags.CameraL;

            if (inputs.RightTriggerPressure > 0)
                controller.ButtonFlags |= ButtonFlags.CameraR;
        }

        /*
            -------------------
            Functions (Private)
            -------------------
        */

        /// <summary>
        /// Returns the buttons that have been pressed <see cref="before"/> but not pressed <see cref="after"/>.
        /// </summary>
        private ButtonFlags GetReleasedButtons(ButtonFlags before, ButtonFlags after)
        {
            // Return B and NOT A
            // "Return those before without the ones after"
            return before & (~after);
        }

        /// <summary>
        /// Returns the buttons that have been pressed in <see cref="after"/> but not pressed <see cref="before"/>.
        /// </summary>
        private ButtonFlags GetPressedButtons(ButtonFlags before, ButtonFlags after)
        {
            // Return A and NOT B
            // "Return those after without the ones before"
            return after & (~before);
        }

        private int GetMinusOneButtonFlags(ButtonFlags buttonFlags)
        {
            if (Utility.IsWindowActivated())
                return (-1 - (int)buttonFlags);

            return 0;
        }
    }
}
