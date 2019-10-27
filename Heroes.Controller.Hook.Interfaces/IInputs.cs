using System;
using System.Collections.Generic;
using System.Text;
using Heroes.Controller.Hook.Interfaces.Definitions;

namespace Heroes.Controller.Hook.Interfaces.Structures.Interfaces
{
    public interface IInputs
    {
        /// <summary>
        /// Contains the currently pressed buttons at any point.
        /// </summary>
        ButtonFlags ButtonFlags { get; set; }

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
