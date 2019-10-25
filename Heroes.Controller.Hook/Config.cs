using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Heroes.Controller.Hook.Shared;
using Heroes.Controller.Hook.Shared.Configuration;

namespace Heroes.Controller.Hook
{
    public class Config : Configurable
    {
        /// <summary>
        /// Uses original inputs acquired from the game.
        /// </summary>
        [DisplayName("If set to true, original inputs from the game are not overwritten. This may be desirable for those who still want mouse support.")]
        public bool UseOriginalInputs { get; set; } = false;
    }
}
