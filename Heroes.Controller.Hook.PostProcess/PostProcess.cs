using System;
using Heroes.Controller.Hook.Interfaces.Structures;
using Heroes.Controller.Hook.PostProcess.Configuration;

namespace Heroes.Controller.Hook.PostProcess
{
    public class PostProcess
    {
        private const int ControllerCount = 4;

        private Config[] _configurations;
        private Configurator _configurator;

        public PostProcess(string modFolder)
        {
            _configurator       = new Configurator(modFolder);
            _configurations     = new Config[ControllerCount];
            for (int x = 0; x < _configurations.Length; x++)
            {
                _configurations[x] = _configurator.GetConfiguration<Config>(x);
                _configurations[x].Save();
            }
        }

        /// <summary>
        /// Sends inputs to the Inter Mod Communication's <see cref="Inputs"/> structure.
        /// </summary>
        public void PostProcessInputs(ref Inputs inputs, int port)
        {
            var config = _configurations[port];

            inputs.LeftStickX = config.LeftStickXDeadzone.ApplyDeadzone(inputs.LeftStickX);
            inputs.LeftStickY = config.LeftStickYDeadzone.ApplyDeadzone(inputs.LeftStickY);
            inputs.RightStickX = config.RightStickXDeadzone.ApplyDeadzone(inputs.RightStickX);
            inputs.RightStickY = config.RightStickYDeadzone.ApplyDeadzone(inputs.RightStickY);
            inputs.LeftTriggerPressure = config.LeftTriggerDeadzone.ApplyDeadzone(inputs.LeftTriggerPressure);
            inputs.RightTriggerPressure = config.RightTriggerDeadzone.ApplyDeadzone(inputs.RightTriggerPressure);

            if (config.SwapTriggers)
            {
                byte leftTriggerPressure    = inputs.LeftTriggerPressure;
                inputs.LeftTriggerPressure  = inputs.RightTriggerPressure;
                inputs.RightTriggerPressure = leftTriggerPressure;
            }
        }
    }
}
