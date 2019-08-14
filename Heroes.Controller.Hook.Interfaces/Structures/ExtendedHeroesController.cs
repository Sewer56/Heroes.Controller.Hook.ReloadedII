using System;
using System.Collections.Generic;
using System.Text;
using Heroes.Controller.Hook.Interfaces.Enums;
using Heroes.Controller.Hook.Interfaces.Internal;

namespace Heroes.Controller.Hook.Interfaces.Structures
{
    public struct ExtendedHeroesController
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

        public ExtendedHeroesController(ref IHeroesController controller, byte leftTriggerPressure = 0, byte rightTriggerPressure = 0)
        {
            ButtonFlags = controller.ButtonFlags;
            MinusOneMinusButtonFlags = controller.MinusOneMinusButtonFlags;
            OneFramePressButtonFlag = controller.OneFramePressButtonFlag;
            OneFrameReleaseButtonFlag = controller.OneFrameReleaseButtonFlag;
            LeftStickX = controller.LeftStickX;
            LeftStickY = controller.LeftStickY;
            RightStickX = controller.RightStickX;
            RightStickY = controller.RightStickY;
            LeftTriggerPressure = leftTriggerPressure;
            RightTriggerPressure = rightTriggerPressure;
        }
    }
}
