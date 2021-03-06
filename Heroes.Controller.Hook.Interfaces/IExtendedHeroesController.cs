﻿using System;
using System.Collections.Generic;
using System.Text;
using Heroes.Controller.Hook.Interfaces.Definitions;

namespace Heroes.Controller.Hook.Interfaces.Structures.Interfaces
{
    public interface IExtendedHeroesController
    {
        /// <summary>
        /// Contains the currently pressed buttons at any point.
        /// </summary>
        ButtonFlags ButtonFlags { get; set; }

        /// <summary>
        /// This value is (-1 - _buttonFlags).
        /// This value is also 0 when the window is not in focus.
        /// </summary>
        int MinusOneMinusButtonFlags { get; set; }

        /// <summary>
        /// If a button is pressed and it was not pressed the last frame,
        /// set the <see cref="ButtonFlags"/> of said button.
        /// </summary>
        ButtonFlags OneFramePressButtonFlag { get; set; }

        /// <summary>
        /// If a button is released and it was pressed the last frame,
        /// set the <see cref="ButtonFlags"/> of said button.
        /// </summary>
        ButtonFlags OneFrameReleaseButtonFlag { get; set; }

        /// <summary>
        /// Range -1.0 to 1.0.
        /// </summary>
        float LeftStickX { get; set; }

        /// <summary>
        /// Range -1.0 to 1.0.
        /// </summary>
        float LeftStickY { get; set; }

        /// <summary>
        /// Range -1.0 to 1.0.
        /// </summary>
        float RightStickX { get; set; }

        /// <summary>
        /// Range -1.0 to 1.0.
        /// </summary>
        float RightStickY { get; set; }

        /// <summary>
        /// Range 0 to 255.
        /// </summary>
        byte LeftTriggerPressure { get; set; }

        /// <summary>
        /// Range 0 to 255.
        /// </summary>
        byte RightTriggerPressure { get; set; }
    }
}
